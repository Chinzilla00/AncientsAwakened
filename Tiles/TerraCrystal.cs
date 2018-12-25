using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TerraCrystal : ModTile
    {

        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][mod.TileType("TerraWood")] = true;
            soundType = 21;
            Main.tileLighted[Type] = true;
            dustType = 107;
            AddMapEntry(new Color(0, 200, 51));
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;

            BaseMod.BaseDrawing.DrawTileTexture(spriteBatch, mod.GetTexture("Tiles/TerraCrystal"), i, j, true, false, false, null, AAGlobalTile.GetTerraColorDim);
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(Color.LimeGreen, 0.7f);
            r = ((float)color.R / 255f); g = ((float)color.G / 255f); b = ((float)color.B / 255f);
        }
    }
}