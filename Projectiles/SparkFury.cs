using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SparkFury : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Spark");
		}

		public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.alpha = 255;
            projectile.penetrate = 5;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f * projectile.direction;
            projectile.alpha = 255;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 3f)
            {
                int num15 = 100;
                if (projectile.ai[0] > 20f)
                {
                    int num16 = 40;
                    float num17 = projectile.ai[0] - 20f;
                    num15 = (int)(100f * (1f - num17 / num16));
                    if (num17 >= num16)
                    {
                        projectile.Kill();
                    }
                }
                if (projectile.ai[0] <= 10f)
                {
                    num15 = (int)projectile.ai[0] * 10;
                }
                if (Main.rand.Next(100) < num15)
                {
                    int num18 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 150);
                    Main.dust[num18].position = (Main.dust[num18].position + projectile.Center) / 2f;
                    Main.dust[num18].noGravity = true;
                    Main.dust[num18].velocity *= 2f;
                    Main.dust[num18].scale *= 1.2f;
                    Main.dust[num18].velocity += projectile.velocity;
                }
            }
            if (projectile.ai[0] >= 20f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.1f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 1)
            {
                target.AddBuff(BuffID.Daybreak, 160);
            }
            else
            {
                target.AddBuff(BuffID.OnFire, 160);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}
