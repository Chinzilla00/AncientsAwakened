using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Bricks
{
    public class RadiumBrick : BaseAAItem
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
            item.createTile = mod.TileType("RadiumBrick");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Brick");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumOre", 1);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
