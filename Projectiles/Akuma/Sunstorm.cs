using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class Sunstorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sunstorm");

            Main.projFrames[projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 5;
            projectile.penetrate = -1;
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
            if (projectile.ai[1] != -1f && projectile.position.Y > projectile.ai[1])
            {
                projectile.tileCollide = true;
            }
            if (projectile.position.HasNaNs())
            {
                projectile.Kill();
                return;
            }
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.position.X / 16, (int)projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f)];
            dust19.position = projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f)];
            dust18.position = projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
            }
            if (projectile.ai[1] == -1f)
            {
                projectile.ai[0] += 1f;
                projectile.velocity = Vector2.Zero;
                projectile.tileCollide = false;
                projectile.penetrate = -1;
                projectile.position = projectile.Center;
                projectile.width = (projectile.height = 140);
                projectile.Center = projectile.position;
                projectile.alpha -= 10;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                if (++projectile.frameCounter >= projectile.MaxUpdates * 3)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.ai[0] >= Main.projFrames[projectile.type] * projectile.MaxUpdates * 3)
                {
                    projectile.Kill();
                }
                return;
            }
            projectile.alpha = 255;
            internalAI[0]++;
            if (internalAI[0] > 20)
            {
                internalAI[0] = 20; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * 30;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / 30);
                }
            }
            if (projectile.numUpdates == 0)
            {
                int num185 = -1;
                float num186 = 60f;
                for (int num187 = 0; num187 < 200; num187++)
                {
                    NPC nPC2 = Main.npc[num187];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num188 = projectile.Distance(nPC2.Center);
                        if (num188 < num186 && Collision.CanHitLine(projectile.Center, 0, 0, nPC2.Center, 0, 0))
                        {
                            num186 = num188;
                            num185 = num187;
                        }
                    }
                }
                if (num185 != -1)
                {
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = -1f;
                    projectile.netUpdate = true;
                    return;
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
            projectile.ai[0] = 0f;
            projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
        {
            bool flag = WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.position.X / 16, (int)projectile.position.Y / 16));

            for (int num58 = 0; num58 < 4; num58++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default(Color), 1.5f);
            }
            for (int num59 = 0; num59 < 4; num59++)
            {
                int num60 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num60].noGravity = true;
                Main.dust[num60].velocity *= 3f;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
                num60 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num60].velocity *= 2f;
                Main.dust[num60].noGravity = true;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
            }
        }
    }
}