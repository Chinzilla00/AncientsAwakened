using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Axis : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(342);
			projectile.aiStyle = 19;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axis");
        }
		
		public override void AI()
		{
			if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
			{
				projectile.ai[0] -= 2.4f;
				if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
				{
					projectile.localAI[0] = 1f;
					if (Collision.CanHit(Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height, new Vector2(projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y), projectile.width, projectile.height))
					{
						Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y, projectile.velocity.X * 2.6f, projectile.velocity.Y * 2.6f, mod.ProjectileType("AxisShot"), (int)((double)projectile.damage * 0.8), projectile.knockBack * 0.85f, projectile.owner, 0f, 0f);
					}
				}
			}
			else
			{
				projectile.ai[0] += 2.1f;
			}
		}
		
		public bool stop = false;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
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
    }
}
