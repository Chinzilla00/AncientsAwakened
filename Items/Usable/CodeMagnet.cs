using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class CodeMagnet : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charged Code Magnet");
			Tooltip.SetDefault("'Pulls items to you by moving its code closer to you'");
		}

        public override void SetDefaults()
        {
            item.useStyle = 4;
            item.width = 16;
            item.height = 16;
            item.rare = 4;
            item.maxStack = 1;
			item.value = 8000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddIngredient(null, "CodeMagnetWeak", 20);
            recipe.AddTile(null, "HellstoneAnvil");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
