
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class DemiseBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.light = 0.5f;
            projectile.friendly = true;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {

            int num309 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, DustID.Shadowflame, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default, 1.25f);
            Main.dust[num309].velocity *= -0.25f;
            num309 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X * 4f + 2f, projectile.position.Y + 2f - projectile.velocity.Y * 4f), 8, 8, DustID.Shadowflame, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default, 1.25f);
            Main.dust[num309].velocity *= -0.25f;
            Main.dust[num309].position -= projectile.velocity * 0.5f;

            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item60, projectile.position);
            }

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;

            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num794 = 4; num794 < 31; num794++)
            {
                float num795 = projectile.oldVelocity.X * (30f / num794);
                float num796 = projectile.oldVelocity.Y * (30f / num794);
                int num797 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num795, projectile.oldPosition.Y - num796), 8, 8, DustID.Shadowflame, projectile.oldVelocity.X, projectile.oldVelocity.Y, DustID.Shadowflame, default, 1.8f);
                Main.dust[num797].noGravity = true;
                Main.dust[num797].velocity *= 0.5f;
                num797 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num795, projectile.oldPosition.Y - num796), 8, 8, DustID.Shadowflame, projectile.oldVelocity.X, projectile.oldVelocity.Y, DustID.Shadowflame, default, 1.4f);
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