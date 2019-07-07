using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    public class Star1 : ModProjectile
	{
        public override void SetDefaults()
		{
            projectile.width = 26;
            projectile.height = 26;
            projectile.alpha = 30;
            projectile.light = 0.2f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            projectile.rotation += .1f;
            int stardust = mod.DustType<Dusts.StarDust>();
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, stardust, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, stardust, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int n = 0; n < 3; n++)
            {
                float x = projectile.position.X + Main.rand.Next(-400, 400);
                float y = projectile.position.Y - Main.rand.Next(500, 800);
                Vector2 vector = new Vector2(x, y);
                float num13 = projectile.position.X + (projectile.width / 2) - vector.X;
                float num14 = projectile.position.Y + (projectile.height / 2) - vector.Y;
                num13 += Main.rand.Next(-100, 101);
                int num15 = 23;
                float num16 = (float)Math.Sqrt(num13 * num13 + num14 * num14);
                num16 = num15 / num16;
                num13 *= num16;
                num14 *= num16;
                int num17 = Projectile.NewProjectile(x, y, num13, num14, mod.ProjectileType<Stars>(), 70, 5f, Main.myPlayer, 0f, 0f);
                Main.projectile[num17].ai[1] = projectile.position.Y;
            }
        }

        public override void Kill(int timeleft)
        {
            int stardust = mod.DustType("StarDust");
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, stardust, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, stardust, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
