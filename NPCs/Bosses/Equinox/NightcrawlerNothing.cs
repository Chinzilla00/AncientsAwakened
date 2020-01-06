using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class NightcrawlerNothing : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightclawer Nothing");
            Main.projFrames[projectile.type] = 5;
		}

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.scale = 1f;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
			projectile.timeLeft = 200;
        }

        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), .37f, .8f, .89f);
            
            if(projectile.ai[0] ++ == 5)
            {
                SpawnDust();
            }

            if(projectile.timeLeft <= 0)
            {
                projectile.Kill();
            }

            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }

            

            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            
            const int aislotHomingCooldown = 0;
            const int homingDelay = 9;
            const float desiredFlySpeedInPixelsPerFrame = 11;
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if(projectile.ai[1] == 0)
                {
                    if (foundTarget != -1)
                    {
                        Player target = Main.player[foundTarget];
                        Vector2 desiredVelocity = projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                        projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                    }
                }
                else if(projectile.ai[1] == 1)
                {
                    if (foundTarget != -1)
                    {
                        NPC n = Main.npc[foundTarget];
                        Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                        projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(95, 205, 228, 200);
        }

        public override void Kill(int timeLeft)
        {
            SpawnDust();
            projectile.active = false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(163, 60);
        }

        public void SpawnDust()
        {
            Vector2 position = projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;
            
            int selectedTarget = -1;

            if(projectile.ai[1] == 0)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player target = Main.player[i];
                    if (target.active && (!target.wet || homingCanAimAtWetEnemies))
                    {
                        float distance = projectile.Distance(target.Center);
                        if (distance <= homingMaximumRangeInPixels &&
                            (
                                selectedTarget == -1 || //there is no selected target
                                projectile.Distance(Main.player[selectedTarget].Center) > distance) 
                        )
                            selectedTarget = i;
                    }
                }
            }
            else if(projectile.ai[1] == 1)
            {
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
            }
            

            return selectedTarget;
        }
    }
}