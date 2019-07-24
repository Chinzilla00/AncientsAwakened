using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DMC : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DMC");
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Electrified"), 500);
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = 9;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int num107 = Main.rand.Next(3);
                if (num107 == 0)
                {
                    num107 = 15;
                }
                else if (num107 == 1)
                {
                    num107 = 57;
                }
                else
                {
                    num107 = 58;
                }
                int num108 = Dust.NewDust(projectile.position, projectile.width, projectile.height, num107, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 255, default, 0.7f);
                Main.dust[num108].velocity *= 0.25f;
                Main.dust[num108].position = (Main.dust[num108].position + projectile.position) / 2f;
            }
            if (Main.myPlayer == projectile.owner && projectile.ai[0] <= 0f)
            {
                if (Main.player[projectile.owner].channel)
                {
                    float num113 = 30;
                    Vector2 vector9 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num114 = (float)Main.mouseX + Main.screenPosition.X - vector9.X;
                    float num115 = (float)Main.mouseY + Main.screenPosition.Y - vector9.Y;
                    if (Main.player[projectile.owner].gravDir == -1f)
                    {
                        num115 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector9.Y;
                    }
                    float num116 = (float)Math.Sqrt((double)(num114 * num114 + num115 * num115));
                    if (projectile.ai[0] < 0f)
                    {
                        projectile.ai[0] += 1f;
                    }
                    if (num116 < 100f)
                    {
                        if (projectile.velocity.Length() < num113)
                        {
                            projectile.velocity *= 1.1f;
                            if (projectile.velocity.Length() > num113)
                            {
                                projectile.velocity.Normalize();
                                projectile.velocity *= num113;
                            }
                        }
                        if (projectile.ai[0] == 0f)
                        {
                            projectile.ai[0] = -10f;
                        }
                    }
                    else if (num116 > num113)
                    {
                        num116 = num113 / num116;
                        num114 *= num116;
                        num115 *= num116;
                        int num117 = (int)(num114 * 1000f);
                        int num118 = (int)(projectile.velocity.X * 1000f);
                        int num119 = (int)(num115 * 1000f);
                        int num120 = (int)(projectile.velocity.Y * 1000f);
                        if (num117 != num118 || num119 != num120)
                        {
                            projectile.netUpdate = true;
                        }
                        Vector2 value6 = new Vector2(num114, num115);
                        projectile.velocity = (projectile.velocity * 4f + value6) / 5f;
                    }
                    else
                    {
                        int num121 = (int)(num114 * 1000f);
                        int num122 = (int)(projectile.velocity.X * 1000f);
                        int num123 = (int)(num115 * 1000f);
                        int num124 = (int)(projectile.velocity.Y * 1000f);
                        if (num121 != num122 || num123 != num124)
                        {
                            projectile.netUpdate = true;
                        }
                        projectile.velocity.X = num114;
                        projectile.velocity.Y = num115;
                    }
                }
                else if (projectile.ai[0] <= 0f)
                {
                    projectile.netUpdate = true;
                    
                    projectile.ai[0] = 1f;
                }
            }
            projectile.localAI[0] += 1f;
            if (projectile.ai[0] > 0f && projectile.localAI[0] > 15f)
            {
                projectile.tileCollide = false;
                Vector2 vector11 = Main.player[projectile.owner].Center - projectile.Center;
                if (vector11.Length() < 20f)
                {
                    projectile.Kill();
                }
                vector11.Normalize();
                vector11 *= 25f;
                projectile.velocity = (projectile.velocity * 5f + vector11) / 6f;
            }
            else if (projectile.ai[0] > 0f)
            {
                projectile.rotation += 0.5f * projectile.direction;
            }
            else
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }
    }
}
