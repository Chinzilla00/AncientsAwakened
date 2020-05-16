using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Doom Platform");
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
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = mod.TileType("DoomPlatform");
		}

		public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate");
            recipe.SetResult(this, 2);
            recipe.AddRecipe(); 
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.SetResult(null, "ApocalyptitePlate");
            recipe.AddRecipe();
        }
	}
}
