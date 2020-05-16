using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

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

        float shoot = 0;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (projectile.ai[0] < 0f)
            {
                projectile.ai[0] += 1f;

                projectile.ai[1] -= projectile.direction * 0.3926991f / 50f;
            }

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 5;
            }
            else
            {
                projectile.alpha = 0;
            }

            if (projectile.scale < 1)
            {
                projectile.scale += .019f;
            }
            else
            {
                projectile.scale = 1;
            }

            float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            bool flag24 = false;
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] += 1f;
                projectile.extraUpdates = 1;
                if (projectile.ai[1] > 40f)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }
            if (flag24)
            {
                return;
            }
            Vector2 vector46 = projectile.position;
            bool flag25 = false;
            if (projectile.ai[0] != 1f)
            {
                projectile.tileCollide = false;
            }
            if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }
            for (int num645 = 0; num645 < 200; num645++)
            {
                NPC nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        vector46 = nPC2.Center;
                        flag25 = true;
                    }
                }
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num647)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            if (flag25 && projectile.ai[0] == 0f)
            {
                Vector2 vector47 = vector46 - projectile.Center;
                float num648 = vector47.Length();
                vector47.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 8f;
                    vector47 *= scaleFactor2;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    vector47 *= -num649;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = projectile.ai[0] == 1f;
                }
                float num650 = 5f; //6
                if (flag26)
                {
                    num650 = 12f; //15
                }
                Vector2 center2 = projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 6.5f) //200 and 8
                {
                    num650 = 6.5f; //8
                }
                if (num651 < num636 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
                    projectile.netUpdate = true;
                }
                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    projectile.velocity = (projectile.velocity * 40f + vector48) / 41f;
                }
                else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.2f;
                    projectile.velocity.Y = -0.1f;
                }
            }
            shoot += 1f;
            if (shoot % 30f == 0f && shoot < 180f && Main.netMode != NetmodeID.MultiplayerClient)
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
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<AthenaShockF>(), projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(projectile.Center, 0f, 0.85f, 0.9f);
            if (projectile.alpha < 150 && shoot < 180f)
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

            projectile.velocity = Vector2.Zero;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
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
            Texture2D tex = mod.GetTexture("Projectiles/Athena/OwlRuneCharged");
            Rectangle SunFrame = new Rectangle(projectile.frame, 0, tex.Width, tex.Height / 4);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position + new Vector2(0, projectile.gfxOffY), projectile.width, projectile.height, projectile.scale, 0, projectile.spriteDirection, 4, SunFrame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}