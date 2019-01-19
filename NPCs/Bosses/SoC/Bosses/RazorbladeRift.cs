using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    class RazorbladeRift : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] =4;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.penetrate = -1;
            npc.aiStyle = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (projectile.ai[1] > 0f)
            {
                int num611 = (int)projectile.ai[1] - 1;
                if (num611 < 255)
                {
                    projectile.localAI[0] += 1f;
                    if (projectile.localAI[0] > 10f)
                    {
                        int num612 = 6;
                        for (int num613 = 0; num613 < num612; num613++)
                        {
                            Vector2 vector43 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f;
                            vector43 = vector43.RotatedBy((double)(num613 - (num612 / 2 - 1)) * 3.1415926535897931 / (double)((float)num612), default(Vector2)) + projectile.Center;
                            Vector2 value15 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                            int num614 = Dust.NewDust(vector43 + value15, 0, 0, mod.DustType<Dusts.CthulhuDust>(), value15.X * 2f, value15.Y * 2f, 100, default(Color), 1.4f);
                            Main.dust[num614].noGravity = true;
                            Main.dust[num614].noLight = true;
                            Main.dust[num614].velocity /= 4f;
                            Main.dust[num614].velocity -= projectile.velocity;
                        }
                        projectile.alpha -= 5;
                        if (projectile.alpha < 100)
                        {
                            projectile.alpha = 100;
                        }
                        projectile.rotation += projectile.velocity.X * 0.1f;
                        projectile.frame = (int)(projectile.localAI[0] / 3f) % 3;
                    }
                    Vector2 value16 = Main.player[num611].Center - projectile.Center;
                    float num615 = 4f;
                    num615 += projectile.localAI[0] / 20f;
                    projectile.velocity = Vector2.Normalize(value16) * num615;
                    if (value16.Length() < 50f)
                    {
                        projectile.Kill();
                    }
                }
            }
            else
            {
                float num616 = 0.209439516f;
                float num617 = 4f;
                float num618 = (float)(Math.Cos((double)(num616 * projectile.ai[0])) - 0.5) * num617;
                projectile.velocity.Y = projectile.velocity.Y - num618;
                projectile.ai[0] += 1f;
                num618 = (float)(Math.Cos((double)(num616 * projectile.ai[0])) - 0.5) * num617;
                projectile.velocity.Y = projectile.velocity.Y + num618;
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 10f)
                {
                    projectile.alpha -= 5;
                    if (projectile.alpha < 100)
                    {
                        projectile.alpha = 100;
                    }
                    projectile.rotation += projectile.velocity.X * 0.1f;
                    projectile.frame = (int)(projectile.localAI[0] / 3f) % 3;
                }
            }
            if (projectile.wet)
            {
                projectile.position.Y = projectile.position.Y - 16f;
                projectile.Kill();
                return;
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(4, (int)projectile.Center.X, (int)projectile.Center.Y, 19, 1f, 0f);
            int num325 = 36;
            for (int num326 = 0; num326 < num325; num326++)
            {
                Vector2 vector8 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f;
                vector8 = vector8.RotatedBy((double)((float)(num326 - (num325 / 2 - 1)) * 6.28318548f / (float)num325), default(Vector2)) + projectile.Center;
                Vector2 vector9 = vector8 - projectile.Center;
                int num327 = Dust.NewDust(vector8 + vector9, 0, 0, mod.DustType<Dusts.CthulhuDust>(), vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[num327].noGravity = true;
                Main.dust[num327].noLight = true;
                Main.dust[num327].velocity = vector9;
            }
            if (projectile.owner == Main.myPlayer)
            {
                if (projectile.ai[1] < 1f)
                {
                    int num328 = Main.expertMode ? 25 : 40;
                    int num329 = Projectile.NewProjectile(projectile.Center.X - (float)(projectile.direction * 30), projectile.Center.Y - 4f, (float)(-(float)projectile.direction) * 0.01f, 0f, mod.ProjectileType<Leviacane>(), num328, 4f, projectile.owner, 16f, 15f);
                    Main.projectile[num329].netUpdate = true;
                }
                else
                {
                    int num330 = (int)(projectile.Center.Y / 16f);
                    int num331 = (int)(projectile.Center.X / 16f);
                    int num332 = 100;
                    if (num331 < 10)
                    {
                        num331 = 10;
                    }
                    if (num331 > Main.maxTilesX - 10)
                    {
                        num331 = Main.maxTilesX - 10;
                    }
                    if (num330 < 10)
                    {
                        num330 = 10;
                    }
                    if (num330 > Main.maxTilesY - num332 - 10)
                    {
                        num330 = Main.maxTilesY - num332 - 10;
                    }
                    for (int num333 = num330; num333 < num330 + num332; num333++)
                    {
                        Tile tile = Main.tile[num331, num333];
                        if (tile.active() && (Main.tileSolid[(int)tile.type] || tile.liquid != 0))
                        {
                            num330 = num333;
                            break;
                        }
                    }
                    int num334 = Main.expertMode ? 100 : 140;
                    int num335 = Projectile.NewProjectile((float)(num331 * 16 + 8), (float)(num330 * 16 - 24), 0f, 0f, mod.ProjectileType<Leviacane2>(), num334, 4f, Main.myPlayer, 16f, 24f);
                    Main.projectile[num335].netUpdate = true;
                }
            }
        }
    }
}