using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class SpiralStairs : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Stairs");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 2;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 100;
            item.createTile = mod.TileType("SpiralStairs");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddRecipeGroup("AAMod:Iron", 2);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
