using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ChaosJavelinEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Javelin");
            Tooltip.SetDefault(@"Explodes on contact
Chaos Javelin EX");
        }

        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("ChaosJavelinEX");
            item.shootSpeed = 12f;
            item.damage = 200;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 1000000;
            item.rare = 11;
            item.expert = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosJavelin");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
