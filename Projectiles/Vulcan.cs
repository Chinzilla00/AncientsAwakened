using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Vulcan : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare of Evil");
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
            Main.PlaySound(SoundID.Item14, projectile.position);
            projectile.Kill();
            return false;
        }

        public override void AI()
        {
            int num102 = Dust.NewDust(new Vector2(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height, DustID.Fire, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 3f * projectile.scale);
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
            target.AddBuff(BuffID.OnFire, 420, false);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("VulcanExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            int DustType = DustID.Fire;
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
