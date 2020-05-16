using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Terra
{
    public class TerraBookcase : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Bookcase");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("TerraBookcase");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TerraShard"), 20);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddIngredient(ItemID.Book, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}