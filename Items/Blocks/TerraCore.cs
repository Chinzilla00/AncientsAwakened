using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TerraCore : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of Terraria");
            Tooltip.SetDefault(@"Combines most crafting stations into one
Used to create ancient crafting stations");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 11;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 1000000;
            item.createTile = mod.TileType("TerraCore");
        }
        public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.overrideColor = new Color(70, 200, 30);
            }
        }
        }   

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:AstralStations", 1);
            recipe.AddIngredient(null, "TruePaladinsSmeltery", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
