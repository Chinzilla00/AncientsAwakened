using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class YWProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydra Soul");
            Main.projFrames[projectile.type] = 4;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 4; //
            projectile.timeLeft = 300;
            projectile.aiStyle = 1; //
            aiType = ProjectileID.Bullet;
        }

        public override void AI()
        {

            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X);
            }
            else
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
            }
            int num557 = 8;
            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X + num557, projectile.position.Y + num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X + num557, projectile.position.Y + num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            Main.dust[dustId3].noGravity = true;

            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 50;
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

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 700;

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
        

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num579 = 0; num579 < 20; num579++)
            {
                int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 2f;
                num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num580].velocity *= 2f;
            }

            int num3;
            for (int num622 = 0; num622 < 20; num622 = num3 + 1)
            {
                int num623 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0f, 0f, 0);
                Dust dust = Main.dust[num623];
                dust.scale *= 1.1f;
                Main.dust[num623].noGravity = true;
                num3 = num622;
            }
            for (int num624 = 0; num624 < 30; num624 = num3 + 1)
            {
                int num625 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0f, 0f, 0);
                Dust dust = Main.dust[num625];
                dust.velocity *= 2.5f;
                dust = Main.dust[num625];
                dust.scale *= 0.8f;
                Main.dust[num625].noGravity = true;
                num3 = num624;
            }
            if (projectile.owner == Main.myPlayer)
            {
                int num626 = 2;
                if (Main.rand.Next(10) == 0)
                {
                    num626++;
                }
                if (Main.rand.Next(10) == 0)
                {
                    num626++;
                }
                if (Main.rand.Next(10) == 0)
                {
                    num626++;
                }
                for (int num627 = 0; num627 < num626; num627 = num3 + 1)
                {
                    float num628 = Main.rand.Next(-35, 36) * 0.02f;
                    float num629 = Main.rand.Next(-35, 36) * 0.02f;
                    num628 *= 10f;
                    num629 *= 10f;
                    int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num628, num629, mod.ProjectileType<YWSplit>(), projectile.damage * 3, (int)(projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
                    num3 = num627;
                    Main.projectile[p].timeLeft = 240;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 300);
        }
    }
}