using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class Prismshot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism Shot");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 100;
            projectile.aiStyle = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            return false;
        }

        public override void AI()
        {
            for (int num110 = 0; num110 < 1; num110++)
            {
                int num111 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 66, 0f, 0f, 100, AAColor.Hallow, 1f);
                Main.dust[num111].velocity *= 0.1f;
                Main.dust[num111].velocity += projectile.velocity * 0.2f;
                Main.dust[num111].position.X = projectile.position.X + (float)(projectile.width / 2) + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[num111].position.Y = projectile.position.Y + (float)(projectile.height / 2) + (float)Main.rand.Next(-2, 3);
                Main.dust[num111].noGravity = true;
            }

            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 5;
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

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

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 100;

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

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num585 = 0; num585 < 20; num585++)
            {
                int num586 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 66, 0f, 0f, 100, AAColor.Hallow, 2f);
                Main.dust[num586].noGravity = true;
                Main.dust[num586].velocity *= 4f;
            }
        }
    }
}