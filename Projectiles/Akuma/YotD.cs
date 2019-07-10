using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class YotD : ModProjectile
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
                        int num353 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 1.8f);
                        Main.dust[num353].noGravity = true;
                        Main.dust[num353].velocity *= 3f;
                        Main.dust[num353].fadeIn = 0.5f;
                        Main.dust[num353].position += projectile.velocity / 2f;
                        Main.dust[num353].velocity += projectile.velocity / 4f + Main.player[projectile.owner].velocity * 0.1f;
                    }
                }
                if (projectile.ai[0] > 2f)
                {
                    int num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 15f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                    Main.dust[num354].noGravity = true;
                    Main.dust[num354].velocity *= 0.2f;
                    Main.dust[num354].position = Main.dust[num354].position.RotatedBy(projectile.rotation, projectile.Center);
                    num354 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 10f), 8, 8, mod.DustType<Dusts.AkumaDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
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
                    int num356 = Dust.NewDust(new Vector2(projectile.position.X + 2f, projectile.position.Y + 20f), 8, 8, mod.DustType<Dusts.AkumaADust>(), projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
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
            Vector2 vector13 = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2();
            float num653 = Main.rand.Next(5, 9);
            float num654 = Main.rand.Next(12, 17);
            float value26 = Main.rand.Next(3, 7);
            float num655 = 20f;
            for (float num656 = 0f; num656 < num653; num656 += 1f)
            {
                for (int num657 = 0; num657 < 2; num657++)
                {
                    Vector2 value27 = vector13.RotatedBy(((num657 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f));
                    for (float num658 = 0f; num658 < num655; num658 += 1f)
                    {
                        Vector2 value28 = Vector2.Lerp(vector13, value27, num658 / num655);
                        float scaleFactor2 = MathHelper.Lerp(num654, value26, num658 / num655);
                        int num659 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 1.3f);
                        Main.dust[num659].velocity *= 0.1f;
                        Main.dust[num659].noGravity = true;
                        Main.dust[num659].velocity += value28 * scaleFactor2;
                    }
                }
                vector13 = vector13.RotatedBy(6.28318548f / num653);
            }
            for (float num660 = 0f; num660 < num653; num660 += 1f)
            {
                for (int num661 = 0; num661 < 2; num661++)
                {
                    Vector2 value29 = vector13.RotatedBy(((num661 == 0) ? 1f : -1f) * 6.28318548f / (num653 * 2f));
                    for (float num662 = 0f; num662 < num655; num662 += 1f)
                    {
                        Vector2 value30 = Vector2.Lerp(vector13, value29, num662 / num655);
                        float scaleFactor3 = MathHelper.Lerp(num654, value26, num662 / num655) / 2f;
                        int num663 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default(Color), 1.3f);
                        Main.dust[num663].velocity *= 0.1f;
                        Main.dust[num663].noGravity = true;
                        Main.dust[num663].velocity += value30 * scaleFactor3;
                    }
                }
                vector13 = vector13.RotatedBy(6.28318548f / num653);
            }
            for (int num664 = 0; num664 < 100; num664++)
            {
                float num665 = num654;
                int num667 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}
