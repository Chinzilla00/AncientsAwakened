
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class OdinsBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = -1;
            projectile.ranged = true;
            projectile.penetrate = 3;
            projectile.light = 0.5f;
            projectile.friendly = true;
            projectile.extraUpdates = 1;
            projectile.scale *= .8f;
        }

        public override void AI()
        {
            if (projectile.localAI[1] > 7f)
            {
                int num309 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default, 1.25f);
                Main.dust[num309].velocity *= -0.25f;
                num309 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default, 1.25f);
                Main.dust[num309].velocity *= -0.25f;
                Main.dust[num309].position -= projectile.velocity * 0.5f;
            }

            AIThrownWeapon(projectile, ref projectile.ai, false, 40);

            projectile.ai[1]++;

            if (projectile.ai[0] % 5 == 0)
            {
                int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AxisSnow"), projectile.damage, projectile.knockBack * 0.55f, projectile.owner, 0f, Main.rand.Next(3));
                Main.projectile[p].melee = false;
                Main.projectile[p].ranged = true;
                projectile.netUpdate = true;
            }

            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }

        public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
        {
            p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * p.direction;
            ai[0] += 1f;
            if (ai[0] >= timeUntilDrop)
            {
                p.velocity.Y += yIncrement;
                p.velocity.X *= xScalar;
            }
            else
            if (!spin) { p.rotation = (float)Math.Atan2(p.velocity.Y, p.velocity.X) + 2.355f; }
            if (p.velocity.Y > maxSpeedY) { p.velocity.Y = maxSpeedY; }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num794 = 4; num794 < 31; num794++)
            {
                float num795 = projectile.oldVelocity.X * (30f / num794);
                float num796 = projectile.oldVelocity.Y * (30f / num794);
                int num797 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num795, projectile.oldPosition.Y - num796), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 27, default, 1.8f);
                Main.dust[num797].noGravity = true;
                Main.dust[num797].velocity *= 0.5f;
                num797 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num795, projectile.oldPosition.Y - num796), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), projectile.oldVelocity.X, projectile.oldVelocity.Y, 27, default, 1.4f);
                Main.dust[num797].velocity *= 0.05f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, .5f, 1f, 10, false, 0f, 0f, new Color(35, 23, 87));
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, Color.White, false);
            return false;
        }
    }
}