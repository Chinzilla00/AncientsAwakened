using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Sagittarius
{
    public class SagTrophy : ModItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sagittarius Trophy");
		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 1;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 2000;
			item.rare = 1;
            item.expert = true;
			item.createTile = mod.TileType("SagTrophy");
		}
	}
}