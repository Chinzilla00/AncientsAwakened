using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TerraPrismStation : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Core");
            Tooltip.SetDefault(@"The 'craft-all'.
Combiles all vanilla and Ancients Awakened crafting stations together");
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
            item.rare = 9;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 100000;
            item.createTile = mod.TileType("TerraPrism");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FurnitureDynamo", 1);
            recipe.AddIngredient(null, "TerraCore", 1);
            recipe.AddRecipeGroup("AAMod:ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
