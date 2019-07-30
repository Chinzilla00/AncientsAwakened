using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AcropolisClouds : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = -1;
            AddMapEntry(new Color(30, 89, 125));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.Sky, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool PreDraw(int i, int j, SpriteBatch sb)
        {
            Tile tile = Main.tile[i, j];
            if (tile != null && tile.active() && tile.type == this.Type)
            {
                BaseMod.BaseDrawing.DrawTileTexture(sb, Main.tileTexture[Type], i, j, true, false, false, null, GetSkyColorBright);
            }
            return false;
        }

        public static Color GetSkyColorBrightInvert(Color color) { return GetSkyColor(color, 1f, 0.6f, true); }
        public static Color GetSkyColorDim(Color color) { return GetSkyColor(color, 0.4f, 1f, false); }
        public static Color GetSkyColorBright(Color color) { return GetSkyColor(color, 0.6f, 1f, false); }
        public static Color GetSkyColor(Color color, float min, float max, bool clamp)
        {
            return AAGlobalTile.GetTimedColor(GetAlpha(AAColor.Sky), color, min, max, clamp);
        }

        public static Color GetAlpha(Color newColor)
        {
            float num = (255 - AAWorld.CloudAlpha) / 255f;
            int num2 = (int)(newColor.R * num);
            int num3 = (int)(newColor.G * num);
            int num4 = (int)(newColor.B * num);
            int num5 = newColor.A - AAWorld.CloudAlpha;
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num5 > 255)
            {
                num5 = 255;
            }
            return new Color(num2, num3, num4, num5);
        }
    }
}