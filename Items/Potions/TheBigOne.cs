using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class TheBigOne : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Big One");
            Tooltip.SetDefault("Heals a LOT");
		}
		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 38;
			item.useTurn = true;
			item.maxStack = 50;
			item.healLife = 600;
            item.healMana = 600;
            item.useAnimation = 17;
			item.useTime = 17;
			item.useStyle = 2;
			item.UseSound = SoundID.Item3;
			item.consumable = true;
			item.potion = true;
			item.value = 100000;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.overrideColor = new Color(216, 110, 40);
	            }
	        }
	    }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GrandHealingPotion");
            recipe.AddIngredient(null, "GrandManaPotion");
            recipe.AddRecipeGroup("AAMod:SuperAncientMaterials");
            recipe.AddTile(null, "BinaryReassembler");
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
}