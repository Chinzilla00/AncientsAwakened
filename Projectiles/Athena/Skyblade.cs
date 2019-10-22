using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Athena
{
    public class Skyblade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        float desiredFlySpeedInPixelsPerFrame = 20;

        public override void AI()
        {
            if (projectile.frameCounter++ > 2)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float amountOfFramesToLerpBy = 10; // minimum of 1, please keep in full numbers even though it's a float!
            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }

            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 100, 0f, 0f, 0, Color.White, 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].alpha = 20;
            }

            if (projectile.alpha % 15 == 0)
            {
                desiredFlySpeedInPixelsPerFrame--;
            }

            if (projectile.alpha < 255)
            {
                projectile.alpha -= 3;
            }
            else
            {
                projectile.active = false;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            desiredFlySpeedInPixelsPerFrame--;
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

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
    }
}