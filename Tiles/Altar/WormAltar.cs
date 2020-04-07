using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Equinox;
using Terraria.DataStructures;
using Terraria.Enums;

namespace AAMod.Tiles.Altar
{
    public class WormAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            dustType = DustID.BlueCrystalShard;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Worm Altar");
            AddMapEntry(new Color(150, 100, 0), name);
            disableSmartCursor = true;
            animationFrameHeight = 72;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(mod.NPCType("DaybringerHead")) || NPC.AnyNPCs(mod.NPCType("NightcrawlerHead")))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<Items.BossSummons.Owl>();
            bool Worms = NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>()) || NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>());
            if (BasePlayer.HasItem(player, type, 1) && !Worms)
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        SpawnBoss(player, ModContent.NPCType<WormSpawn>(), false, player.Center);
                    }
                }
            }
            return true;
        }

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 Pos = default, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            if (overrideDirection == 0)
                overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            if (overrideDirectionY == 0)
                overrideDirectionY = -1;
            Vector2 npcCenter = Pos + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
            AAModGlobalNPC.SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return AAWorld.downedEquinox;
        }

        public override bool CanExplode(int i, int j)
        {
            return AAWorld.downedEquinox;
        }
    }
}