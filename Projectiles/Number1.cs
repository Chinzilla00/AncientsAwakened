using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Number1 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TiedEighthNote);
            projectile.penetrate = 3;
            projectile.width = 40;
            projectile.height = 40;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Number One");
		}
    }
}