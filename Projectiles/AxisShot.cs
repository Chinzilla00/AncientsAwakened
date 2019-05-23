using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AxisShot : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.CloneDefaults(343);
			projectile.light = 1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axis Shot");
        }
		
        public override void AI()
        {
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 45f)
			{
				projectile.ai[0] = 45f;
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
				if (projectile.velocity.Y > 16f)
				{
					projectile.velocity.Y = 16f;
				}
				projectile.velocity.X = projectile.velocity.X * 0.995f;
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
			projectile.alpha -= 50;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] >= 4f)
				{
					projectile.localAI[0] = 0f;
					int num566 = 0;
					int num3;
					for (int num567 = 0; num567 < 1000; num567 = num3 + 1)
					{
						if (Main.projectile[num567].active && Main.projectile[num567].owner == projectile.owner && Main.projectile[num567].type == 344)
						{
							num566++;
						}
						num3 = num567;
					}
					float num568 = (float)projectile.damage * 0.8f;
					if (num566 > 100)
					{
						float num569 = (float)(num566 - 100);
						num569 = 1f - num569 / 110f;
						num568 *= num569;
					}
					if (num566 > 100)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 120)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 140)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 150)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 160)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 165)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num566 > 170)
					{
						projectile.localAI[0] -= 2f;
					}
					if (num566 > 175)
					{
						projectile.localAI[0] -= 3f;
					}
					if (num566 > 180)
					{
						projectile.localAI[0] -= 4f;
					}
					if (num566 > 185)
					{
						projectile.localAI[0] -= 5f;
					}
					if (num566 > 190)
					{
						projectile.localAI[0] -= 6f;
					}
					if (num566 > 195)
					{
						projectile.localAI[0] -= 7f;
					}
					if (num568 > (float)projectile.damage * 0.1f)
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AxisSnow"), (int)num568, projectile.knockBack * 0.55f, projectile.owner, 0f, (float)Main.rand.Next(3));
						return;
					}
				}
			}
        }
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, projectile.position);
			int num3;
			for (int num369 = 4; num369 < 31; num369 = num3 + 1)
			{
				float num370 = projectile.oldVelocity.X * (30f / (float)num369);
				float num371 = projectile.oldVelocity.Y * (30f / (float)num369);
				int num372 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num370, projectile.oldPosition.Y - num371), 8, 8, 180, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
				Main.dust[num372].noGravity = true;
				Dust dust = Main.dust[num372];
				dust.velocity *= 0.5f;
				num3 = num369;
			}
		}
		
		public bool stop = false;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			if (!stop)
			{
				Vector2 vel1 = new Vector2(-1, -1);
				vel1 *= 5f;
				Projectile.NewProjectile(target.position.X+130, target.position.Y+130, vel1.X, vel1.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel2 = new Vector2(1, 1);
				vel2 *= 5f;
				Projectile.NewProjectile(target.position.X-130, target.position.Y-130, vel2.X, vel2.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel3 = new Vector2(1, -1);
				vel3 *= 5f;
				Projectile.NewProjectile(target.position.X-130, target.position.Y+130, vel3.X, vel3.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel4 = new Vector2(-1, 1);
				vel4 *= 5f;
				Projectile.NewProjectile(target.position.X+130, target.position.Y-130, vel4.X, vel4.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel5 = new Vector2(0, -1);
				vel5 *= 5f;
				Projectile.NewProjectile(target.position.X, target.position.Y+130, vel5.X, vel5.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel6 = new Vector2(0, 1);
				vel6 *= 5f;
				Projectile.NewProjectile(target.position.X, target.position.Y-130, vel6.X, vel6.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel7 = new Vector2(1, 0);
				vel7 *= 5f;
				Projectile.NewProjectile(target.position.X-130, target.position.Y, vel7.X, vel7.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-1, 0);
				vel8 *= 5f;
				Projectile.NewProjectile(target.position.X+130, target.position.Y, vel8.X, vel8.Y, mod.ProjectileType("AxisSnow"), projectile.damage/3, 0, Main.myPlayer);
				stop = true;
			}
		}
		
		public override Color? GetAlpha(Color newColor)
		{
			float num6 = 1f - (float)projectile.alpha / 255f;
			return new Color((int)(250f * num6), (int)(250f * num6), (int)(250f * num6), (int)(100f * num6));
		}
    }
}
