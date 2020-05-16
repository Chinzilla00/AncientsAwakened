using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Piano");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("BogwoodPiano");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Bogwood"), 15);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.Bone, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}