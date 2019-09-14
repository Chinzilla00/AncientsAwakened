using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OrderArrow : ModProjectile
	{
		public static int defense = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Order Arrow");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(1);
			projectile.width = 14;
			projectile.height = 18;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = 1;
            projectile.arrow = true;
        }

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 107,
				projectile.velocity.X * .5f, projectile.velocity.Y * .5f, 200, Scale: .6f);
				dust.velocity += projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
		}

		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.defense = target.defDefense - 30;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			target.defense = defense;
		}

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 107, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, .6f);
            }
        }

    }
}