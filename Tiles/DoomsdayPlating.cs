using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DoomsdayPlating : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            soundType = 21;
            drop = mod.ItemType("DoomsdayPlating");   //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(70, 50, 50
                ));
			minPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.active() && tile.type == Type)
            {
                Texture2D glowTex = mod.GetTexture("Glowmasks/ApocalyptiteTile_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}