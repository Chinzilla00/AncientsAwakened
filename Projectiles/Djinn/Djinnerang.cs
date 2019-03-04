using Terraria.ModLoader;

namespace AAMod.Projectiles.Djinn
{
    public class Djinnerang : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("yBoomerangP");
        }

        public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.timeLeft = 550;
			projectile.extraUpdates = 2;
            projectile.melee = true;
        }
    }
}
