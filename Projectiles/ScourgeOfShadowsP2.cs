using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class ScourgeOfShadowsP2 : ModProjectile
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
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
            }
            else
            {
                projectile.extraUpdates = 0;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 2)
            {
                projectile.frame = 0;
            }
            for (int num363 = 0; num363 < 3; num363++)
            {
                float num364 = projectile.velocity.X / 3f * num363;
                float num365 = projectile.velocity.Y / 3f * num363;
                int num366 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 0);
                Main.dust[num366].position.X = projectile.Center.X - num364;
                Main.dust[num366].position.Y = projectile.Center.Y - num365;
                Main.dust[num366].velocity *= 0f;
                Main.dust[num366].scale = 0.5f;
            }
            float num367 = projectile.position.X;
            float num368 = projectile.position.Y;
            float num369 = 100000f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 30f)
            {
                projectile.ai[0] = 30f;
                for (int num370 = 0; num370 < 200; num370++)
                {
                    if (Main.npc[num370].CanBeChasedBy(this, false))
                    {
                        float num371 = Main.npc[num370].position.X + Main.npc[num370].width / 2;
                        float num372 = Main.npc[num370].position.Y + Main.npc[num370].height / 2;
                        float num373 = Math.Abs(projectile.position.X + projectile.width / 2 - num371) + Math.Abs(projectile.position.Y + projectile.height / 2 - num372);
                        if (num373 < 800f && num373 < num369 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num370].position, Main.npc[num370].width, Main.npc[num370].height))
                        {
                            num369 = num373;
                            num367 = num371;
                            num368 = num372;
                        }
                    }
                }
            }
            projectile.friendly = true;
            float num374 = 9f;
            float num375 = 0.2f;
            Vector2 vector27 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num376 = num367 - vector27.X;
            float num377 = num368 - vector27.Y;
            float num378 = (float)Math.Sqrt(num376 * num376 + num377 * num377);
            num378 = num374 / num378;
            num376 *= num378;
            num377 *= num378;
            if (projectile.velocity.X < num376)
            {
                projectile.velocity.X = projectile.velocity.X + num375;
                if (projectile.velocity.X < 0f && num376 > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + num375 * 2f;
                }
            }
            else if (projectile.velocity.X > num376)
            {
                projectile.velocity.X = projectile.velocity.X - num375;
                if (projectile.velocity.X > 0f && num376 < 0f)
                {
                    projectile.velocity.X = projectile.velocity.X - num375 * 2f;
                }
            }
            if (projectile.velocity.Y < num377)
            {
                projectile.velocity.Y = projectile.velocity.Y + num375;
                if (projectile.velocity.Y < 0f && num377 > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num375 * 2f;
                    return;
                }
            }
            else if (projectile.velocity.Y > num377)
            {
                projectile.velocity.Y = projectile.velocity.Y - num375;
                if (projectile.velocity.Y > 0f && num377 < 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num375 * 2f;
                    return;
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
				int num623 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 0);
				Dust dust = Main.dust[num623];
				dust.scale *= 1.1f;
				Main.dust[num623].noGravity = true;
				num3 = num622;
			}
			for (int num624 = 0; num624 < 30; num624 = num3 + 1)
			{
				int num625 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 0);
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
					int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num628, num629, ModContent.ProjectileType<CursedFireball>(), projectile.damage*3, (int)(projectile.knockBack * 0.35), Main.myPlayer, 0f, 0f);
					num3 = num627;
					Main.projectile[p].timeLeft = 240;
				}
			}
		}
    }
}