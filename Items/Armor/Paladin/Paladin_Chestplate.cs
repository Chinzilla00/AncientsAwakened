using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Paladin
{
	[AutoloadEquip(EquipType.Body)]
	public class Paladin_Chestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Paladin Chestplate");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10000;
			item.rare = 8;
			item.vanity = true;
		}
	}
}