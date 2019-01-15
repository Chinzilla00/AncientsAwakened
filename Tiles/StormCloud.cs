using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class StormCloud : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public Color color;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][TileID.Cloud] = true;
            Main.tileMergeDirt[Type] = false;
            dustType = mod.DustType("FulguriteDust");
            AddMapEntry(new Color(60, 20, 90));
			minPick = 9999;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Tile tile = Main.tile[x, y];
            Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Violet, BaseDrawing.GetLightColor(new Vector2(x, y)), BaseDrawing.GetLightColor(new Vector2(x, y)));
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(new Vector2(x, y)), BaseDrawing.GetLightColor(new Vector2(x, y)), Color.Violet, BaseDrawing.GetLightColor(new Vector2(x, y)), Color.Violet, BaseDrawing.GetLightColor(new Vector2(x, y)));
            Vector2 zero=  new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/StormCloud_Glow"), new Vector2((x * 16) - (int)Main.screenPosition.X, (y * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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