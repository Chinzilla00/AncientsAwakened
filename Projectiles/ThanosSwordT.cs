using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    class ThanosSwordT : ModProjectile
	{
		public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            projectile.width = 75;
			projectile.height = 75;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
            projectile.timeLeft = 300;
            aiType = ProjectileID.WoodenBoomerang;
		}
	}
}
