using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class HydraShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Shot");
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            projectile.velocity *= 0.95f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] == 180f)
            {
                projectile.Kill();
            }

            Dust.NewDust(projectile.position, projectile.width, projectile.height, 88, 0, 0, 50);
        }
    }
}
