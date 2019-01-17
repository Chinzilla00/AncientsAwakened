using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System.Diagnostics;
using System.Collections.Generic;
using Terraria.Enums;
using System;

namespace AAMod.Tiles
{
    class DreadAltar : ModTile
    {
        Texture2D MoonTexture = null;

        public override void SetDefaults()
		{
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            ModTranslation name = CreateMapEntryName();
            TileObjectData.addTile(Type);
            minPick = 200;
            mineResist = 3f;
            name.SetDefault("Dread Moon Pedestal");
            dustType = mod.DustType("YamataDust");
            AddMapEntry(new Color(200, 200, 200), name);
		}

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("DreadAltar"));
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            if (MoonTexture == null)
            {
                if (!AAWorld.downedAllAncients)
                {
                    MoonTexture = mod.GetTexture("Tiles/DreadAltarMoon");
                }
                else
                {
                    MoonTexture = mod.GetTexture("Tiles/DiscordianEclipse");
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
                Rectangle source = new Rectangle(0, 0, MoonTexture.Width, MoonTexture.Height);
                Color color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 0);
                Vector2 origin = new Vector2((float)(MoonTexture.Width / 2), (float)(MoonTexture.Height / 2));
                if (NPC.downedMoonlord)
                {
                    Main.spriteBatch.Draw(MoonTexture, SunVector1, new Rectangle?(source), color, Main.sunCircle, origin, 1f, SpriteEffects.None, 0f);
                }
            }
        }

        public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("DreadAltar");
		}
	}
}
