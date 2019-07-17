using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueCopper
{
    public class CopperSoul : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            #region Damage
            if (player.meleeDamage > player.rangedDamage && player.meleeDamage > player.magicDamage && player.meleeDamage > player.minionDamage && player.meleeDamage > player.thrownDamage)
            {
                projectile.ranged = false;
                projectile.magic = false;
                projectile.minion = false;
                projectile.thrown = false;
                projectile.melee = true;
            }
            else if (player.rangedDamage > player.meleeDamage && player.rangedDamage > player.magicDamage && player.rangedDamage > player.minionDamage && player.rangedDamage > player.thrownDamage)
            {
                projectile.magic = false;
                projectile.minion = false;
                projectile.thrown = false;
                projectile.melee = false;
                projectile.ranged = true;
            }
            else if (player.magicDamage > player.meleeDamage && player.magicDamage > player.rangedDamage && player.magicDamage > player.minionDamage && player.magicDamage > player.thrownDamage)
            {
                projectile.minion = false;
                projectile.thrown = false;
                projectile.melee = false;
                projectile.ranged = false;
                projectile.magic = true;
            }
            else if (player.minionDamage > player.meleeDamage && player.minionDamage > player.magicDamage && player.minionDamage > player.rangedDamage && player.minionDamage > player.thrownDamage)
            {
                projectile.magic = false;
                projectile.thrown = false;
                projectile.melee = false;
                projectile.ranged = false;
                projectile.minion = true;
            }
            else if (player.thrownDamage > player.meleeDamage && player.thrownDamage > player.magicDamage && player.thrownDamage > player.minionDamage && player.thrownDamage > player.rangedDamage)
            {
                projectile.magic = false;
                projectile.minion = false;
                projectile.melee = false;
                projectile.ranged = false;
                projectile.thrown = true;
            }
            else
            {
                projectile.magic = false;
                projectile.minion = false;
                projectile.thrown = false;
                projectile.melee = false;
                projectile.ranged = false;
            }
            #endregion
            
            Dust dust = Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 100, Main.DiscoColor, 1f);

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

        public override void Kill(int timeleft)
        {
            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(6f, 0f), (m / 20f) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(9f, 0f), (m / 20f) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Soul");
		}
    }
}