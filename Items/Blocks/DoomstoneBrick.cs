using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;

namespace AAMod.Items.Blocks
{
    public class DoomstoneBrick : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 10;
            item.consumable = true;
            item.createTile = mod.TileType("DoomstoneBrick"); //put your CustomBlock Tile name
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomstone Brick");
            Tooltip.SetDefault("");
           
        }
        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Doomstone", 3);
                recipe.AddTile(null, "ACS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
