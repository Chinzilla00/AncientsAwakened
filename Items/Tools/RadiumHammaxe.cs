using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class RadiumHammaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 70;
            item.melee = true;
            item.width = 44;
            item.height = 40;
            item.useTime = 10;
            item.useAnimation = 20;
            item.axe = 190;
            item.hammer = 45;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Hammaxe");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
