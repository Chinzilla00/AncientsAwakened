using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Bricks
{
    public class SkymarbleBrick : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("SkymarbleBrick");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skymarble Brick");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyCrystal", 1);
            recipe.AddIngredient(ItemID.Marble, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
