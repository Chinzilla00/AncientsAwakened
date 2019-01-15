using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System.Diagnostics;
using System.Collections.Generic;

namespace AAMod.Tiles
{
    class DracoAltar : ModTile
	{
        Texture2D SunTexture = null;

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Draconian Sun Pedestal");
            dustType = mod.DustType("AkumaDust");
            AddMapEntry(new Color(200, 50, 0), name);
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
            if (SunTexture == null)
            {
                if (!AAWorld.downedAllAncients)
                {
                    SunTexture = mod.GetTexture("Tiles/DraconianAltarSun");
                }
                else
                {
                    SunTexture = mod.GetTexture("Tiles/DiscordianEclipse");
                }
            }
            Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int num3 = 0;
            int num8 = 16;
            for (int num301 = 0; num301 < num3; num301++)
            {
                int num302 = Main.specX[num301];
                int num303 = Main.specY[num301];
                Tile tile6 = Main.tile[num302, num303];
                ushort type4 = tile6.type;
                short frameX = tile6.frameX;
                Vector2 SunVector1 = new Vector2((num302 * 16) - (int)Main.screenPosition.X + (num8 / 2f), (num303 * 16) - (int)Main.screenPosition.Y - 36) + zero;
                Rectangle source = new Rectangle(0, 0, SunTexture.Width, SunTexture.Height);
                Color color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 0);
                Vector2 origin = new Vector2((float)(SunTexture.Width / 2), (float)(SunTexture.Height / 2));
                if (NPC.downedMoonlord && !AAWorld.downedAllAncients)
                {
                    Main.spriteBatch.Draw(SunTexture, SunVector1, new Rectangle?(source), color, Main.sunCircle, origin, 1f, SpriteEffects.None, 0f);
                }
                if (NPC.downedMoonlord && AAWorld.downedAllAncients)
                {
                    Main.spriteBatch.Draw(SunTexture, SunVector1, new Rectangle?(source), color, Main.sunCircle, origin, 1f, SpriteEffects.None, 0f);
                }
            }
        }

        public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("DracoAltar");
		}
	}
}
