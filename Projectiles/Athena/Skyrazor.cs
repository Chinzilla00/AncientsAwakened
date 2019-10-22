using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Athena
{
    public class Skyrazor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.melee = true;
            projectile.ignoreWater = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 10f && Main.rand.Next(3) == 0)
            {
                for (int num694 = 0; num694 < 3; num694++)
                {
                    Vector2 vector56 = Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width, projectile.height) / 2f;
                    vector56 = vector56.RotatedBy((num694 - (3 / 2 - 1)) * 3.1415926535897931 / 3f, default) + projectile.Center;
                    Vector2 value24 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    int num695 = Dust.NewDust(vector56 + value24, 0, 0, 217, value24.X * 2f, value24.Y * 2f, DustID.Electric, default, 1.4f);
                    Main.dust[num695].noLight = true;
                    Main.dust[num695].velocity /= 4f;
                    Main.dust[num695].velocity -= projectile.velocity;
                }
                projectile.alpha -= 5;
                if (projectile.alpha < 50)
                {
                    projectile.alpha = 50;
                }
                projectile.rotation += projectile.velocity.X * 0.1f;
                projectile.frame = (int)(projectile.localAI[1] / 3f) % 3;
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.1f, 0.4f, 0.6f);
            }
            int num696 = -1;
            Vector2 vector57 = projectile.Center;
            float num697 = 500f;
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }
            if (projectile.ai[0] == 0f && projectile.localAI[0] == 0f)
            {
                for (int num698 = 0; num698 < 200; num698++)
                {
                    NPC nPC6 = Main.npc[num698];
                    if (nPC6.CanBeChasedBy(this, false) && (projectile.ai[0] == 0f || projectile.ai[0] == num698 + 1))
                    {
                        Vector2 center4 = nPC6.Center;
                        float num699 = Vector2.Distance(center4, vector57);
                        if (num699 < num697 && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC6.position, nPC6.width, nPC6.height))
                        {
                            num697 = num699;
                            vector57 = center4;
                            num696 = num698;
                        }
                    }
                }
                if (num696 >= 0)
                {
                    projectile.ai[0] = num696 + 1;
                    projectile.netUpdate = true;
                }
            }
            if (projectile.localAI[0] == 0f && projectile.ai[0] == 0f)
            {
                projectile.localAI[0] = 30f;
            }
            bool flag31 = false;
            if (projectile.ai[0] != 0f)
            {
                int num700 = (int)(projectile.ai[0] - 1f);
                if (Main.npc[num700].active && !Main.npc[num700].dontTakeDamage && Main.npc[num700].immune[projectile.owner] == 0)
                {
                    float num701 = Main.npc[num700].position.X + Main.npc[num700].width / 2;
                    float num702 = Main.npc[num700].position.Y + Main.npc[num700].height / 2;
                    float num703 = Math.Abs(projectile.position.X + projectile.width / 2 - num701) + Math.Abs(projectile.position.Y + projectile.height / 2 - num702);
                    if (num703 < 1000f)
                    {
                        flag31 = true;
                        vector57 = Main.npc[num700].Center;
                    }
                }
                else
                {
                    projectile.ai[0] = 0f;
                    flag31 = false;
                    projectile.netUpdate = true;
                }
            }
            if (flag31)
            {
                Vector2 v = vector57 - projectile.Center;
                float num704 = projectile.velocity.ToRotation();
                float num705 = v.ToRotation();
                double num706 = num705 - num704;
                if (num706 > 3.1415926535897931)
                {
                    num706 -= 6.2831853071795862;
                }
                if (num706 < -3.1415926535897931)
                {
                    num706 += 6.2831853071795862;
                }
                projectile.velocity = projectile.velocity.RotatedBy(num706 * 0.10000000149011612, default);
            }
            float num707 = projectile.velocity.Length();
            projectile.velocity.Normalize();
            projectile.velocity *= num707 + 0.0025f; 
            
            for (int u = 0; u < Main.maxNPCs; u++)
            {
                NPC target = Main.npc[u];

                if (target.type != NPCID.TargetDummy && target.active && !target.boss && target.chaseable && target.chaseable && Vector2.Distance(projectile.Center, target.Center) < 200)
                {
                    float num3 = 6f;
                    Vector2 vector = new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2);
                    float num4 = projectile.Bottom.X - vector.X;
                    float num5 = projectile.Bottom.Y - vector.Y;
                    float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                    num6 = num3 / num6;
                    num4 *= num6;
                    num5 *= num6;
                    int num7 = 6;
                    target.velocity.X = (target.velocity.X * (num7 - 1) + num4) / num7;
                    target.velocity.Y = (target.velocity.Y * (num7 - 1) + num5) / num7;
                    target.velocity *= target.knockBackResist;
                }
            }
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -1f;
            }
            return false;
        }
    }
}