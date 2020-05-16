using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodLantern : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Lantern");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("BogwoodLantern");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Bogwood"), 6);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}