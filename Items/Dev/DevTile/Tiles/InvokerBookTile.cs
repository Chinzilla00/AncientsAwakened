using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ObjectData;
using Terraria.ModLoader.IO;

namespace AAMod.Items.Dev.DevTile.Tiles
{
    public class InvokerBookTile : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.addTile(Type);
			drop = mod.ItemType("InvokerBook");
			ModTranslation modTranslation = CreateMapEntryName(null);
			modTranslation.SetDefault("Aleister Book");
			AddMapEntry(Color.Gold, modTranslation);
			animationFrameHeight = 16;
		}

        public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = mod.ItemType("InvokerBook");
		}

        public override bool NewRightClick(int i, int j)
		{
            Item.NewItem(i * 16, j * 16, 16, 16, mod.ItemType("InvokerBook"), 1, false, 0, false, false);
            WorldGen.KillTile(i, j, false, false, true);
            return true;
		}
    }
}