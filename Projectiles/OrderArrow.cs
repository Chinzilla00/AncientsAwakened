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
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 211,
				projectile.velocity.X * .5f, projectile.velocity.Y * .5f, 200, Scale: 1.1f);
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
	}
}