using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DMCE : ModProjectile
      {
	  public override void SetStaticDefaults() 
           {
	     ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;    //The length of old position to be recorded
             ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode        
           }

        public override void SetDefaults()
         {
	    projectile.aiStyle = -1;
            projectile.width = 38;
            projectile.height = 60;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.alpha = 254;
            projectile.extraUpdates = 1;
         }

        public override void AI()
        {
           projectile.rotation = (projectile.position.X + projectile.position.Y / 4) * 0.0150f;
           Lighting.AddLight(projectile.Center, (0 - projectile.alpha) * 1f / 100f, (64 - projectile.alpha) * 1f / 100f, (45 - projectile.alpha) * 1f / 100f);

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 5;
            }
            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; 

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
          public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
          {
                int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 1.5f;
                num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100);
                Main.dust[num580].velocity *= 1.5f;
          }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

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