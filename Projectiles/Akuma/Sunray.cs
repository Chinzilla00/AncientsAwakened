using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class Sunray : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Portal");    //The recording mode
		}

		public override void SetDefaults()
		{
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float n = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 30f * projectile.scale, ref n))
                return true;

            return false;
        }
        
        public override bool PreAI()
        {
            projectile.Center = Main.projectile[(int)projectile.ai[1]].Center;
            projectile.velocity = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].ai[1].ToRotationVector2());
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            float num796 = 1f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 50f)
            {
                projectile.Kill();
            }
            projectile.scale = (float)Math.Sin((double)(projectile.localAI[0] * 3.14159274f / 50f)) * 10f * num796;
            if (projectile.scale > num796)
            {
                projectile.scale = num796;
            }
            float num798 = projectile.velocity.ToRotation();
            projectile.rotation = num798 - 1.57079637f;
            projectile.velocity = num798.ToRotationVector2();
            float num799 = 0f;
            float num800 = 0f;
            Vector2 samplingPoint = projectile.Center;
            num799 = 2f;
            num800 = 0f;
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
            Vector2 vector72 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (int num808 = 0; num808 < 2; num808++)
            {
                float num809 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num810 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector73 = new Vector2((float)Math.Cos((double)num809) * num810, (float)Math.Sin((double)num809) * num810);
                int num811 = Dust.NewDust(vector72, 0, 0, 229, vector73.X, vector73.Y, 0, default(Color), 1f);
                Main.dust[num811].noGravity = true;
                Main.dust[num811].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value38 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                int num812 = Dust.NewDust(vector72 + value38 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num812].velocity *= 0.5f;
                Main.dust[num812].velocity.Y = -Math.Abs(Main.dust[num812].velocity.Y);
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
                return false;

            Texture2D tex2 = Main.projectileTexture[projectile.type];
            float num210 = projectile.localAI[1];
            Color c_ = new Color(255, 255, 255, 127);
            Vector2 value20 = projectile.Center.Floor();
            num210 -= projectile.scale * 10.5f;
            Vector2 vector41 = new Vector2(projectile.scale);
            DelegateMethods.f_1 = 1f;
            DelegateMethods.c_1 = c_;
            DelegateMethods.i_1 = 54000 - (int)Main.time / 2;
            Vector2 vector42 = projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Utils.DrawLaser(Main.spriteBatch, tex2, value20 - Main.screenPosition, value20 + projectile.velocity * num210 - Main.screenPosition, vector41, new Utils.LaserLineFraming(DelegateMethods.TurretLaserDraw));
            DelegateMethods.c_1 = new Color(255, 255, 255, 127) * 0.75f * projectile.Opacity;
            Utils.DrawLaser(Main.spriteBatch, tex2, value20 - Main.screenPosition, value20 + projectile.velocity * num210 - Main.screenPosition, vector41 / 2f, new Utils.LaserLineFraming(DelegateMethods.TurretLaserDraw));
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

    }
}
