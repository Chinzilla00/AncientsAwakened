using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SoulSiphon : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Siphon");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 2;
            projectile.melee = true;
            projectile.ignoreWater = true;
        }

        bool hit = false;

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 10f && Main.rand.Next(3) == 0)
            {
                int num698 = 6;
                int num3;
                for (int num699 = 0; num699 < num698; num699 = num3 + 1)
                {
                    Vector2 vector63 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width, (float)projectile.height) / 2f;
                    vector63 = vector63.RotatedBy((double)(num699 - (num698 / 2 - 1)) * 3.1415926535897931 / (double)((float)num698), default(Vector2)) + projectile.Center;
                    Vector2 vector64 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num700 = Dust.NewDust(vector63 + vector64, 0, 0, 191, vector64.X * 2f, vector64.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num700].noGravity = true;
                    Main.dust[num700].noLight = true;
                    Dust dust3 = Main.dust[num700];
                    dust3.velocity /= 4f;
                    dust3 = Main.dust[num700];
                    dust3.velocity -= projectile.velocity;
                    num3 = num699;
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
            int num701 = -1;
            Vector2 vector65 = projectile.Center;
            float num702 = 500f;
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }
            if (projectile.ai[0] == 0f && projectile.localAI[0] == 0f)
            {
                int num3;
                for (int num703 = 0; num703 < 200; num703 = num3 + 1)
                {
                    NPC nPC6 = Main.npc[num703];
                    if (nPC6.CanBeChasedBy(projectile, false) && (projectile.ai[0] == 0f || projectile.ai[0] == (float)(num703 + 1)))
                    {
                        Vector2 center4 = nPC6.Center;
                        float num704 = Vector2.Distance(center4, vector65);
                        if (num704 < num702 && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC6.position, nPC6.width, nPC6.height))
                        {
                            num702 = num704;
                            vector65 = center4;
                            num701 = num703;
                        }
                    }
                    num3 = num703;
                }
                if (num701 >= 0)
                {
                    projectile.ai[0] = (float)(num701 + 1);
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
                int num705 = (int)(projectile.ai[0] - 1f);
                if (Main.npc[num705].active && !Main.npc[num705].dontTakeDamage && Main.npc[num705].immune[projectile.owner] == 0)
                {
                    float num706 = Main.npc[num705].position.X + (float)(Main.npc[num705].width / 2);
                    float num707 = Main.npc[num705].position.Y + (float)(Main.npc[num705].height / 2);
                    float num708 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num706) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num707);
                    if (num708 < 1000f)
                    {
                        flag31 = true;
                        vector65 = Main.npc[num705].Center;
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
                Vector2 v = vector65 - projectile.Center;
                float num709 = projectile.velocity.ToRotation();
                float num710 = v.ToRotation();
                double num711 = (double)(num710 - num709);
                if (num711 > 3.1415926535897931)
                {
                    num711 -= 6.2831853071795862;
                }
                if (num711 < -3.1415926535897931)
                {
                    num711 += 6.2831853071795862;
                }
                projectile.velocity = projectile.velocity.RotatedBy(num711 * 0.10000000149011612, default(Vector2));
            }
            float num712 = projectile.velocity.Length();
            projectile.velocity.Normalize();
            projectile.velocity *= num712 + 0.0025f;
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }
            float Heal = damage * 0.075f;
            if ((int)Heal == 0)
            {
                return;
            }
            if (Main.player[Main.myPlayer].lifeSteal <= 0f)
            {
                return;
            }
            Main.player[Main.myPlayer].lifeSteal -= Heal;
            int num2 = projectile.owner;
            Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, mod.ProjectileType("SoulSiphonHeal"), 0, 0f, projectile.owner, (float)num2, Heal);
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