using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class TorchstoneWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("TorchstoneWall");
            AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TorchstoneWall_Glow");
            BaseMod.BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetIncineriteColorDim);
        }
    }
}