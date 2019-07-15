using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class FreedomStar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Star");
            Tooltip.SetDefault(@"Tails' trusty blaster.
Hold the use button to charge, and then release a powerful Charged Shot!
Kept you waiting, huh?
Tails
Mobian Buster EX");
        }

        public override void SetDefaults()
        {
            item.width = 74;
            item.height = 34;
            item.ranged = true;
            item.damage = 200;  
            item.shoot = mod.ProjectileType("FreedomStar");
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            item.noMelee = true;
			item.rare = 11;
			item.shootSpeed = 12f;
			item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "MobianBuster");
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}

// pls nerf
