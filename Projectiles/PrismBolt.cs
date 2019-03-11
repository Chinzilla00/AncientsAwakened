using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class PrismBolt : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.magic = false;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.friendly = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prism Bolt");

		}

        public override void AI()
        {
            for (int num339 = 0; num339 < 4; num339++)
            {
                Dust dust1;
                Dust dust2;
                Dust dust3;
                Dust dust4;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust2 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust3 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust4 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.alpha = 0;
                dust2.alpha = 0;
                dust3.alpha = 0;
                dust4.alpha = 0;
                dust1.noGravity = true;
                dust2.noGravity = true;
                dust3.noGravity = true;
                dust4.noGravity = true;
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 10;
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
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust2 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
        }
	}
}