using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah

{
    public abstract class RajahRocket : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rocket");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.scale = 0.9f;
        }

        public override void AI()
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                projectile.ai[1] = 0f;
                projectile.alpha = 255;
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 200;
                projectile.height = 200;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.knockBack = 10f;
            }
            else
            {
                projectile.damage = 0;
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] > 6f)
                {
                    projectile.alpha = 0;
                }
                else
                {
                    projectile.alpha = (int)(255f - 42f * projectile.localAI[1]) + 100;
                    if (projectile.alpha > 255)
                    {
                        projectile.alpha = 255;
                    }
                }
                for (int num230 = 0; num230 < 2; num230++)
                {
                    float num231 = 0f;
                    float num232 = 0f;
                    if (num230 == 1)
                    {
                        num231 = projectile.velocity.X * 0.5f;
                        num232 = projectile.velocity.Y * 0.5f;
                    }
                    if (projectile.localAI[1] > 9f)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            int num233 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num231, projectile.position.Y + 3f + num232) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                            Main.dust[num233].scale *= 1.4f + (float)Main.rand.Next(10) * 0.1f;
                            Main.dust[num233].velocity *= 0.2f;
                            Main.dust[num233].noGravity = true;
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            int num234 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num231, projectile.position.Y + 3f + num232) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, mod.DustType<Dusts.CarrotDust>(), 0f, 0f, 100, default(Color), 0.5f);
                            Main.dust[num234].fadeIn = 0.5f + (float)Main.rand.Next(5) * 0.1f;
                            Main.dust[num234].velocity *= 0.05f;
                        }
                    }
                }
                float num235 = projectile.position.X;
                float num236 = projectile.position.Y;
                float num237 = 600f;
                bool flag5 = false;
                projectile.ai[0] += 1f;
                if (projectile.ai[0] > 30f)
                {
                    projectile.ai[0] = 30f;
                    int num238 = BaseAI.GetPlayer(projectile.Center, 1000);
                    float num239 = Main.player[num238].position.X + (Main.player[num238].width / 2);
                    float num240 = Main.player[num238].position.Y + (Main.player[num238].height / 2);
                    float num241 = Math.Abs(projectile.position.X + (projectile.width / 2) - num239) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num240);
                    if (num241 < num237 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.player[num238].position, Main.player[num238].width, Main.player[num238].height))
                    {
                        num235 = num239;
                        num236 = num240;
                        flag5 = true;
                    }
                }
                if (!flag5)
                {
                    num235 = projectile.position.X + (projectile.width / 2) + projectile.velocity.X * 100f;
                    num236 = projectile.position.Y + (projectile.height / 2) + projectile.velocity.Y * 100f;
                }
                float num242 = 16f;
                Vector2 vector20 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num243 = num235 - vector20.X;
                float num244 = num236 - vector20.Y;
                float num245 = (float)Math.Sqrt((num243 * num243 + num244 * num244));
                num245 = num242 / num245;
                num243 *= num245;
                num244 *= num245;
                projectile.velocity.X = (projectile.velocity.X * 11f + num243) / 12f;
                projectile.velocity.Y = (projectile.velocity.Y * 11f + num244) / 12f;
            }
            projectile.ai[0] += 1f;
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X) - 1.57f;
            }
            else
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            }
        }
    }
}
