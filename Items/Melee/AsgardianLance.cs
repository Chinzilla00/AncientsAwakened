using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Melee
{
    public class AsgardianLance : BaseAAItem
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
            item.shoot = mod.ProjectileType("AsgardianLance");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.AsgardianLanceShot>(), damage, knockBack, item.owner);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RaiderLance", 1);
            recipe.AddIngredient(mod, "IceCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}