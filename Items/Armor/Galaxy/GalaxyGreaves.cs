using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Galaxy
{
	[AutoloadEquip(EquipType.Legs)]
	public class GalaxyGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galactic Greaves");
			Tooltip.SetDefault("7% increased critical strike chance"
				+ "\n+12% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 10;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 7;
			player.thrownVelocity += 0.12f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Galaxy_Fragment", 15);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.SetResult(this);
			recipe.AddTile(null, "Soul_Forge_Placed");
			recipe.AddRecipe();
		}
	}
}