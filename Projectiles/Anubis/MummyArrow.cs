using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis
{
    public class MummyArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mummy Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
            projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
            projectile.arrow = true;
		}

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 32, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
