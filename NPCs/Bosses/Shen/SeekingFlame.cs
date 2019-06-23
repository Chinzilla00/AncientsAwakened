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

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
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
            projectile.alpha = 60;
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

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] == 1 ? mod.BuffType("DiscordInferno") : projectile.ai[0] == 2 ? mod.BuffType("HydraToxin") : mod.BuffType("DiscordInferno"), 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int ShaderType = projectile.ai[0] == 1 ? mod.ItemType<Items.Dyes.BlazingDye>() : projectile.ai[0] == 2 ? mod.ItemType<Items.Dyes.AbyssalDye>() : mod.ItemType<Items.Dyes.DiscordianDye>();
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ShaderType);
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type], 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], shader, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, Main.projFrames[projectile.type], frame, Color.White, true);
            return false;
        }

        public override void Kill(int timeleft)
        {

            int dustType = projectile.ai[0] == 1 ? mod.DustType<Dusts.AkumaADust>() : projectile.ai[0] == 2 ? mod.DustType<Dusts.YamataADust>() : mod.DustType<Dusts.Discord>();
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, dustType, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].velocity *= 2f;
            }
            float ExplosionType = projectile.ai[0] == 1 ? 1 : projectile.ai[0] == 2 ? 2 : 0;
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("ShenBoom"), projectile.damage, projectile.knockBack, projectile.owner, ExplosionType, 0f);
        }
    }
}