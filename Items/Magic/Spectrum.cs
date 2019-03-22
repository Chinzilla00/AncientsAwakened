using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic 
{
	public class Spectrum : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spectrum");
			Tooltip.SetDefault("Focusing devastating beam of the light");
		}

	    public override void SetDefaults()
	    {
	        item.damage = 118;
	        item.magic = true;
	        item.mana = 14;
	        item.width = 16;
	        item.height = 16;
	        item.useTime = 10;
	        item.useAnimation = 10;
	        item.reuseDelay = 5;
	        item.useStyle = 5;
	        item.UseSound = SoundID.Item13;
	        item.noMelee = true;
	        item.noUseGraphic = true;
			item.channel = true;
	        item.knockBack = 0f;
	        item.value = 1000000;
	        item.shoot = mod.ProjectileType("Spectrum");
	        item.shootSpeed = 30f;
			item.rare = 9;
	    }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LastPrism);
			recipe.AddIngredient(mod.ItemType("EXSoul"));
			recipe.AddTile(null, "AncientForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}