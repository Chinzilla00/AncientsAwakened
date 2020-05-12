using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Furniture.Doom
{
    public class DoomClock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(200, 0, 0), name);
            dustType = mod.DustType("DoomDust");
            adjTiles = new int[] { TileID.GrandfatherClocks };
        }

        public override bool NewRightClick(int x, int y)
        {
            if (!AAWorld.downedZero)
            {
                Main.NewText(Lang.TilesInfo("DoomClockGetTime1"), 200, 0, 0);
            }
            else
            {
                Main.NewText(Lang.TilesInfo("DoomClockGetTime2"), 200, 0, 0);
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.clock = true;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType("DoomClock"));
        }


        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = 16;
            Texture2D tex = mod.GetTexture("Tiles/Furniture/Doom/DoomClock");
            Texture2D Glow = mod.GetTexture("Glowmasks/DoomClock_Glow");
            if (AAWorld.downedZero)
            {
                tex = mod.GetTexture("Tiles/Furniture/Doom/DoomClock0");
                Glow = mod.GetTexture("Glowmasks/DoomClock0_Glow");
            }
            Main.spriteBatch.Draw(tex, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Glow, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}