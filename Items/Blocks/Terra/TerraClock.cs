using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Terra
{
    public class TerraClock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Clock");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("TerraClock");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TerraShard"), 10);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddRecipeGroup("IronBar");
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}