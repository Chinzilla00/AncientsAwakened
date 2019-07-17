using BaseMod;
using Microsoft.Xna.Framework;
using System;
using System.IO;
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

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
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
                        int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.8f);
                        Main.dust[num353].noGravity = true;
                        Main.dust[num353].velocity *= 3f;
                        Main.dust[num353].fadeIn = 0.5f;
                        Main.dust[num353].position += projectile.velocity / 2f;
                        Main.dust[num353].velocity += projectile.velocity / 4f + Main.player[projectile.owner].velocity * 0.1f;
                    }
                }
                if (projectile.ai[0] > 2f)
                {
                    int num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 15f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 10f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    return;
                }
            }
            else
            {
                projectile.ai[0] += 1f;
                if (projectile.ai[0] > 4f)
                {
                    int num356 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, mod.DustType<Dusts.AkumaADust>(), projectile.velocity.X, projectile.velocity.Y, 100, default, 1.2f);
                    Main.dust[num356].noGravity = true;
                    Main.dust[num356].velocity *= 0.2f;
                    Main.dust[num356].position = Main.dust[num356].position.RotatedBy(projectile.rotation, projectile.Center);
                    return;
                }
            }
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            internalAI[0]++;
            if (internalAI[0] > homingDelay)
            {
                internalAI[0] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
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
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void Kill(int timeLeft)
        {
            int ExplosionType = Main.rand.Next(3);

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
                    int num642 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
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
                    int num649 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100);
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
                for (int num671 = 0; num671 < 400; num671++)
                {
                    int num672 = mod.DustType<Dusts.AkumaDust>();
                    float num673 = 16f;
                    if (num671 > 100)
                    {
                        num673 = 11f;
                    }
                    if (num671 > 100)
                    {
                        num672 = mod.DustType<Dusts.AkumaADust>();
                    }
                    if (num671 > 200)
                    {
                        num673 = 8f;
                    }
                    if (num671 > 200)
                    {
                        num672 = mod.DustType<Dusts.AkumaDust>();
                    }
                    if (num671 > 300)
                    {
                        num673 = 5f;
                    }
                    if (num671 > 300)
                    {
                        num672 = mod.DustType<Dusts.AkumaDust>();
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
