using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class UraniumSword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.useStyle = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.knockBack = 6f;
            item.width = 52;
            item.height = 52;
            item.damage = 60;
            item.scale = 1.15f;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.value = 103500;
            item.melee = true;
            item.shoot = mod.ProjectileType<Projectiles.Radiosphere>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Hazardblade");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(mod, "UraniumBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
