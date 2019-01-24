using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class FulguritePlating : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            soundType = 21;
            drop = mod.ItemType("FulguritePlating");   //put your CustomBlock name
            dustType = mod.DustType("FulguriteDust");
            AddMapEntry(new Color(70, 20, 90));
			minPick = 200;
        }


        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(Color.Violet, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/FulguritePlating_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(spriteBatch, glowTex, x, y, true, false, false, null, AAGlobalTile.GetstormColorDim);
            }
        }
    }
}