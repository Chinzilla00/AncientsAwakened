using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class UraniumAnvil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Anvil");
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
            item.rare = 4;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 10000;
            item.createTile = mod.TileType("UraniumAnvil");
        }

        public override void AddRecipes()
        { 
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "UraniumBar", 10);
                recipe.AddIngredient(ItemID.Anvils, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
