using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class ThunderSpark : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thunder Spark");
            Main.projFrames[projectile.type] = 4;

		}

		public override void SetDefaults()
        {
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
        }

        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}