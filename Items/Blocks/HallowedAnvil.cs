using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class HallowedAnvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Anvil");
            Tooltip.SetDefault("A Holy Anvil");
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
            item.rare = 7;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 100000;
            item.createTile = mod.TileType("HallowedAnvil");
        }

        public override void AddRecipes()
        { 
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.HallowedBar, 10);
                recipe.AddIngredient(ItemID.MythrilAnvil, 1);
                recipe.AddIngredient(ItemID.PearlwoodWorkBench, 1);
                recipe.AddIngredient(ItemID.CrystalBall, 1);
                recipe.AddIngredient(ItemID.HeavyWorkBench, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.HallowedBar, 10);
                recipe.AddIngredient(ItemID.OrichalcumAnvil, 1);
                recipe.AddIngredient(ItemID.PearlwoodWorkBench, 1);
                recipe.AddIngredient(ItemID.CrystalBall, 1);
                recipe.AddIngredient(ItemID.HeavyWorkBench, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
