using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Crafters
{
    public class ChaosCrucible : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = mod.DustType("AbyssiumDust");
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("ChaosCrucible");
            AddMapEntry(new Color(40, 0, 0), name);
            disableSmartCursor = true;
            adjTiles = new int[]
            {
                mod.TileType("ACS"),
            };
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0.50f;
        }

        public Color White(Color color)
        {
            return Color.White;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/ChaosCrucible_Glow");
            Texture2D Sphere = mod.GetTexture("Glowmasks/ChaosCrucible_Sphere");
            int frameY = tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 54) : 0;

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, frameY, false, false, false, null, White);
            BaseDrawing.DrawTileTexture(sb, Sphere, x, y, 16, 16, tile.frameX, frameY, false, false, false, null, AAGlobalTile.GetShenColorBright);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("ChaosCrucible"));
        }
    }
}