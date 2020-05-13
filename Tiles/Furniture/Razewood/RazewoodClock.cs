using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Furniture.Razewood
{
    public class RazewoodClock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            // name.SetDefault("Example Clock"); // Automatic from .lang files
            AddMapEntry(new Color(205, 62, 12), name);
            dustType = mod.DustType("RazewoodDust");
            adjTiles = new int[] { TileID.GrandfatherClocks };
        }

        public override bool NewRightClick(int x, int y)
        {
            Main.NewText(Lang.TilesInfo("RazewoodClockGetTime"), 205, 62, 12);
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.clock = true;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType("RazewoodClock"));
        }
    }
}