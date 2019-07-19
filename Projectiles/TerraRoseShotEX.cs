using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles
{
    public class TerraRoseShotEX : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;  
            projectile.width = 18;
            projectile.height = 18;
            projectile.tileCollide = false;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
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

        public override void AI()
		{
			if (Main.rand.NextFloat() < 0.8f)
			{
				Vector2 position = projectile.position;
                int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 107, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100);
                Main.dust[dustId].noGravity = true;

                const int aislotHomingCooldown = 0;
                const int homingDelay = 10;
                const float desiredFlySpeedInPixelsPerFrame = 60;
                const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

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
            }
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }

        public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.position);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DummyExplosionTerra"), projectile.damage, 0, Main.myPlayer);
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, 0.0f, 0.0f, 100, new Color(), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
    }
}
