using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BaseMod;

namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class ZeroStarP : ModProjectile
	{
        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.LightDisc);
            aiType = ProjectileID.LightDisc;
            projectile.width = 46;
			projectile.height = 46;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
        }
        
        public float[] shootAI = new float[4];

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, player.Center, 20, 20, false, 16, 40, .9f, 1f, true);
            const int aislotHomingCooldown = 0;
            const int homingDelay = 20;

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    projectile.ai[aislotHomingCooldown] = 0;
                    NPC n = Main.npc[foundTarget];
                    BaseAI.ShootPeriodic(projectile, n.position, n.width, n.height, Terraria.ModLoader.ModContent.ProjectileType<Darkray>(), ref shootAI[0], 5, projectile.damage, 24f, true, projectile.Center);
                }
            }
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Zero/ZeroStarP"), 0, projectile, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, Glow, 0, projectile, AAColor.Oblivion, true);
            return false;
        }
    }
}
