using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class EFlairon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Flairon");
            Tooltip.SetDefault("Lets loose an armada of homing bubbles");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Flairon);
            item.damage = 300;
            item.rare = 11;
            item.shoot = mod.ProjectileType("EFlairon");
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flairon);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}