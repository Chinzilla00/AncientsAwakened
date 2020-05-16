using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodWall : BaseAAItem
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
            item.createWall = mod.WallType("BogwoodWall"); //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Wall");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bogwood");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(null, "Bogwood");
            recipe.AddRecipe();
        }
    }
}
