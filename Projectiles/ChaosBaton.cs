using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosBaton : ModProjectile
	{	

		public override void SetDefaults()
		{
			projectile.width = 130;
			projectile.height = 130;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 26;
			aiType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];	
			
			projectile.ai[0]++;
			
			if (player.dead)
			{
				projectile.Kill();
				return;
			}
			
			if (player.direction > 0)
			{
				projectile.rotation += 0.35f;
				projectile.spriteDirection = 1;
			}
			else
			{
				projectile.rotation -= 0.35f;
				projectile.spriteDirection = -1;
			}
			
			projectile.position.X = player.Center.X - (projectile.width / 2f);
			projectile.position.Y = player.Center.Y - (projectile.height / 2f);
			
			if (projectile.timeLeft < 8)
			{
				projectile.alpha = 100;
			}
			if (projectile.timeLeft < 6)
			{
				projectile.alpha = 140;
			}
			if (projectile.timeLeft < 4)
			{
				projectile.alpha = 180;
			}
			if (projectile.timeLeft < 2)
			{
				projectile.alpha = 220;
			}
		}
	}
}