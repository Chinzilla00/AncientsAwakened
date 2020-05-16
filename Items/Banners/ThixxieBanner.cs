using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Banners
{
	public class ThixxieBanner : BaseAAItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			item.width = 32;
			item.height = 32;
			item.maxStack = 9999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.createTile = mod.TileType("ThixxieBanner");
			item.placeStyle = 0;
		}
	    public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FatPixieBanner", 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}