using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class ApocalyptitePlate : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 30;
            item.maxStack = 99;
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apocalyptite Plate");
            Tooltip.SetDefault("A forboding energy rings from this metal plating");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(90, 0, 20);
                }
            }
        }

        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Apocalyptite", 5);              //example of how to craft with a modded item
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
