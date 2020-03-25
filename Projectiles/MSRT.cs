using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MSRT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Tear");     
            Main.projFrames[projectile.type] = 10;     
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 42;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 30;
            projectile.melee = true;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 9)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

    }
}
