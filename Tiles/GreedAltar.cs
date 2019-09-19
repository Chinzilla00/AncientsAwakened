using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using AAMod.NPCs.Bosses.Greed;

namespace AAMod.Tiles
{
    public class GreedAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            dustType = DustID.BlueCrystalShard;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Desire Altar");
            AddMapEntry(new Color(80, 50, 0), name);
            disableSmartCursor = true;
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(mod.NPCType("Greed")) || NPC.AnyNPCs(mod.NPCType("GreedSpawn")) || NPC.AnyNPCs(mod.NPCType("GreedA")) || NPC.AnyNPCs(mod.NPCType("GreedTransition")))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public override void RightClick(int i, int j)
        {
            if (NPC.AnyNPCs(mod.NPCType("Greed")) || NPC.AnyNPCs(mod.NPCType("GreedSpawn")) || NPC.AnyNPCs(mod.NPCType("GreedA")) || NPC.AnyNPCs(mod.NPCType("GreedTransition")))
            {
                return;
            }
            Player player = Main.LocalPlayer;
            int type = mod.ItemType<Items.BossSummons.GoldenGrub>();
            if (BasePlayer.HasItem(player, type, 1))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        if (AAWorld.downedGreed)
                        {
                            AAModGlobalNPC.SpawnBoss(player, mod.NPCType<Greed>(), true, 0, 0, "Greed");
                        }
                        else
                        {
                            AAModGlobalNPC.SpawnBoss(player, mod.NPCType<GreedSpawn>(), false, new Vector2(i * 16, (j * 16) - 200), "Greed");
                        }
                    }
                }
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
    }
}