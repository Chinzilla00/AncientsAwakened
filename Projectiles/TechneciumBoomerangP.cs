using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TechneciumBoomerangP : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 40;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 7;
			projectile.timeLeft = 500;
			projectile.light = 0.9f;
			projectile.extraUpdates = 3;
			
			
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("tBoomerangP");
    }

       

    }
}
