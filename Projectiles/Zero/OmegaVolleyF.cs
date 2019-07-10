using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class OmegaVolleyF : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.light = 0.3f;
            projectile.alpha = 255;
            projectile.extraUpdates = 7;
            projectile.scale = 1.18f;
            projectile.timeLeft = 300;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Omega Shot");
		}
    }
}
