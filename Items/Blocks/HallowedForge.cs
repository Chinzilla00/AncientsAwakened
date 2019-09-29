using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class HallowedForge : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Forge");
            Tooltip.SetDefault("It's amazing what this thing CAN'T cook");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 7;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150000;
            item.createTile = mod.TileType("HallowedForge");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HallowedOre", 20);
            recipe.AddRecipeGroup("AAMod:HForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
