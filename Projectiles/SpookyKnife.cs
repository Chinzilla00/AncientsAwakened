using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class SpookyKnife : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(14);
			projectile.penetrate = 1;
			projectile.width = 16;
			projectile.height = 16;
            projectile.ranged = true;
            projectile.friendly = true;
			projectile.timeLeft = 300;
			projectile.alpha = 0;
			projectile.aiStyle = 1;
			aiType = 14;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
			projectile.light = 0.33f;
		}

		public override void AI()
		{
			projectile.alpha = 0;
			if (Main.rand.Next(3) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 6,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300);
			projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 7);
			for (int h = 0; h < 3; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int type = Main.rand.Next(326,328);
				int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, type, projectile.damage/2, 0, Main.myPlayer);
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].friendly = true;
			}
		}
	}
}
