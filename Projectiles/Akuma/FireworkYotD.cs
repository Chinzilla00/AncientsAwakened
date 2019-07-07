using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class FireworkYotD : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Year of the Dragon");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 34;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 45;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[0] += 1f;
                if (projectile.ai[0] == 1f)
                {
                    for (int num352 = 0; num352 < 8; num352++)
                    {
                        int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.8f);
                        Main.dust[num353].noGravity = true;
                        Main.dust[num353].velocity *= 3f;
                        Main.dust[num353].fadeIn = 0.5f;
                        Main.dust[num353].position += projectile.velocity / 2f;
                        Main.dust[num353].velocity += projectile.velocity / 4f + Main.player[projectile.owner].velocity * 0.1f;
                    }
                }
                if (projectile.ai[0] > 2f)
                {
                    int num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 15f), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 10f), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    return;
                }
            }
            else
            {
                if (projectile.type < 415 || projectile.type > 418)
                {
                    int num355 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num355].noGravity = true;
                    Main.dust[num355].velocity *= 0.2f;
                    Main.dust[num355].position = Main.dust[num355].position.RotatedBy(projectile.rotation, projectile.Center);
                    return;
                }
                projectile.ai[0] += 1f;
                if (projectile.ai[0] > 4f)
                {
                    int num356 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num356].noGravity = true;
                    Main.dust[num356].velocity *= 0.2f;
                    Main.dust[num356].position = Main.dust[num356].position.RotatedBy(projectile.rotation, projectile.Center);
                    return;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void Kill(int timeLeft)
        {
            int ExplosionType = Main.rand.Next(4);

            if (ExplosionType == 0)
            {
                for (int num639 = 0; num639 < 400; num639++)
                {
                    float num640 = 16f;
                    if (num639 < 300)
                    {
                        num640 = 12f;
                    }
                    if (num639 < 200)
                    {
                        num640 = 8f;
                    }
                    if (num639 < 100)
                    {
                        num640 = 4f;
                    }
                    int num641 = 130;
                    int num642 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, num641, 0f, 0f, 100);
                    float num643 = Main.dust[num642].velocity.X;
                    float num644 = Main.dust[num642].velocity.Y;
                    if (num643 == 0f && num644 == 0f)
                    {
                        num643 = 1f;
                    }
                    float num645 = (float)Math.Sqrt(num643 * num643 + num644 * num644);
                    num645 = num640 / num645;
                    num643 *= num645;
                    num644 *= num645;
                    Main.dust[num642].velocity *= 0.5f;
                    Dust expr_152EE_cp_0 = Main.dust[num642];
                    expr_152EE_cp_0.velocity.X += num643;
                    Dust expr_1530D_cp_0 = Main.dust[num642];
                    expr_1530D_cp_0.velocity.Y += num644;
                    Main.dust[num642].scale = 1.3f;
                    Main.dust[num642].noGravity = true;
                }
            }
            else if (ExplosionType == 1)
            {
                for (int num646 = 0; num646 < 400; num646++)
                {
                    float num647 = 2f * (num646 / 100f);
                    if (num646 > 100)
                    {
                        num647 = 10f;
                    }
                    if (num646 > 250)
                    {
                        num647 = 13f;
                    }
                    int num648 = 131;
                    int num649 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, num648, 0f, 0f, 100);
                    float num650 = Main.dust[num649].velocity.X;
                    float num651 = Main.dust[num649].velocity.Y;
                    if (num650 == 0f && num651 == 0f)
                    {
                        num650 = 1f;
                    }
                    float num652 = (float)Math.Sqrt(num650 * num650 + num651 * num651);
                    num652 = num647 / num652;
                    if (num646 <= 200)
                    {
                        num650 *= num652;
                        num651 *= num652;
                    }
                    else
                    {
                        num650 = num650 * num652 * 1.25f;
                        num651 = num651 * num652 * 0.75f;
                    }
                    Main.dust[num649].velocity *= 0.5f;
                    Dust expr_154F4_cp_0 = Main.dust[num649];
                    expr_154F4_cp_0.velocity.X += num650;
                    Dust expr_15513_cp_0 = Main.dust[num649];
                    expr_15513_cp_0.velocity.Y += num651;
                    if (num646 > 100)
                    {
                        Main.dust[num649].scale = 1.3f;
                        Main.dust[num649].noGravity = true;
                    }
                }
            }
            else if (ExplosionType == 2)
            {
                Vector2 vector13 = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2();
                float num653 = Main.rand.Next(5, 9);
                float num654 = Main.rand.Next(12, 17);
                float value26 = Main.rand.Next(3, 7);
                float num655 = 20f;
                for (float num656 = 0f; num656 < num653; num656 += 1f)
                {
                    for (int num657 = 0; num657 < 2; num657++)
                    {
                        Vector2 value27 = vector13.RotatedBy(((num657 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f), default(Vector2));
                        for (float num658 = 0f; num658 < num655; num658 += 1f)
                        {
                            Vector2 value28 = Vector2.Lerp(vector13, value27, num658 / num655);
                            float scaleFactor2 = MathHelper.Lerp(num654, value26, num658 / num655);
                            int num659 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, 133, 0f, 0f, 100, default(Color), 1.3f);
                            Main.dust[num659].velocity *= 0.1f;
                            Main.dust[num659].noGravity = true;
                            Main.dust[num659].velocity += value28 * scaleFactor2;
                        }
                    }
                    vector13 = vector13.RotatedBy(6.28318548f / num653, default(Vector2));
                }
                for (float num660 = 0f; num660 < num653; num660 += 1f)
                {
                    for (int num661 = 0; num661 < 2; num661++)
                    {
                        Vector2 value29 = vector13.RotatedBy(((num661 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f), default(Vector2));
                        for (float num662 = 0f; num662 < num655; num662 += 1f)
                        {
                            Vector2 value30 = Vector2.Lerp(vector13, value29, num662 / num655);
                            float scaleFactor3 = MathHelper.Lerp(num654, value26, num662 / num655) / 2f;
                            int num663 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, 133, 0f, 0f, 100, default(Color), 1.3f);
                            Main.dust[num663].velocity *= 0.1f;
                            Main.dust[num663].noGravity = true;
                            Main.dust[num663].velocity += value30 * scaleFactor3;
                        }
                    }
                    vector13 = vector13.RotatedBy(6.28318548f / num653, default(Vector2));
                }
                for (int num664 = 0; num664 < 100; num664++)
                {
                    float num665 = num654;
                    int num666 = 132;
                    int num667 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, num666, 0f, 0f, 100);
                    float num668 = Main.dust[num667].velocity.X;
                    float num669 = Main.dust[num667].velocity.Y;
                    if (num668 == 0f && num669 == 0f)
                    {
                        num668 = 1f;
                    }
                    float num670 = (float)Math.Sqrt(num668 * num668 + num669 * num669);
                    num670 = num665 / num670;
                    num668 *= num670;
                    num669 *= num670;
                    Main.dust[num667].velocity *= 0.5f;
                    Dust expr_15A4E_cp_0 = Main.dust[num667];
                    expr_15A4E_cp_0.velocity.X += num668;
                    Dust expr_15A6D_cp_0 = Main.dust[num667];
                    expr_15A6D_cp_0.velocity.Y += num669;
                    Main.dust[num667].scale = 1.3f;
                    Main.dust[num667].noGravity = true;
                }
            }
            else if (ExplosionType == 3)
            {
                for (int num671 = 0; num671 < 400; num671++)
                {
                    int num672 = 133;
                    float num673 = 16f;
                    if (num671 > 100)
                    {
                        num673 = 11f;
                    }
                    if (num671 > 100)
                    {
                        num672 = 134;
                    }
                    if (num671 > 200)
                    {
                        num673 = 8f;
                    }
                    if (num671 > 200)
                    {
                        num672 = 133;
                    }
                    if (num671 > 300)
                    {
                        num673 = 5f;
                    }
                    if (num671 > 300)
                    {
                        num672 = 134;
                    }
                    int num674 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, num672, 0f, 0f, 100);
                    float num675 = Main.dust[num674].velocity.X;
                    float num676 = Main.dust[num674].velocity.Y;
                    if (num675 == 0f && num676 == 0f)
                    {
                        num675 = 1f;
                    }
                    float num677 = (float)Math.Sqrt(num675 * num675 + num676 * num676);
                    num677 = num673 / num677;
                    if (num671 > 300)
                    {
                        num675 = num675 * num677 * 0.7f;
                        num676 *= num677;
                    }
                    else if (num671 > 200)
                    {
                        num675 *= num677;
                        num676 = num676 * num677 * 0.7f;
                    }
                    else if (num671 > 100)
                    {
                        num675 = num675 * num677 * 0.7f;
                        num676 *= num677;
                    }
                    else
                    {
                        num675 *= num677;
                        num676 = num676 * num677 * 0.7f;
                    }
                    Main.dust[num674].velocity *= 0.5f;
                    Dust expr_15CE9_cp_0 = Main.dust[num674];
                    expr_15CE9_cp_0.velocity.X += num675;
                    Dust expr_15D08_cp_0 = Main.dust[num674];
                    expr_15D08_cp_0.velocity.Y += num676;
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[num674].scale = 1.3f;
                        Main.dust[num674].noGravity = true;
                    }
                }
            }
        }

    }
}
