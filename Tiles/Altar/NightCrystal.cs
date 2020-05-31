using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Altar
{
    public class NightCrystal : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileBlendAll[Type] = false;
            soundType = SoundID.Tink;
            Main.tileLighted[Type] = true;
            dustType = DustID.BlueCrystalShard;
            AddMapEntry(Color.DarkBlue);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(Main.tileTexture[Type], new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), c(), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public Color c()
        {
            if (AAWorld.GravActive)
            {
                return Globals.AAColor.COLOR_WHITEFADE1;
            }
            return Color.Transparent;
        }
    }
}