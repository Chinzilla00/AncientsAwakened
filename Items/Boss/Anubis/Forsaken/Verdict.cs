using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
	public class Verdict : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Verdict");
			Tooltip.SetDefault(@"Releases enchanted sand rain on enemy hit
Creates 2 forsaken phantom blades which hit enemy horizontally as well");
		}

		public override void SetDefaults()
		{
			item.damage = 140;
			item.melee = true;
			item.crit = 10;
			item.width = 96;
			item.height = 92;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.value = 100000;
			item.rare = 11;
            item.knockBack = 4;
            item.autoReuse = true;
			item.UseSound = SoundID.Item1;
			item.scale = 1.1f;
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			Vector2 vel1 = new Vector2(0, -1);
			vel1 *= 8f;
			Projectile.NewProjectile(target.Center.X, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-5, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-10, target.Center.Y-18, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-15, target.Center.Y-16, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-20, target.Center.Y-14, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-25, target.Center.Y-12, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-30, target.Center.Y-10, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/2, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+5, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+10, target.Center.Y-18, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+15, target.Center.Y-16, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+20, target.Center.Y-14, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+25, target.Center.Y-12, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+30, target.Center.Y-10, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/2, 0, Main.myPlayer);
			Vector2 vel2 = new Vector2(-1, 0);
			vel2 *= 16f;
			Vector2 vel3 = new Vector2(1, 0);
			vel3 *= 16f;
			Projectile.NewProjectile(target.Center.X + 600, target.Center.Y, vel2.X, vel2.Y, mod.ProjectileType("ForsakenPhantomBlade"), damage/2, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X - 600, target.Center.Y, vel3.X, vel3.Y, mod.ProjectileType("ForsakenPhantomBlade"), damage/2, 0, Main.myPlayer);
		}
	}
}
