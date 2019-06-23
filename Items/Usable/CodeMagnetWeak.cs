using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class CodeMagnetWeak : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uncharged Code Magnet");
			Tooltip.SetDefault("'Pulls items to you by moving its code closer to you'");
		}

        public override void SetDefaults()
        {
            item.useStyle = 4;
            item.width = 16;
            item.height = 16;
            item.rare = 2;
            item.maxStack = 1;
			item.value = 1000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
