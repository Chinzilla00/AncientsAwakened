using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class CLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.aiStyle = 84;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
        }
        
        public override void AI()
        {
            Vector2? vector69 = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (Main.npc[(int)projectile.ai[1]].active)
            {
                Vector2 value29 = new Vector2(27f, 59f);
                Vector2 value30 = Utils.Vector2FromElipse(Main.npc[(int)projectile.ai[1]].localAI[0].ToRotationVector2(), value29 * Main.npc[(int)projectile.ai[1]].localAI[1]);
                projectile.position = Main.npc[(int)projectile.ai[1]].Center + value30 - new Vector2((float)projectile.width, (float)projectile.height) / 2f;
            }
            else
            {
                if (!Main.projectile[(int)projectile.ai[1]].active)
                {
                    projectile.Kill();
                    return;
                }
                float num790 = (float)((int)projectile.ai[0]) - 2.5f;
                Vector2 value35 = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].velocity);
                float num791 = num790 * 0.5235988f;
                Vector2 value36 = Vector2.Zero;
                float num792;
                float y;
                float num793;
                float scaleFactor6;
                if (projectile.ai[0] < 180f)
                {
                    num792 = 1f - projectile.ai[0] / 180f;
                    y = 20f - projectile.ai[0] / 180f * 14f;
                    if (projectile.ai[0] < 120f)
                    {
                        num793 = 20f - 4f * (projectile.ai[0] / 120f);
                        projectile.Opacity = projectile.ai[0] / 120f * 0.4f;
                    }
                    else
                    {
                        num793 = 16f - 10f * ((projectile.ai[0] - 120f) / 60f);
                        projectile.Opacity = 0.4f + (projectile.ai[0] - 120f) / 60f * 0.6f;
                    }
                    scaleFactor6 = -22f + projectile.ai[0] / 180f * 20f;
                }
                else
                {
                    num792 = 0f;
                    num793 = 1.75f;
                    y = 6f;
                    projectile.Opacity = 1f;
                    scaleFactor6 = -2f;
                }
                float num794 = (projectile.ai[0] + num790 * num793) / (num793 * 6f) * 6.28318548f;
                num791 = Vector2.UnitY.RotatedBy((double)num794, default(Vector2)).Y * 0.5235988f * num792;
                value36 = (Vector2.UnitY.RotatedBy((double)num794, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                projectile.position = projectile.Center + value35 * 16f - projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)projectile.ai[1]].gfxOffY);
                projectile.position += projectile.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
                projectile.position += value36;
                projectile.velocity = Vector2.Normalize(projectile.velocity).RotatedBy((double)num791, default(Vector2));
                projectile.scale = 1.4f * (1f - num792);
                if (projectile.ai[0] >= 180f)
                {
                    projectile.damage *= 3;
                    vector69 = new Vector2?(projectile.Center);
                }
                if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 0, 0, projectile.Center, 0, 0))
                {
                    vector69 = new Vector2?(Main.player[projectile.owner].Center);
                }
                projectile.friendly = (projectile.ai[0] > 30f);
            }
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(29, (int)projectile.position.X, (int)projectile.position.Y, 104, 1f, 0f);
            }
            float num795 = 1f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 180f)
            {
                projectile.Kill();
                return;
            }
            projectile.scale = (float)Math.Sin((double)(projectile.localAI[0] * 3.14159274f / 180f)) * 10f * num795;
            if (projectile.scale > num795)
            {
                projectile.scale = num795;
            }
            float num798 = projectile.velocity.ToRotation();
            num798 += projectile.ai[0];
            projectile.rotation = num798 - 1.57079637f;
            projectile.velocity = num798.ToRotationVector2();
            float num799 = 3f;
            float num800 = projectile.width;
            Vector2 samplingPoint = projectile.Center;
            if (vector69.HasValue)
            {
                samplingPoint = vector69.Value;
            }
            
            float[] array3 = new float[(int)num799];
            Collision.LaserScan(samplingPoint, projectile.velocity, num800 * projectile.scale, 2400f, array3);
            float num801 = 0f;
            for (int num802 = 0; num802 < array3.Length; num802++)
            {
                num801 += array3[num802];
            }
            num801 /= num799;
            float amount = 0.5f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num801, amount);
            Vector2 vector70 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (int num803 = 0; num803 < 2; num803++)
            {
                float num804 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num805 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector71 = new Vector2((float)Math.Cos((double)num804) * num805, (float)Math.Sin((double)num804) * num805);
                int num806 = Dust.NewDust(vector70, 0, 0, 229, vector71.X, vector71.Y, 0, default(Color), 1f);
                Main.dust[num806].noGravity = true;
                Main.dust[num806].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value37 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                int num807 = Dust.NewDust(vector70 + value37 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num807].velocity *= 0.5f;
                Main.dust[num807].velocity.Y = -Math.Abs(Main.dust[num807].velocity.Y);
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture2D18 = Main.projectileTexture[projectile.type];
            Texture2D texture2D19 = mod.GetTexture("NPCs/Bosses/SoC/CLaserTex");
            Texture2D texture2D20 = mod.GetTexture("NPCs/Bosses/SoC/CLaserHead");
            float num224 = projectile.localAI[1];
            Microsoft.Xna.Framework.Color color44 = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 0.9f;
            Main.spriteBatch.Draw(texture2D18, projectile.Center - Main.screenPosition, null, color44, projectile.rotation, texture2D18.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
            num224 -= (float)(texture2D18.Height / 2 + texture2D20.Height) * projectile.scale;
            Vector2 value21 = projectile.Center;
            value21 += projectile.velocity * projectile.scale * (float)texture2D18.Height / 2f;
            if (num224 > 0f)
            {
                float num225 = 0f;
                Rectangle value22 = new Microsoft.Xna.Framework.Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), texture2D19.Width, 16);
                while (num225 + 1f < num224)
                {
                    if (num224 - num225 < (float)value22.Height)
                    {
                        value22.Height = (int)(num224 - num225);
                    }
                    Main.spriteBatch.Draw(texture2D19, value21 - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(value22), color44, projectile.rotation, new Vector2((float)(value22.Width / 2), 0f), projectile.scale, SpriteEffects.None, 0f);
                    num225 += (float)value22.Height * projectile.scale;
                    value21 += projectile.velocity * (float)value22.Height * projectile.scale;
                    value22.Y += 16;
                    if (value22.Y + value22.Height > texture2D19.Height)
                    {
                        value22.Y = 0;
                    }
                }
            }
            Main.spriteBatch.Draw(texture2D20, value21 - Main.screenPosition, null, color44, projectile.rotation, texture2D20.Frame(1, 1, 0, 0).Top(), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
