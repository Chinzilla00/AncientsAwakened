using Terraria.ModLoader;

namespace AAMod.Items.Walls
{
    public class DoomsdayWall : BaseAAItem
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
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = mod.WallType("DoomsdayWall"); //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomsday Curcuit Wall");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayPlating");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
