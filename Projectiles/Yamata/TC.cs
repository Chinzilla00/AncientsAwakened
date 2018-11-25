using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class TC : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.PossessedHatchet);
			projectile.width = 18;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 6;
			projectile.timeLeft = 550;
			projectile.light = 0.9f;
			projectile.extraUpdates = 2;
			
			
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("tc");
    }

       

    }
}
