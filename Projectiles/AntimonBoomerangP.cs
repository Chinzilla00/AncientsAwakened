using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AntimonBoomerangP : ModProjectile
	{
		public override void SetDefaults()
		{

			projectile.width = 18;
			projectile.height = 40;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
			projectile.light = 0.9f;
			projectile.extraUpdates = 1;
			
			
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("AntimonBoomerangP");
    }

       

    }
}
