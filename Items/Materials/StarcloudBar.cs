using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class StarcloudBar : ModItem
    {
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 0, 70, 0);

            item.width = 32;
            item.height = 32;
			item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcloud Bar");
      Tooltip.SetDefault("");
    }

		public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:Gold");            //example of how to craft with a modded item
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddIngredient(ItemID.Cloud, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
