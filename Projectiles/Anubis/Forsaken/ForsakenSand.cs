using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
	public class ForsakenSand : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults() 
        {
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
		}

		public override void AI() 
        {
            if (projectile.frameCounter++ > 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
			if (projectile.timeLeft >= 119) projectile.ai[0] += 0.1f;
			if (projectile.timeLeft >= 115) projectile.friendly = false;
			if (projectile.timeLeft <= 114) projectile.friendly = true;
			projectile.velocity.Y += projectile.ai[0];
			if (Main.rand.NextBool(2)) 
            {
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Sandnado, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			projectile.penetrate--;
			if (projectile.penetrate <= 0) 
            {
				projectile.Kill();
			}
			else 
            {
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X) {
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y) {
					projectile.velocity.Y = -oldVelocity.Y;
				}
				projectile.velocity *= 0.9f;
				Main.PlaySound(SoundID.Item10, projectile.position);
			}
			return false;
		}

		public override void Kill(int timeLeft) 
        {
			for (int k = 0; k < 5; k++) 
            {
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Sandnado, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
            Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<ForsakenBoom>(), projectile.damage, projectile.knockBack * 2, Main.myPlayer);
			Main.PlaySound(SoundID.Item25, projectile.position);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			projectile.ai[0] += 0.1f;
			projectile.Kill();
		}
	}
}