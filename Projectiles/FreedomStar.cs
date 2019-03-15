using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class FreedomStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FreedomStar");
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }
    }
}
