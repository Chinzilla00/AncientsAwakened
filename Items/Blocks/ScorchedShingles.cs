using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    class ScorchedShingles : BaseAAItem
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
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("ScorchedShingles"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorched Dynasty Shingles");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RedDynastyShingles, 1);
            recipe.needLava = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
