using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Projectiles.Athena
{
    public class OwlRuneCharged : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.sentry = true;
            projectile.scale = .001f;
            projectile.alpha = 255;
        }
        

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frame += 1;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.localAI[1] == 0f)
            {
                Main.PlaySound(SoundID.Item121, projectile.position);
                projectile.localAI[1] = 1f;
            }
            if (projectile.ai[0] < 180f)
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 180f && Main.netMode != 1)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < Main.maxNPCs; num840++)
                {
                    if (Main.npc[num840].active)
                    {
                        Vector2 center9 = Main.npc[num840].Center;
                        float num841 = Vector2.Distance(center9, projectile.Center);
                        if (num841 < num839 && Collision.CanHit(projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - projectile.Center;
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 10f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector83.X, vector83.Y, Terraria.ModLoader.ModContent.ProjectileType<AthenaShockF>(), projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(projectile.Center, 0f, 0.85f, 0.9f);
            if (projectile.alpha < 150 && projectile.ai[0] < 180f)
            {
                for (int num843 = 0; num843 < 1; num843++)
                {
                    float num844 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num844 < -0.5f)
                    {
                        num844 = -0.5f;
                    }
                    if (num844 > 0.5f)
                    {
                        num844 = 0.5f;
                    }
                    Vector2 value47 = new Vector2(-projectile.width * 0.2f * projectile.scale, 0f).RotatedBy(num844 * 6.28318548f, default).RotatedBy(projectile.velocity.ToRotation(), default);
                    int num845 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num845].position = projectile.Center + value47;
                    Main.dust[num845].velocity = Vector2.Normalize(Main.dust[num845].position - projectile.Center) * 2f;
                    Main.dust[num845].noGravity = true;
                }
                for (int num846 = 0; num846 < 1; num846++)
                {
                    float num847 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num847 < -0.5f)
                    {
                        num847 = -0.5f;
                    }
                    if (num847 > 0.5f)
                    {
                        num847 = 0.5f;
                    }
                    Vector2 value48 = new Vector2(-projectile.width * 0.6f * projectile.scale, 0f).RotatedBy(num847 * 6.28318548f, default).RotatedBy(projectile.velocity.ToRotation(), default);
                    int num848 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num848].velocity = Vector2.Zero;
                    Main.dust[num848].position = projectile.Center + value48;
                    Main.dust[num848].noGravity = true;
                }
                return;
            }
        }

        public float Rotation = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = mod.GetTexture("Projectiles/Athena/OwlRune");
            Rectangle SunFrame = new Rectangle(0, 0, tex.Width, tex.Height);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position + new Vector2(0, projectile.gfxOffY), projectile.width, projectile.height, projectile.scale, 0, projectile.spriteDirection, 1, SunFrame, projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}