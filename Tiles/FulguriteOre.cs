using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class FulguriteOre : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public Color color;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            soundType = 21;
            drop = mod.ItemType("FulguriteShard");   //put your CustomBlock name
            dustType = mod.DustType("FulguriteDust");
            AddMapEntry(new Color(204, 0, 150));
			minPick = 200;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Tile tile = Main.tile[x, y];
            Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Violet, Color.White, Color.White);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Black, Color.Violet, Color.Black, Color.Violet, Color.Black, Color.Black, Color.Black);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/FulguriteOre_Glow"), new Vector2((x * 16) - (int)Main.screenPosition.X, (y * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}