using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TerraPrismStation : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Infuser");
            Tooltip.SetDefault(@"Used to infuse biome prisms with the power of the biome you are currently in");
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
            recipe.AddIngredient(null, "Prism", 5);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(null, "HallowedAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
