using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class YtriumBar : ModItem
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
      DisplayName.SetDefault("Yttrium Bar");
      Tooltip.SetDefault("");
    }

		public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YtriumOre", 4);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
