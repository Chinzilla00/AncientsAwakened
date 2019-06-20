using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria;

namespace AAMod.Items.Melee
{
    public class RaiderLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raider Lance");		
		}

        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 2.3f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = 10800;
            item.rare = 3;
            item.shootSpeed = 7f;
            item.shoot = mod.ProjectileType("RaiderLance");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "VikingPolearm", 1);
            recipe.AddIngredient(mod, "HydrasSpear", 1);
            recipe.AddIngredient(mod, "SaltwaterSpear", 1);
            recipe.AddIngredient(mod, "Executioner", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}