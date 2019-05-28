using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class VoidBreaker : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Breakser");

            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.aiStyle = 71;
            projectile.alpha = 255;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 2;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            projectile.friendly = false;
        }
		
        public override void AI()
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 10f && Main.rand.Next(3) == 0)
            {
                int num693 = 6;
                for (int num694 = 0; num694 < num693; num694++)
                {
                    Vector2 vector56 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width, (float)projectile.height) / 2f;
                    vector56 = vector56.RotatedBy((double)(num694 - (num693 / 2 - 1)) * 3.1415926535897931 / (double)((float)num693), default(Vector2)) + projectile.Center;
                    Vector2 value24 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num695 = Dust.NewDust(vector56 + value24, 0, 0, 217, value24.X * 2f, value24.Y * 2f, mod.DustType<Dusts.VoidDust>(), default(Color), 1.4f);
                    Main.dust[num695].noGravity = true;
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
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.6f, 0.1f, 0.1f);
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
                    Player player = Main.player[num698];
                    if ((projectile.ai[0] == 0f || projectile.ai[0] == (float)(num698 + 1)))
                    {
                        Vector2 center4 = player.Center;
                        float num699 = Vector2.Distance(center4, vector57);
                        if (num699 < num697 && Collision.CanHit(projectile.position, projectile.width, projectile.height, player.position, player.width, player.height))
                        {
                            num697 = num699;
                            vector57 = center4;
                            num696 = num698;
                        }
                    }
                }
                if (num696 >= 0)
                {
                    projectile.ai[0] = (num696 + 1);
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
                if (Main.player[num700].active)
                {
                    float num701 = Main.player[num700].position.X + (float)(Main.player[num700].width / 2);
                    float num702 = Main.player[num700].position.Y + (float)(Main.player[num700].height / 2);
                    float num703 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num701) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num702);
                    if (num703 < 1000f)
                    {
                        flag31 = true;
                        vector57 = Main.player[num700].Center;
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
                double num706 = (double)(num705 - num704);
                if (num706 > 3.1415926535897931)
                {
                    num706 -= 6.2831853071795862;
                }
                if (num706 < -3.1415926535897931)
                {
                    num706 += 6.2831853071795862;
                }
                projectile.velocity = projectile.velocity.RotatedBy(num706 * 0.10000000149011612, default(Vector2));
            }
            float num707 = projectile.velocity.Length();
            projectile.velocity.Normalize();
            projectile.velocity *= num707 + 0.0025f;
        }
		
    }
}