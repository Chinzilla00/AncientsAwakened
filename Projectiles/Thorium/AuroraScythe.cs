
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Thorium
{
    public class AuroraScythe : ModProjectile
	{	

		public override void SetDefaults()
		{
			projectile.width = 130;
			projectile.height = 128;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.light = 0.2f;
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
			
			projectile.position.X = player.Center.X - ((float)projectile.width / 2f);
			projectile.position.Y = player.Center.Y - ((float)projectile.height / 2f);
			
			Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("AuroraScytheDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("AuroraScytheDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			
			if (projectile.timeLeft == 13)
			{
				Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("AuroraScytheDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
				Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("AuroraScytheDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
			}
			
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