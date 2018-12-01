using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class TorchstoneWall : ModWall
	{
        public Texture2D glowTex;
        public bool glow = true;

        public override void SetDefaults()
        {
            Main.wallHouse[this.Type] = true;
            drop = mod.ItemType("TorchstoneWall");
            AddMapEntry(new Color(25, 12, 10));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(AAPlayer.IncineriteColor, 0.7f);
            r = ((float)color.R / 255f); g = ((float)color.G / 255f); b = ((float)color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            if (glow)
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/TorchstoneWall_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetIncineriteColorDim);
            }
        }
    }
}