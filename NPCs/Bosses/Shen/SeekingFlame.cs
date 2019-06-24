using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    internal class SeekingFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("Seeking Flame");
        }

        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.scale = 1.1f;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (projectile.timeLeft > 0)
            {
                projectile.timeLeft--;
            }
            if (projectile.timeLeft == 0)
            {
                projectile.Kill();
            }

            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            const int aislotHomingCooldown = 1;
            const int homingDelay = 60;
            const float desiredFlySpeedInPixelsPerFrame = 10;
            const float amountOfFramesToLerpBy = 20;

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player target = Main.player[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                Player target = Main.player[i];
                if (target.active && (!target.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(target.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                    (
                        selectedTarget == -1 || projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public float[] InternalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(InternalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                InternalAI[0] = reader.ReadFloat();
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = projectile.ai[0] == 1 ? AAColor.AkumaA : projectile.ai[0] == 2 ? AAColor.YamataA : Color.Magenta;
            return new Color(color.R, color.G, color.B, 200);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] == 1 ? mod.BuffType("DiscordInferno") : projectile.ai[0] == 2 ? mod.BuffType("HydraToxin") : mod.BuffType("DiscordInferno"), 300);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 4f;
            double offsetAngle;
            int i;
            int dustType = projectile.ai[0] == 1 ? mod.DustType<Dusts.AkumaADust>() : projectile.ai[0] == 2 ? mod.DustType<Dusts.YamataADust>() : mod.DustType<Dusts.Discord>();
            if (projectile.owner == Main.myPlayer)
            {
                for (i = 0; i < 4; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("HomingSplit"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), mod.ProjectileType("HomingSplit"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
                }
                for (i = 0; i < 2; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("HomingSplit"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), mod.ProjectileType("HomingSplit"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
                }
            }
            for (int dust = 0; dust <= 10; dust++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDust>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}