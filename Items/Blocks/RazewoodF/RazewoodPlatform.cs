using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Razewood Platform");
		}

		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 10;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("RazewoodPlatform");
		}

		public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Razewood");
            recipe.SetResult(this, 2);
            recipe.AddRecipe(); 
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.SetResult(null, "Razewood");
            recipe.AddRecipe();
        }
	}
}
