using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class YtriumBar : BaseAAItem
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
            item.rare = 2;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("YttriumBar");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Bar");
            Tooltip.SetDefault("");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YtriumOre", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
