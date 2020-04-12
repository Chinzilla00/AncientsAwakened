using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class EquinoxBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("EquinoxBrick");   
            AddMapEntry(Color.DarkGoldenrod);
            dustType = ModContent.DustType<Dusts.RadiumDust>();
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            if (Main.dayTime)
            {
                BaseDrawing.DrawTileTexture(spriteBatch, Main.tileTexture[Type], x, y, true, false, false);
            }
            else
            {
                BaseDrawing.DrawTileTexture(spriteBatch, mod.GetTexture("Tiles/Bricks/DarkmatterBrick"), x, y, true, false, false);
            }
            return false;
        }
    }
}
