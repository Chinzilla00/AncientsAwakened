using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    class TitanAxeEX : ModProjectile
	{
		public override void SetDefaults()
		{
			// while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
			projectile.width = 94;
			projectile.height = 96;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
            projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            aiType = ProjectileID.PaladinsHammerFriendly;
            drawOffsetX = 5;
			drawOriginOffsetY = 5;
		
		}

        public override void AI()
        {
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector14, Main.DiscoR, 0.8f, Main.DiscoB);
            for (int i = 0; i < 5; i++)
            {
                int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 63, projectile.velocity.X, projectile.velocity.Y, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 2f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= projectile.velocity;
            }
        }
    }
}
