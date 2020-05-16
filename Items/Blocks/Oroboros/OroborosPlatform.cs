using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Oroboros
{
    public class OroborosPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Oroboros Wood Platform");
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
			item.createTile = mod.TileType("OroborosPlatform");
		}

		public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OroborosWood");
            recipe.SetResult(this, 2);
            recipe.AddRecipe(); 
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.SetResult(null, "OroborosWood");
            recipe.AddRecipe();
        }
	}
}
