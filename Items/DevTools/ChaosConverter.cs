using System; 
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Worldgen;

namespace AAMod.Items.DevTools
{
	//meant for testing, shows you how to run the generator
	public class ChaosConverter : ModItem
	{
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.rare = 10;
            item.value = 0;

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;		
        }

		public override bool UseItem(Player player)
		{
			ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionHandler.CONVERTID_INFERNO);
			return true;
		}
	}
}