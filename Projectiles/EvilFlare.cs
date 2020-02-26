using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class EvilFlare : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare of Evil");
            Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.8f;
            projectile.alpha = 100;
            projectile.melee = true;
            projectile.penetrate = 2;
        }

        public override bool OnTileCollide(Vector2 velocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.ai[0] += 1f;
            if ((projectile.ai[0] >= 5f && projectile.type != 253) || (projectile.type == 253 && projectile.ai[0] >= 8f))
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
            else
            {
                if (projectile.type == 15 && projectile.velocity.Y > 4f)
                {
                    if (projectile.velocity.Y != velocity.Y)
                    {
                        projectile.velocity.Y = -velocity.Y * 0.8f;
                    }
                }
                else if (projectile.velocity.Y != velocity.Y)
                {
                    projectile.velocity.Y = -velocity.Y;
                }
                if (projectile.velocity.X != velocity.X)
                {
                    projectile.velocity.X = -velocity.X;
                }
            }
            return false;
        }

        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0];
            int DustType = projectile.ai[0] == 1 ? DustID.GoldFlame : 75;
            int num102 = Dust.NewDust(new Vector2(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height, DustType, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 3f * projectile.scale);
            Main.dust[num102].noGravity = true;
            projectile.ai[1] += 1f;

			if (projectile.ai[1] >= 20f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}

            projectile.rotation += 0.3f * projectile.direction;

            if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
				return;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(projectile.ai[0] == 1 ? BuffID.Ichor : BuffID.CursedInferno, 420, false);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            int DustType = projectile.ai[0] == 1 ? DustID.GoldFlame : 75;
            for (int num583 = 0; num583 < 20; num583++)
            {
                int num584 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustType, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default, 2f * projectile.scale);
                Main.dust[num584].noGravity = true;
                Main.dust[num584].velocity *= 2f;
                num584 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustType, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default, 1f * projectile.scale);
                Main.dust[num584].velocity *= 2f;
            }
        }
    }
}
