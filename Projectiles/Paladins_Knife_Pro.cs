using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Paladins_Knife_Pro : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.friendly = true;
			projectile.aiStyle = -1;
			projectile.timeLeft = 1200;
			projectile.penetrate = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paladin's Knife");
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 1, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 0);
		}
		
		public float targetWhoAmI
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }
		
		private const int alphaReduction = 25;
        private const float maxTicks = 55f;

		public override void AI()
		{
			// Slowly remove alpha as it is present
			if (projectile.alpha > 0)
			{
				projectile.alpha -= alphaReduction;
			}
			// If alpha gets lower than 0, set it to 0
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			targetWhoAmI += 1f;
			// For a little while, the javelin will travel with the same speed, but after this, the javelin drops velocity very quickly.
			if (targetWhoAmI >= maxTicks)
			{
				// Change these multiplication factors to alter the javelin's movement change after reaching maxTicks
				float velXmult = 0.98f; // x velocity factor, every AI update the x velocity will be 98% of the original speed
				float
				velYmult = 0.35f; // y velocity factor, every AI update the y velocity will be be 0.35f bigger of the original speed, causing the javelin to drop to the ground
				targetWhoAmI = maxTicks; // set ai1 to maxTicks continuously
				projectile.velocity.X = projectile.velocity.X * velXmult;
				projectile.velocity.Y = projectile.velocity.Y + velYmult;
			}
			// Make sure to set the rotation accordingly to the velocity, and add some to work around the sprite's rotation
			projectile.rotation =
			projectile.velocity.ToRotation() +
			MathHelper.ToRadians(90f); // Please notice the MathHelper usage, offset the rotation by 90 degrees (to radians because rotation uses radians) because the sprite's rotation is not aligned!
		}
	}
}