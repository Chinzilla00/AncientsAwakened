using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bogwood Chest");
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
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("BogwoodChest");
		}

		public override void AddRecipes()
		{
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.IronBar, 2);
                recipe.AddIngredient(null, "Bogwood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.LeadBar, 2);
                recipe.AddIngredient(null, "Bogwood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}