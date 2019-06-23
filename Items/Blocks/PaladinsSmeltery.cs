using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class PaladinsSmeltery : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Paladin's Smeltery Forge");
            Tooltip.SetDefault(
@"This thing can make a lot of stuff
Functions as most hardmode crafting stations + A workbench and heavy workbench");
        }

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 9;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("PaladinsSmeltery");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "HallowedAnvil", 1);
                recipe.AddIngredient(null, "HallowedForge", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
