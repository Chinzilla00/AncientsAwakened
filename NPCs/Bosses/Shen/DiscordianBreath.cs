using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class DiscordianBreath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Breath");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 136;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public int chargeWidth = 50;
        public int normalWidth = 250;

        public override void AI()
        {
            if (projectile.ai[1] < 0f || projectile.ai[1] > 200f)
            {
                projectile.Kill();
                return;
            }
            NPC nPC = Main.npc[(int)projectile.ai[1]];
            float num = -8f;
            Vector2 center = nPC.Center + new Vector2((110f + num) * (float)nPC.spriteDirection, 30f).RotatedBy((double)nPC.rotation, default(Vector2));
            projectile.Center = center;
            projectile.rotation = nPC.DirectionTo(projectile.Center).ToRotation();
            DelegateMethods.v3_1 = new Vector3(1.2f, 1f, 0.3f);
            float num2 = projectile.ai[0] / 40f;
            if (num2 > 1f)
            {
                num2 = 1f;
            }
            float num3 = (projectile.ai[0] - 38f) / 40f;
            if (num3 < 0f)
            {
                num3 = 0f;
            }
            Utils.PlotTileLine(projectile.Center + (projectile.rotation.ToRotationVector2() * 400f * num3), projectile.Center + (projectile.rotation.ToRotationVector2() * 400f * num2), 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));
            Utils.PlotTileLine(projectile.Center + (projectile.rotation.ToRotationVector2().RotatedBy(0.19634954631328583, default(Vector2)) * 400f * num3), projectile.Center + (projectile.rotation.ToRotationVector2().RotatedBy(0.19634954631328583, default(Vector2)) * 400f * num2), 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));
            Utils.PlotTileLine(projectile.Center + (projectile.rotation.ToRotationVector2().RotatedBy(-0.19634954631328583, default(Vector2)) * 400f * num3), projectile.Center + (projectile.rotation.ToRotationVector2().RotatedBy(-0.19634954631328583, default(Vector2)) * 400f * num2), 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));
            if (num3 == 0f && num2 > 0.1f)
            {
                for (int i = 0; i < 3; i++)
                {
                    int NUM1 = mod.DustType<Dusts.Discord>();
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, NUM1, 0f, 0f, 0, default(Color), 1f);
                    dust.fadeIn = 1.5f;
                    dust.velocity = projectile.rotation.ToRotationVector2().RotatedBy((double)(Main.rand.NextFloatDirection() * 0.2617994f), default(Vector2)) * (0.5f + (Main.rand.NextFloat() * 2.5f)) * 15f;
                    dust.velocity += nPC.velocity * 2f;
                    dust.noLight = true;
                    dust.noGravity = true;
                    dust.alpha = 200;
                }
            }
            if (Main.rand.Next(5) == 0 && projectile.ai[0] >= 15f)
            {
                Vector2 vector = projectile.Center + (projectile.rotation.ToRotationVector2() * 300f);
                vector -= Utils.RandomVector2(Main.rand, -20f, 20f);
                Gore gore = Gore.NewGoreDirect(vector, Vector2.Zero, 61 + Main.rand.Next(3), 0.5f);
                gore.velocity *= 0.3f;
                gore.velocity += projectile.rotation.ToRotationVector2() * 4f;
            }
            for (int j = 0; j < 1; j++)
            {
                int NUM1 = mod.DustType<Dusts.Discord>();
                Dust dust2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 1f);
                dust2.fadeIn = 1.5f;
                dust2.scale = 0.4f;
                dust2.velocity = projectile.rotation.ToRotationVector2().RotatedBy((double)(Main.rand.NextFloatDirection() * 0.2617994f), default(Vector2)) * (0.5f + (Main.rand.NextFloat() * 2.5f)) * 15f;
                dust2.velocity += nPC.velocity * 2f;
                dust2.velocity *= 0.3f;
                dust2.noLight = true;
                dust2.noGravity = true;
                float num4 = Main.rand.NextFloat();
                dust2.position = Vector2.Lerp(projectile.Center + (projectile.rotation.ToRotationVector2() * 400f * num3), projectile.Center + (projectile.rotation.ToRotationVector2() * 400f * num2), num4);
                dust2.position += projectile.rotation.ToRotationVector2().RotatedBy(1.5707963705062866, default(Vector2)) * (20f + (100f * (num4 - 0.5f)));
            }
            projectile.frameCounter++;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 78f)
            {
                projectile.Kill();
            }
        }
    }
}