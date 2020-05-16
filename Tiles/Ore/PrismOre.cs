using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class PrismOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 420; 
            TileID.Sets.Ore[Type] = true;
            soundType = SoundID.Tink;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("Prism");   
            dustType = DustID.Stone;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Prism Ore");
            AddMapEntry(new Color(100, 100, 100), name);
			minPick = 65;
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
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/PrismOre_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), new Color(Main.DiscoR / 3, Main.DiscoG / 3, Main.DiscoB / 3), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}