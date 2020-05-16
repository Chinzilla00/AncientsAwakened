using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class TerraWoodSolid : ModTile
    {

        public bool glow = true; 
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("TerraWood")] = true;
            Main.tileMerge[Type][mod.TileType("TerraLeaves")] = true;
            Main.tileMerge[Type][mod.TileType("TerraCrystal")] = true;
            soundType = SoundID.Tink;
            Main.tileLighted[Type] = true;
            dustType = 107;
            AddMapEntry(new Color(52, 200, 0));
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
            BaseDrawing.DrawTileTexture(spriteBatch, Main.tileTexture[Type], i, j, true, false, false, null, AAGlobalTile.GetTerraColorBright);
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(Color.LimeGreen, 1f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }
    }
}