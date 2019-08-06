using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
            if (NPC.AnyNPCs(mod.NPCType("Greed")))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public Color White(Color color)
        {
            return AAColor.Sky;
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            int type = mod.ItemType<Items.BossSummons.GoldenGrub>();
            if (BasePlayer.HasItem(player, type, 1))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        if (item.stack <= 0)
                        {
                            item = new Item();
                        }
                        AAModGlobalNPC.SpawnBoss(player, mod.NPCType<NPCs.Bosses.Greed.Greed>(), true, 0, 0, "Greed", false);
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