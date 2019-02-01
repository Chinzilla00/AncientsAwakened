using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using BaseMod;

namespace AAMod.NPCs.Bosses.Kraken
{
    public class Tentacle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kraken Tentacle");
            Main.projFrames[projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 90;
            projectile.height = 62;
			projectile.aiStyle = -1;
           // projectile.alpha = 255;
        }

        public override void AI()
        {
            projectile.ai[1]++;
            if (projectile.ai[1] > 5)
            {
                projectile.frame += 1;
                projectile.ai[1] = 0;
                projectile.ai[0] += 1;
            }

            if (projectile.frame >= 29)
            {
                projectile.active = false;
            }
            int foundTarget = HomeOnTarget();
            if (foundTarget != -1)
            {
                Player n = Main.player[foundTarget];
                if (projectile.ai[0] < 11)
                {
                    projectile.Center = new Vector2(n.Center.X + 80, n.Center.Y);
                }
                else
                {
                    projectile.velocity.X = 0;
                    projectile.velocity.Y = 0;
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < 2; i++)
            {
                Player n = Main.player[i];
                if (!n.wet || homingCanAimAtWetEnemies)
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.player[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
