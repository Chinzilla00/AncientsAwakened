using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Galaxy
{
	[AutoloadEquip(EquipType.Head)]
	public class GalaxyHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galactic Helmet");
			Tooltip.SetDefault("15% increased thrown damage");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
			item.defense = 8;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("GalaxyBreastplate") && legs.type == mod.ItemType("GalaxyGreaves");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.15f;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.20f; 
			player.setBonus = "+20% movement speed";
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Galaxy_Fragment", 10);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.SetResult(this);
			recipe.AddTile(null, "Soul_Forge_Placed");
			recipe.AddRecipe();
		}
	}
}