using Terraria.ModLoader;

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
            projectile.ai[0] += 1f;

            if(projectile.ai[0] >= 180f)
            {
                projectile.Kill();
            }
        }
    }
}
