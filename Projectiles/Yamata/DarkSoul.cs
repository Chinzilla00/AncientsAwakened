using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class DarkSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Soul");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 59;
            projectile.alpha = 255;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 3;
        }

        public override void AI()
        {
            projectile.ai[1] += 1f;
            if (projectile.ai[1] >= 60f)
            {
                const int aislotHomingCooldown = 0;
                const int homingDelay = 20;
                const float desiredFlySpeedInPixelsPerFrame = 15;
                const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

                projectile.ai[aislotHomingCooldown]++;
                if (projectile.ai[aislotHomingCooldown] > homingDelay)
                {
                    projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

                    int foundTarget = HomeOnTarget();
                    if (foundTarget != -1)
                    {
                        NPC n = Main.npc[foundTarget];
                        Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                        projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                    }
                }
            }
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = projectile.velocity.X * 0.2f * (float)num572;
                float num574 = -(projectile.velocity.Y * 0.2f) * (float)num572;
                int num575 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataDustLight>(), 0f, 0f, 100, default(Color), 1.3f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }
            return; 
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
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

    }
}
