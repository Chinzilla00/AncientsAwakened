using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Doom Chest");
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
            item.rare = 1;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("DoomChest");
		}

		public override void AddRecipes()
		{
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.IronBar, 2);
                recipe.AddIngredient(null, "ApocalyptitePlate", 12);
                recipe.AddTile(null, "ACS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.LeadBar, 2);
                recipe.AddIngredient(null, "ApocalyptitePlate", 12);
                recipe.AddTile(null, "ACS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}