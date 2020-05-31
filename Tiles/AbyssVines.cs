using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AbyssVines : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            AddMapEntry(new Color(50, 0, 0));
        }


        public Texture2D glowTex;
        public bool glow = true;

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.active() && tile.type == Type)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Tiles/AbyssVines");
                BaseDrawing.DrawTileTexture(spriteBatch, glowTex, x, y, true, false, false, null, Globals.AAGlobalTile.GetYamataColorDim);
            }
        }
    }
}