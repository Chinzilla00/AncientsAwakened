using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Void : BaseAAItem
    {

        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.Terrarian);
            item.damage = 190;                            
            item.value = 1000000;
            item.rare = 9;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 12;
            item.useTime = 12;
            item.rare = 11;
            item.shoot = mod.ProjectileType("Void");  
		}

        public override void SetStaticDefaults()
        {
             DisplayName.SetDefault("Void");
            Tooltip.SetDefault("Made out of pure Dark Matter");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkEnergy", 5);
            recipe.AddIngredient(null, "DarkMatter", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
