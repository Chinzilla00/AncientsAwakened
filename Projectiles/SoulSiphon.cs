using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            projectile.aiStyle = 71;
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hit = true;
        }

        public override void AI()
        {
            if (!hit)
            {
                float num472 = projectile.Center.X;
                float num473 = projectile.Center.Y;
                float num474 = 400f;
                bool flag17 = false;
                for (int num475 = 0; num475 < 200; num475++)
                {
                    if (Main.npc[num475].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
                    {
                        float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
                        float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
                        float num478 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num476) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num477);
                        if (num478 < num474)
                        {
                            num474 = num478;
                            num472 = num476;
                            num473 = num477;
                            flag17 = true;
                        }
                    }
                }
                if (flag17)
                {
                    float num483 = 20f;
                    Vector2 vector35 = new Vector2(projectile.position.X + ((float)projectile.width * 0.5f), projectile.position.Y + ((float)projectile.height * 0.5f));
                    float num484 = num472 - vector35.X;
                    float num485 = num473 - vector35.Y;
                    float num486 = (float)Math.Sqrt((double)((num484 * num484) + (num485 * num485)));
                    num486 = num483 / num486;
                    num484 *= num486;
                    num485 *= num486;
                    projectile.velocity.X = ((projectile.velocity.X * 20f) + num484) / 21f;
                    projectile.velocity.Y = ((projectile.velocity.Y * 20f) + num485) / 21f;
                    projectile.rotation += projectile.direction * 0.8f;
                    projectile.ai[0] += 1f;
                    if (projectile.ai[0] >= 30f)
                    {
                        if (projectile.ai[0] < 100f)
                        {
                            projectile.velocity *= 1.00f;
                        }
                        else
                        {
                            projectile.ai[0] = 200f;
                        }
                    }
                    for (int num257 = 0; num257 < 2; num257++)
                    {
                        int num258 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, new Color(120, 0, 30), 1f);
                        Main.dust[num258].noGravity = true;
                    }
                    return;
                }
                projectile.rotation += projectile.direction * 0.8f;
                projectile.ai[0] += 1f;
                if (projectile.ai[0] >= 30f)
                {
                    if (projectile.ai[0] < 100f)
                    {
                        projectile.velocity *= 1.00f;
                    }
                    else
                    {
                        projectile.ai[0] = 200f;
                    }
                }
                for (int num257 = 0; num257 < 2; num257++)
                {
                    int num258 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 183, 0f, 0f, 100, new Color(30, 6, 49), 1f);
                    Main.dust[num258].noGravity = true;
                }
                return;
            }

            projectile.scale *= .8f;
            projectile.alpha += 10;
            if (projectile.alpha > 255)
            {
                projectile.alpha = 255;
                projectile.scale = .1f;
            }
            int num487 = projectile.owner;
            float num488 = 4f;
            Vector2 vector36 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num489 = Main.player[num487].Center.X - vector36.X;
            float num490 = Main.player[num487].Center.Y - vector36.Y;
            float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
            if (num491 < 50f && projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && projectile.position.X + (float)projectile.width > Main.player[num487].position.X && projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && projectile.position.Y + (float)projectile.height > Main.player[num487].position.Y)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    int num492 = (int)(projectile.damage * .1f);
                    Main.player[num487].HealEffect(num492, false);
                    Main.player[num487].statLife += num492;
                    if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
                    {
                        Main.player[num487].statLife = Main.player[num487].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, num487, (float)num492, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }
            num491 = num488 / num491;
            num489 *= num491;
            num490 *= num491;
            projectile.velocity.X = (projectile.velocity.X * 15f + num489) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num490) / 16f;
            for (int num493 = 0; num493 < 3; num493++)
            {
                float num494 = projectile.velocity.X * 0.334f * (float)num493;
                float num495 = -(projectile.velocity.Y * 0.334f) * (float)num493;
                int num496 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 183, 0f, 0f, 100, new Color(30, 6, 49), 1.1f);
                Main.dust[num496].noGravity = true;
                Main.dust[num496].velocity *= 0f;
                Dust expr_1556E_cp_0 = Main.dust[num496];
                expr_1556E_cp_0.position.X = expr_1556E_cp_0.position.X - num494;
                Dust expr_1558D_cp_0 = Main.dust[num496];
                expr_1558D_cp_0.position.Y = expr_1558D_cp_0.position.Y - num495;
            }
        }
    }
}