using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Paladin
{
	[AutoloadEquip(EquipType.Head)]
	public class Paladin_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
			DisplayName.SetDefault("Paladin Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = 10000;
			item.rare = 8;
			item.vanity = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("Paladin_Chestplate") && legs.type == mod.ItemType("Paladin_Boots");
		}
	}
}