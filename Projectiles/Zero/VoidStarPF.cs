using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Projectiles.Zero
{
    class VoidStarPF : ModProjectile
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Star");
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Projectiles/Zero/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

        public override void SetDefaults()
		{
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.aiStyle = 0;
            projectile.alpha = 100;
            projectile.ignoreWater = true;
            projectile.glowMask = customGlowMask;
        }

        public override void AI()
        {
            {
                projectile.ai[0] += 1f;
                int num1002 = 0;
                if (projectile.velocity.Length() <= 4f)
                {
                    num1002 = 1;
                }
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                if (num1002 == 0)
                {
                    projectile.rotation -= 0.104719758f;
                    if (Main.rand.Next(3) == 0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            Vector2 vector124 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                            Dust dust27 = Main.dust[Dust.NewDust(projectile.Center - (vector124 * 30f), 0, 0, Utils.SelectRandom<int>(Main.rand, new int[]
                            {
                                                                                                                    86,
                                                                                                                    90
                            }), 0f, 0f, 0, new Color(120, 0, 30), 1f)];
                            dust27.noGravity = true;
                            dust27.position = projectile.Center - (vector124 * (float)Main.rand.Next(10, 21));
                            dust27.velocity = vector124.RotatedBy(1.5707963705062866, default(Vector2)) * 6f;
                            dust27.scale = 0.5f + Main.rand.NextFloat();
                            dust27.fadeIn = 0.5f;
                            dust27.customData = this;
                        }
                        else
                        {
                            Vector2 vector125 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                            Dust dust28 = Main.dust[Dust.NewDust(projectile.Center - (vector125 * 30f), 0, 0, 240, 0f, 0f, 0, new Color(120, 0, 30), 1f)];
                            dust28.noGravity = true;
                            dust28.position = projectile.Center - (vector125 * 30f);
                            dust28.velocity = vector125.RotatedBy(-1.5707963705062866, default(Vector2)) * 3f;
                            dust28.scale = 0.5f + Main.rand.NextFloat();
                            dust28.fadeIn = 0.5f;
                            dust28.customData = this;
                        }
                    }
                    if (projectile.ai[0] >= 30f)
                    {
                        projectile.velocity *= 0.98f;
                        projectile.scale += 0.00744680827f;
                        if (projectile.scale > 1.3f)
                        {
                            projectile.scale = 1.3f;
                        }
                        projectile.rotation -= 0.0174532924f;
                    }
                    if (projectile.velocity.Length() < 4.1f)
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= 4f;
                        projectile.ai[0] = 0f;
                    }
                }
                else if (num1002 == 1)
                {
                    projectile.rotation -= 0.104719758f;
                    for (int num1003 = 0; num1003 < 1; num1003++)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            Vector2 vector126 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                            Dust dust29 = Main.dust[Dust.NewDust(projectile.Center - (vector126 * 30f), 0, 0, 86, 0f, 0f, 0, new Color(120, 0, 30), 1f)];
                            dust29.noGravity = true;
                            dust29.position = projectile.Center - (vector126 * (float)Main.rand.Next(10, 21));
                            dust29.velocity = vector126.RotatedBy(1.5707963705062866, default(Vector2)) * 6f;
                            dust29.scale = 0.9f + Main.rand.NextFloat();
                            dust29.fadeIn = 0.5f;
                            dust29.customData = this;
                            vector126 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                            dust29 = Main.dust[Dust.NewDust(projectile.Center - (vector126 * 30f), 0, 0, 90, 0f, 0f, 0, new Color(120, 0, 30), 1f)];
                            dust29.noGravity = true;
                            dust29.position = projectile.Center - (vector126 * (float)Main.rand.Next(10, 21));
                            dust29.velocity = vector126.RotatedBy(1.5707963705062866, default(Vector2)) * 6f;
                            dust29.scale = 0.9f + Main.rand.NextFloat();
                            dust29.fadeIn = 0.5f;
                            dust29.customData = this;
                            dust29.color = Color.Crimson;
                        }
                        else
                        {
                            Vector2 vector127 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                            Dust dust30 = Main.dust[Dust.NewDust(projectile.Center - (vector127 * 30f), 0, 0, 240, 0f, 0f, 0, new Color(120, 0, 30), 1f)];
                            dust30.noGravity = true;
                            dust30.position = projectile.Center - (vector127 * (float)Main.rand.Next(20, 31));
                            dust30.velocity = vector127.RotatedBy(-1.5707963705062866, default(Vector2)) * 5f;
                            dust30.scale = 0.9f + Main.rand.NextFloat();
                            dust30.fadeIn = 0.5f;
                            dust30.customData = this;
                        }
                    }
                    if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 241f && Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector128 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 12f;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector128.X, vector128.Y, 618, projectile.damage / 2, 0f, projectile.owner, 0f, (float)projectile.whoAmI);
                    }
                    Vector2 vector129 = projectile.Center;
                    float num1004 = 800f;
                    bool flag58 = false;
                    int num1005 = 0;
                    if (projectile.ai[1] == 0f)
                    {
                        for (int num1006 = 0; num1006 < 200; num1006++)
                        {
                            if (Main.npc[num1006].CanBeChasedBy(this, false))
                            {
                                Vector2 center13 = Main.npc[num1006].Center;
                                if (projectile.Distance(center13) < num1004 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num1006].position, Main.npc[num1006].width, Main.npc[num1006].height))
                                {
                                    num1004 = projectile.Distance(center13);
                                    vector129 = center13;
                                    flag58 = true;
                                    num1005 = num1006;
                                }
                            }
                        }
                        if (flag58)
                        {
                            if (projectile.ai[1] != (float)(num1005 + 1))
                            {
                                projectile.netUpdate = true;
                            }
                            projectile.ai[1] = (float)(num1005 + 1);
                        }
                        flag58 = false;
                    }
                    if (projectile.ai[1] != 0f)
                    {
                        int num1007 = (int)(projectile.ai[1] - 1f);
                        if (Main.npc[num1007].active && Main.npc[num1007].CanBeChasedBy(this, true) && projectile.Distance(Main.npc[num1007].Center) < 1000f)
                        {
                            flag58 = true;
                            vector129 = Main.npc[num1007].Center;
                        }
                    }
                    if (!projectile.friendly)
                    {
                        flag58 = false;
                    }
                    if (flag58)
                    {
                        float num1008 = 4f;
                        int num1009 = 8;
                        Vector2 vector130 = new Vector2(projectile.position.X + ((float)projectile.width * 0.5f), projectile.position.Y + ((float)projectile.height * 0.5f));
                        float num1010 = vector129.X - vector130.X;
                        float num1011 = vector129.Y - vector130.Y;
                        float num1012 = (float)Math.Sqrt((double)((num1010 * num1010) + (num1011 * num1011)));
                        num1012 = num1008 / num1012;
                        num1010 *= num1012;
                        num1011 *= num1012;
                        projectile.velocity.X = ((projectile.velocity.X * (float)(num1009 - 1)) + num1010) / (float)num1009;
                        projectile.velocity.Y = ((projectile.velocity.Y * (float)(num1009 - 1)) + num1011) / (float)num1009;
                    }
                }
                if (projectile.alpha < 150)
                {
                    Lighting.AddLight(projectile.Center, 0.7f, 0.0f, 0.0f);
                }
                if (projectile.ai[0] >= 600f)
                {
                    projectile.Kill();
                    return;
                }
            }
        }
    }
}
