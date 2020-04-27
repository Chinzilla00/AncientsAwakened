
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class EquinoxWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            dustType = mod.DustType("RadiumDust");
            AddMapEntry(new Color(60, 60, 30));
            soundType = 21;
            drop = mod.ItemType("EquinoxWall");
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            if (Main.dayTime)
            {
                BaseDrawing.DrawWallTexture(spriteBatch, Main.wallTexture[Type], x, y, true);
            }
            else
            {
                BaseDrawing.DrawWallTexture(spriteBatch, mod.GetTexture("Walls/Bricks/DarkmatterWall"), x, y, true);
            }
            return false;
        }
    }
}