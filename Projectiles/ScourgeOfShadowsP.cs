using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class ScourgeOfShadowsP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.alpha = 255;
			projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = 5;
			projectile.melee = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			projectile.timeLeft = 300;
        }
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
				
			}
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			return false;
		}
		
        public override void AI()
        {
			if (projectile.alpha <= 200)
			{
				int num3;
				for (int num20 = 0; num20 < 4; num20 = num3 + 1)
				{
					float num21 = projectile.velocity.X / 4f * num20;
					float num22 = projectile.velocity.Y / 4f * num20;
					int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 184, 0f, 0f, 0);
					Main.dust[num23].position.X = projectile.Center.X - num21;
					Main.dust[num23].position.Y = projectile.Center.Y - num22;
					Dust dust = Main.dust[num23];
					dust.velocity *= 0f;
					Main.dust[num23].scale = 0.7f;
					num3 = num20;
				}
			}
			projectile.alpha -= 50;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
			
			if (Main.rand.Next(30) == 0)
			{
				for (int num627 = 0; num627 < 2; num627++)
				{
					float num628 = Main.rand.Next(-35, 36) * 0.02f;
					float num629 = Main.rand.Next(-35, 36) * 0.02f;
					num628 *= 10f;
					num629 *= 10f;
					int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num628, num629, ProjectileID.TinyEater, projectile.damage, (int)(projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					Main.projectile[p].timeLeft = 180;
				}
			}
        }
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			projectile.Kill();
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			int num3;
			for (int num622 = 0; num622 < 20; num622 = num3 + 1)
			{
				int num623 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, 0f, 0f, 0);
				Dust dust = Main.dust[num623];
				dust.scale *= 1.1f;
				Main.dust[num623].noGravity = true;
				num3 = num622;
			}
			for (int num624 = 0; num624 < 30; num624 = num3 + 1)
			{
				int num625 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, 0f, 0f, 0);
				Dust dust = Main.dust[num625];
				dust.velocity *= 2.5f;
				dust = Main.dust[num625];
				dust.scale *= 0.8f;
				Main.dust[num625].noGravity = true;
				num3 = num624;
			}
			if (projectile.owner == Main.myPlayer)
			{
				int num626 = 2;
				if (Main.rand.Next(10) == 0)
				{
					num626++;
				}
				if (Main.rand.Next(10) == 0)
				{
					num626++;
				}
				if (Main.rand.Next(10) == 0)
				{
					num626++;
				}
				for (int num627 = 0; num627 < num626; num627 = num3 + 1)
				{
					float num628 = Main.rand.Next(-35, 36) * 0.02f;
					float num629 = Main.rand.Next(-35, 36) * 0.02f;
					num628 *= 10f;
					num629 *= 10f;
					int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num628, num629, 307, projectile.damage*3, (int)(projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					num3 = num627;
					Main.projectile[p].timeLeft = 240;
				}
			}
		}
    }
}