using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Oroboros
{
    public class OroborosWorkbench : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oroboros Workbench");
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
            item.createTile = mod.TileType("OroborosWorkbench");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OroborosWood"), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}