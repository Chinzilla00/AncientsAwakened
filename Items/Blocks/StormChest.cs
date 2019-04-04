using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class StormChest : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Storm Chest");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 5;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("StormChest");
		}

		public override void AddRecipes()
		{
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddRecipeGroup("AAMod:Iron", 2);
                recipe.AddIngredient(null, "FulguriteBar", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}