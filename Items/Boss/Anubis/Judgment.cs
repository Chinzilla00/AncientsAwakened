using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Anubis
{
	public class Judgment : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Judgement");
			Tooltip.SetDefault("Releases enchanted sand rain on enemy hit");
		}

		public override void SetDefaults()
		{
			item.damage = 32;
			item.melee = true;
			item.crit = 10;
			item.width = 52;
			item.height = 52;
			item.useTime = 21;
			item.useAnimation = 21;
			item.useStyle = 1;
			item.value = 100000;
			item.rare = 6;
            item.knockBack = 4;
            item.autoReuse = true;
			item.UseSound = SoundID.Item1;
			item.scale = 1.1f;
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			Vector2 vel1 = new Vector2(0, -1);
			vel1 *= 11f;
			Projectile.NewProjectile(target.Center.X, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-5, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-10, target.Center.Y-18, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-15, target.Center.Y-16, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-20, target.Center.Y-14, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X-25, target.Center.Y-12, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+5, target.Center.Y-20, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+10, target.Center.Y-18, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+15, target.Center.Y-16, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+20, target.Center.Y-14, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(target.Center.X+25, target.Center.Y-12, vel1.X, vel1.Y, mod.ProjectileType("EnchantedSand"), damage/3, 0, Main.myPlayer);
		}
	}
}
