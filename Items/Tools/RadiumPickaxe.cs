using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class RadiumPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 4;
            item.useAnimation = 12;
            item.pick = 235;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starminer");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RadiumBar", 12);
            recipe.AddTile(mod, "QuantumFusionAccelerator");
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
