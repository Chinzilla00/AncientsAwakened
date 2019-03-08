using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class VoidRing : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.scale = .1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
        }

        public float[] internalAI = new float[4];
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
            projectile.Center = Main.projectile[(int)projectile.ai[0]].Center;
            if (projectile.alpha != 0)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] >= 4f)
                {
                    projectile.alpha -= 90;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                        projectile.localAI[0] = 2f;
                    }
                }
            }
            if (projectile.scale < 1f)
            {
                projectile.scale += .1f;
            }
            if (Vector2.Distance(projectile.Center, new Vector2(internalAI[0], projectile.ai[1]) * 16f + Vector2.One * 8f) <= 16f)
            {
                projectile.Kill();
                return;
            }
            if (projectile.alpha == 0)
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 120f)
                {
                    projectile.Kill();
                    return;
                }
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.8f, 0.3f, 0.8f);
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] == 3f)
                {
                    projectile.localAI[0] = 0f;
                    for (int num53 = 0; num53 < 8; num53++)
                    {
                        Vector2 vector7 = Vector2.UnitX * -8f;
                        vector7 += -Vector2.UnitY.RotatedBy((double)((float)num53 * 3.14159274f / 4f), default(Vector2)) * new Vector2(2f, 4f);
                        vector7 = vector7.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                        int num54 = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num54].scale = 1.5f;
                        Main.dust[num54].noGravity = true;
                        Main.dust[num54].position = projectile.Center + vector7;
                        Main.dust[num54].velocity = projectile.velocity * 0.66f;
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 10)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}