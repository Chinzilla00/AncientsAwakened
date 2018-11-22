using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Dream
{
	[AutoloadEquip(EquipType.Legs)]
	public class Dream_Greaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dream Greaves");
			Tooltip.SetDefault("7% increased critical strike chance"
				+ "\n+12% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 3, 50, 0);
			item.rare = 5;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 7;
			player.thrownVelocity += 0.12f;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Nightmare_Bar", 6); //20       04, 10, 06
            recipe.AddIngredient(ItemID.CrystalShard, 16);//48                  12, 20, 16
			recipe.AddIngredient(ItemID.HallowedBar, 12);//36                 08, 16, 12
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}