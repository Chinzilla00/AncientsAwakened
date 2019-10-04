using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class AmberArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amber Arrow");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BoneArrow);
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = 5;
			projectile.timeLeft = 600;
			aiType = 1;
            projectile.arrow = true;
        }


        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Dirt, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}