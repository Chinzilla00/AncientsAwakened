using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    class RottedShingles : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 24;
            item.height = 22;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RottedShingles"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotted Shingles");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BlueDynastyShingles, 1);
            recipe.needWater = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
