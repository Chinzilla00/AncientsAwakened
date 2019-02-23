using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.DevTools
{
	public class AncientOreGenner : ModItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Ancient Ore Genner");		
            BaseUtility.AddTooltips(item, new string[] { "Spawns Ancient Awakened Ores!" });	
		}

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 7;
			item.expert = true;
            item.value = BaseMod.BaseUtility.CalcValue(0, 15, 0, 0);

			item.useStyle = 500;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;		
        }

		public override bool UseItem(Player player)
		{
			AAGlobalTile.GenAAOres(true);
			return true;
		}

		public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }		
	}
}