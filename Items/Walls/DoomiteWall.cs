using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Walls
{
    public class DoomiteWall : BaseAAItem
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
            item.createWall = mod.WallType("DoomiteWall"); //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Plating Wall");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
