using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
	public class FireballP : ModProjectile
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
			projectile.alpha = 10;
			projectile.aiStyle = 1;
			aiType = 14;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
		}

		public override void AI()
		{
			projectile.alpha = 10;
			for (int index1 = 0; index1 < 3; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 100, new Color(), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				Main.dust[index2].scale *= 0.8f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 100, new Color(), 1f);
				Main.dust[index3].velocity *= 2f;
				Main.dust[index3].scale *= 0.8f;
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
			Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 14);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DummyExplosion"), (int)(projectile.damage * 1.2f), projectile.knockBack, projectile.owner, -10f, 0f);
			for (int index1 = 0; index1 < 30; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index2].velocity *= 1.4f;
			}
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 7f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index3].velocity *= 3f;
			}
			for (int index1 = 0; index1 < 2; ++index1)
			{
				float num2 = 0.4f;
				if (index1 == 1)
				num2 = 0.8f;
				int index2 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index2].velocity *= num2;
				++Main.gore[index2].velocity.X;
				++Main.gore[index2].velocity.Y;
				int index3 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index3].velocity *= num2;
				--Main.gore[index3].velocity.X;
				++Main.gore[index3].velocity.Y;
				int index4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index4].velocity *= num2;
				++Main.gore[index4].velocity.X;
				--Main.gore[index4].velocity.Y;
				int index5 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index5].velocity *= num2;
				--Main.gore[index5].velocity.X;
				--Main.gore[index5].velocity.Y;
			}
		}
	}
}
