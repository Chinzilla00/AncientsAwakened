using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.Projectiles
{
    public class TLThunder : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunderstrike");
            Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 88;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1000;
            projectile.timeLeft = 120 * (projectile.extraUpdates + 0);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                if (projectile.oldPos[i].X == 0f && projectile.oldPos[i].Y == 0f)
                {
                    break;
                }
                projHitbox.X = (int)projectile.oldPos[i].X;
                projHitbox.Y = (int)projectile.oldPos[i].Y;
                if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return false;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter == 0 || projectile.oldPos[0] == Vector2.Zero)
            {
                for (int num31 = projectile.oldPos.Length - 1; num31 > 0; num31--)
                {
                    projectile.oldPos[num31] = projectile.oldPos[num31 - 1];
                }
                projectile.oldPos[0] = projectile.position;
                if (projectile.velocity == Vector2.Zero)
                {
                    float num32 = projectile.rotation + 1.57079637f + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                    float num33 = (float)Main.rand.NextDouble() * 1f + 1f;
                    Vector2 vector2 = new Vector2((float)Math.Cos(num32) * num33, (float)Math.Sin(num32) * num33);
                    int num34 = Dust.NewDust(projectile.oldPos[projectile.oldPos.Length - 1], 0, 0, 229, vector2.X, vector2.Y, 0);
                    Main.dust[num34].noGravity = true;
                    Main.dust[num34].scale = 1.7f;
                }
            }
            Lighting.AddLight(projectile.Center, 0.3f, 0.45f, 0.5f);
            if (projectile.velocity == Vector2.Zero)
            {
                if (projectile.frameCounter >= projectile.extraUpdates * 1)
                {
                    projectile.frameCounter = 0;
                    bool flag35 = true;
                    for (int num849 = 1; num849 < projectile.oldPos.Length; num849++)
                    {
                        if (projectile.oldPos[num849] != projectile.oldPos[0])
                        {
                            flag35 = false;
                        }
                    }
                    if (flag35)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(projectile.extraUpdates) == 1)
                {
                    for (int num850 = 0; num850 < 2; num850++)
                    {
                        float num851 = projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num852 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector84 = new Vector2((float)Math.Cos(num851) * num852, (float)Math.Sin(num851) * num852);
                        int num853 = Dust.NewDust(projectile.Center, 0, 0, 226, vector84.X, vector84.Y, 0);
                        Main.dust[num853].noGravity = true;
                        Main.dust[num853].scale = 1.2f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 value49 = projectile.velocity.RotatedBy(1.5707963705062866, default) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
                        int num854 = Dust.NewDust(projectile.Center + value49 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default, 1.5f);
                        Main.dust[num854].velocity *= 0.5f;
                        Main.dust[num854].velocity.Y = -Math.Abs(Main.dust[num854].velocity.Y);
                        return;
                    }
                }
            }
            else if (projectile.frameCounter >= projectile.extraUpdates * 1)
            {
                projectile.frameCounter = 0;
                float num855 = projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)projectile.ai[1]);
                int num856 = 0;
                Vector2 spinningpoint2 = -Vector2.UnitY;
                Vector2 vector85;
                do
                {
                    int num857 = unifiedRandom.Next();
                    projectile.ai[1] = num857;
                    num857 %= 100;
                    float f = num857 / 100f * 6.28318548f;
                    vector85 = f.ToRotationVector2();
                    if (vector85.Y > 0f)
                    {
                        vector85.Y *= -1f;
                    }
                    bool flag36 = false;
                    if (vector85.Y > -0.02f)
                    {
                        flag36 = true;
                    }
                    if (vector85.X * (projectile.extraUpdates + 1) * 2f * num855 + projectile.localAI[0] > 40f)
                    {
                        flag36 = true;
                    }
                    if (vector85.X * (projectile.extraUpdates + 1) * 2f * num855 + projectile.localAI[0] < -40f)
                    {
                        flag36 = true;
                    }
                    if (!flag36)
                    {
                        goto IL_230B7;
                    }
                }
                while (num856++ < 100);
                projectile.velocity = Vector2.Zero;
                projectile.localAI[1] = 1f;
                goto IL_230BF;
            IL_230B7:
                spinningpoint2 = vector85;
            IL_230BF:
                if (projectile.velocity != Vector2.Zero)
                {
                    projectile.localAI[0] += spinningpoint2.X * (projectile.extraUpdates + 0) * 2f * num855;
                    projectile.velocity = spinningpoint2.RotatedBy(projectile.ai[0] + 1.57079637f, default) * num855;
                    projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 end = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D tex3 = Main.extraTexture[33];
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            projectile.GetAlpha(color25);
            Vector2 scale16 = new Vector2(projectile.scale) / 2f;
            for (int num291 = 0; num291 < 3; num291++)
            {
                if (num291 == 0)
                {
                    scale16 = new Vector2(projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                }
                else if (num291 == 1)
                {
                    scale16 = new Vector2(projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int num292 = projectile.oldPos.Length - 1; num292 > 0; num292--)
                {
                    if (!(projectile.oldPos[num292] == Vector2.Zero))
                    {
                        Vector2 start = projectile.oldPos[num292] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = projectile.oldPos[num292 - 1] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (projectile.oldPos[0] != Vector2.Zero)
                {
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = projectile.oldPos[0] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }
    }
}
