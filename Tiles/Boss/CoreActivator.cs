using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Boss
{
    public class CoreActivator : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            dustType = 107;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Core Engine");
            AddMapEntry(new Color(0, 150, 50), name);
            disableSmartCursor = true;
            animationFrameHeight = 36;
        }

        public Color White(Color color)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/CoreActivator_Glow");

            int frameY = tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 36) : 0;
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, frameY, false, false, false, null, White);
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<Items.Materials.Crystal>();
            if (CoreWorld.PrismCharged)
            {
                player.QuickSpawnItem(ModContent.ItemType<Items.Materials.TerraCrystal>());
                CoreWorld.PrismCharged = false;
                return true;
            }
            if (BasePlayer.HasItem(player, type) && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Core.Core>()))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        SpawnBoss(player, ModContent.NPCType<NPCs.Bosses.Core.Core>(), player.Center);
                    }
                }
            }
            return true;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (CoreWorld.PedestalActive)
            {
                frame = 1;
            }
            else if (CoreWorld.PrismCharged)
            {
                frame = 2;
            }
            else
            {
                frame = 0;
            }
        }

        public static void SpawnBoss(Player player, int bossType, Vector2 Pos = default)
        {
            Vector2 npcCenter = Pos + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * Main.rand.Next(2) == 0 ? -1 : 1, -800f);

            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                }

                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate = true;

                BaseUtility.Chat("The Biome Core whirs to life!", 175, 75, 255, false);
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, true, (int)npcCenter.X, (int)npcCenter.Y, "The Biome Core whirs to life!", false);
            }
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType("Crystal");
        }
    }

    public class CoreWorld : ModWorld
    {
        public static bool PedestalActive;
        public static bool PrismCharged;

        public override void PostUpdate()
        {
            PedestalActive = NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Core.Core>());
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = PedestalActive;
            flags[1] = PrismCharged;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            PedestalActive = flags[0];
            PrismCharged = flags[1];
        }
    }
}