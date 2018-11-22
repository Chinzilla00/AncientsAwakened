using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Dragon
{
	[AutoloadEquip(EquipType.Legs)]
	public class Dragon_Greaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Greaves");
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
            recipe.AddIngredient(null, "Dragon_Scale", 8); //24        10, 8, 6
            recipe.AddIngredient(ItemID.HellstoneBar, 4);//12      3, 5, 4
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}