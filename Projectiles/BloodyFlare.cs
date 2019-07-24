using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class BloodyFlare : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Flare");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(356);  
			projectile.melee = true;   
			projectile.timeLeft = 600;           
		}
		
		public override void AI()
		{
			projectile.ai[1] += 1f;
			int num3;
			if (projectile.ai[1] >= 60f)
			{
				projectile.friendly = true;
				int num570 = (int)projectile.ai[0];
				if (!Main.npc[num570].active)
				{
					int[] array2 = new int[200];
					int num571 = 0;
					for (int num572 = 0; num572 < 200; num572 = num3 + 1)
					{
						if (Main.npc[num572].CanBeChasedBy(projectile, false))
						{
							float num573 = Math.Abs(Main.npc[num572].position.X + Main.npc[num572].width / 2 - projectile.position.X + projectile.width / 2) + Math.Abs(Main.npc[num572].position.Y + Main.npc[num572].height / 2 - projectile.position.Y + projectile.height / 2);
							if (num573 < 800f)
							{
								array2[num571] = num572;
								num571++;
							}
						}
						num3 = num572;
					}
					if (num571 == 0)
					{
						projectile.Kill();
						return;
					}
					num570 = array2[Main.rand.Next(num571)];
					projectile.ai[0] = num570;
				}
				float num574 = 4f;
				Vector2 vector45 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
				float num575 = Main.npc[num570].Center.X - vector45.X;
				float num576 = Main.npc[num570].Center.Y - vector45.Y;
				float num577 = (float)Math.Sqrt(num575 * num575 + num576 * num576);
				num577 = num574 / num577;
				num575 *= num577;
				num576 *= num577;
				int num578 = 30;
				projectile.velocity.X = (projectile.velocity.X * (num578 - 1) + num575) / num578;
				projectile.velocity.Y = (projectile.velocity.Y * (num578 - 1) + num576) / num578;
			}
			for (int num579 = 0; num579 < 5; num579 = num3 + 1)
			{
				float num580 = projectile.velocity.X * 0.2f * num579;
				float num581 = -(projectile.velocity.Y * 0.2f) * num579;
				int num582 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("DiscordLight"), 0f, 0f, 100, Color.Red, 1.3f);
				Main.dust[num582].noGravity = true;
				Dust dust = Main.dust[num582];
				dust.velocity *= 0f;
				Dust D1 = Main.dust[num582];
				D1.position.X -= num580;
				Dust D2 = Main.dust[num582];
				D2.position.Y -= num581;
				num3 = num579;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.immune[projectile.owner] = 1;
        }
    }
}
