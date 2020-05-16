using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Bricks
{
    public class TerraBrick : BaseAAItem
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
            item.createTile = mod.TileType("KeepBrickS");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keep Brick");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "KeepBrick", 300);
            recipe.AddIngredient(ModContent.ItemType<Materials.HeroShards>(), 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this, 300);
            recipe.AddRecipe();
        }
    }
}
