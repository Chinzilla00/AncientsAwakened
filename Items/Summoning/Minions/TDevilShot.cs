using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class TDevilShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Trident");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 27;
            projectile.minion = true;
            projectile.penetrate = 3;
            projectile.light = 0.5f;
            projectile.friendly = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            return false;
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] < 30f)
            {
                projectile.velocity *= 1.125f;
            }
            if (projectile.localAI[1] < 5f)
            {
                projectile.localAI[1] = 5f;
                for (int num303 = 5; num303 < 25; num303++)
                {
                    float num304 = projectile.velocity.X * (30f / (float)num303);
                    float num305 = projectile.velocity.Y * (30f / (float)num303);
                    num304 *= 80f;
                    num305 *= 80f;
                    int num306 = Dust.NewDust(new Vector2(projectile.position.X - num304, projectile.position.Y - num305), 8, 8, 107, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 0.9f);
                    Main.dust[num306].velocity *= 0.25f;
                    Main.dust[num306].velocity -= projectile.velocity * 5f;
                }
            }
            
            if (projectile.localAI[1] < 15f)
            {
                projectile.localAI[1] += 1f;
            }
            else
            {
                int num310 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 4f), 8, 8, 107, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 0.6f);
                Main.dust[num310].velocity *= -0.25f;
                if (projectile.localAI[0] == 0f)
                {
                    projectile.scale -= 0.02f;
                    projectile.alpha += 30;
                    if (projectile.alpha >= 250)
                    {
                        projectile.alpha = 255;
                        projectile.localAI[0] = 1f;
                    }
                }
                else if (projectile.localAI[0] == 1f)
                {
                    projectile.scale += 0.02f;
                    projectile.alpha -= 30;
                    if (projectile.alpha <= 0)
                    {
                        projectile.alpha = 0;
                        projectile.localAI[0] = 0f;
                    }
                }
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                if (projectile.type == 132)
                {
                    Main.PlaySound(SoundID.Item60, projectile.position);
                }
                else
                {
                    Main.PlaySound(SoundID.Item8, projectile.position);
                }
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num585 = 0; num585 < 20; num585++)
            {
                int num586 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 66, 0f, 0f, 100, Main.DiscoColor, 2f);
                Main.dust[num586].noGravity = true;
                Main.dust[num586].velocity *= 4f;
            }
        }
    }
}