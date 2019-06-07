using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

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
            projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.03f * (float)projectile.direction;
            if (projectile.alpha <= 200)
            {
                for (int num19 = 0; num19 < 4; num19++)
                {
                    float num20 = projectile.velocity.X / 4f * (float)num19;
                    float num21 = projectile.velocity.Y / 4f * (float)num19;
                    int num22 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num22].position.X = projectile.Center.X - num20;
                    Main.dust[num22].position.Y = projectile.Center.Y - num21;
                    Main.dust[num22].velocity *= 0f;
                    Main.dust[num22].scale = 0.7f;
                }
            }
            projectile.alpha -= 50;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
			projectile.Kill();
		}
		
		public override void Kill(int timeLeft)
		{
            Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num622].scale *= 1.1f;
                Main.dust[num622].noGravity = true;
            }
            for (int num623 = 0; num623 < 30; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num624].velocity *= 2.5f;
                Main.dust[num624].scale *= 0.8f;
                Main.dust[num624].noGravity = true;
            }
            if (projectile.owner == Main.myPlayer)
            {
                int num625 = 2;
                if (Main.rand.Next(10) == 0)
                {
                    num625++;
                }
                if (Main.rand.Next(10) == 0)
                {
                    num625++;
                }
                if (Main.rand.Next(10) == 0)
                {
                    num625++;
                }
                for (int num626 = 0; num626 < num625; num626++)
                {
                    float num627 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    num627 *= 10f;
                    num628 *= 10f;
                    Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num627, num628, mod.ProjectileType<ScourgeOfShadowsP2>(), projectile.damage, (float)((int)((double)projectile.knockBack * 0.35)), Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}