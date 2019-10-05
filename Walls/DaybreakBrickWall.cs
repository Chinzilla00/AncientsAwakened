using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class DaybreakBrickWall : ModWall
	{
        public Texture2D glowTex;
        public bool glow = true;

        public override void SetDefaults()
        {
            Main.wallHouse[this.Type] = true;
            drop = mod.ItemType("DaybreakWall");
            AddMapEntry(new Color(40, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            if (glow)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/DaybreakBrickWall_Glow");
                BaseMod.BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetAkumaColorDim);
            }
        }
    }
}