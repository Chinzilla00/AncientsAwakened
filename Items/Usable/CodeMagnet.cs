using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class CodeMagnet : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Code Magnet");
			Tooltip.SetDefault("'Pulls items to you by moving its code closer to you'");
		}


        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 4;
            item.maxStack = 1;
			item.value = 8000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddTile(null, "HellstoneAnvil");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
