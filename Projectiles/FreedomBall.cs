using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class FreedomBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Plasma Ball");
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.alpha = 130;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            float maxTime = 180f;
            projectile.ai[0] += 1f;

            if(projectile.ai[0] >= 180f)
            {
                projectile.Kill();
            }
        }
    }
}
