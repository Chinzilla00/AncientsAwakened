using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class BigCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.penetrate = -1;  
            projectile.width = 44;
            projectile.height = 44;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
            projectile.magic = true;
        }

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Crystal");
		}


    }
}
