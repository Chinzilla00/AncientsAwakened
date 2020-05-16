using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomWorkbench : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Workbench");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 18;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("DoomWorkbench");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ApocalyptitePlate"), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}