using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Keep
{
    public class KeepSink : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keep Sink");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("KeepSink");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TerraShard"), 6);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}