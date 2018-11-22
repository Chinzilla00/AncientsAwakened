using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class BookOfSacrifice : ModItem
    {
			public override void SetDefaults()
			{
				item.width = 32;
				item.height = 32;
                item.value = Item.buyPrice(0, 1, 0, 0);
            }

		public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Book Of Sacrifice");
				Tooltip.SetDefault("");
			}
	
		public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddIngredient(ItemID.RedDye, 1);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
