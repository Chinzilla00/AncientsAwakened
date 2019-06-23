using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class StarcloudBar : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 70, 0);

            item.width = 32;
            item.height = 32;
			item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("StarcloudBar");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcloud Bar");
      Tooltip.SetDefault("");
    }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:Gold");
            recipe.AddIngredient(ItemID.FallenStar, 1);
			recipe.AddIngredient(ItemID.Cloud, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
