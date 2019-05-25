using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria;

namespace AAMod.Items.Melee
{
    public class AsgardianLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asgardian Lance");		
		}

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.shootSpeed = 10f;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("AsgardianLance");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RaiderLance", 1);
            recipe.AddIngredient(mod, "IcePrism", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}