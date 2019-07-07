using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MjolnirEffect : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.melee = true;
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MjolnirEffect");
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				projectile.velocity *= 0.75f;
			}
			return false;
		}
	}
}
