using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Doomite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charged Doomite Bar");
            Tooltip.SetDefault("Unsettling energy radiates from this bar");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEnergy", 2);
            recipe.AddIngredient(null, "DeactivatedDoomite", 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}