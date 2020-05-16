using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomCandle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Candle");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 18;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("DoomCandle");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ApocalyptitePlate"), 4);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}