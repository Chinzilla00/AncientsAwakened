using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class GlowingMushiumBar : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Mushium Bar");
            Tooltip.SetDefault("Glowy");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.White, lightColor, lightColor, Color.White);
        }

        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlowingMushium", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
