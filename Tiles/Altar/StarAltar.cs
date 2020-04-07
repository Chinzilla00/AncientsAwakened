using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BaseMod;

namespace AAMod.Tiles.Altar
{
    public class StarAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = mod.DustType("RadiumDust");
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Altar of the Stars");
            AddMapEntry(new Color(100, 80, 20), name);
            disableSmartCursor = true;
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (AAWorld.StarActive)
            {
                if (frame < 1) frame = 1;
                if (++frameCounter >= 5)
                {
                    frameCounter = 0;
                    if (++frame >= 8) frame = 1;
                }
            }
            else
            {
                frame = 0;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (AAWorld.StarActive)
            {
                r = Color.DarkGoldenrod.R / 2;
                g = Color.DarkGoldenrod.G / 2;
                b = Color.DarkGoldenrod.B / 2;
            }
        }


        public static Color lightColor = Color.Violet;

        public Texture2D glowTex = null;

        public Color GetColor(Color color)
        {
            Color glowColor = Color.White;
            return glowColor;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/StarAltar_Glow");
            if (glowTex != null && tile != null && tile.active() && tile.type == Type)
            {
                int width = 16, height = 16;
                int frameX = tile != null && tile.active() ? tile.frameX : 0;
                int frameY = tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 54) : 0;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor);
                for (int m = 0; m < 3; m++)
                {
                    BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor, new Vector2(Main.rand.Next(-3, 4) * 0.5f, Main.rand.Next(-3, 4) * 0.5f));
                }
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
    }
}