using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AAMod.Projectiles.Athena
{
	public class OwlRune : ModProjectile
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
            Player player = Main.player[projectile.owner];
            projectile.velocity = Vector2.Zero;

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
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 30f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                int num658 = mod.ProjectileType<Feather>();
                if (flag25 && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
                    {
                        Vector2 value19 = vector46 - projectile.Center;
                        value19.Normalize();
                        value19 *= 8;
                        Vector2 perturbedSpeed = value19.RotatedByRandom(MathHelper.ToRadians(10));
                        int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, num658, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].timeLeft = 300;
                        projectile.netUpdate = true;
                    }
                }
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