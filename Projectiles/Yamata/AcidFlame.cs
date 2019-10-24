using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    public class AcidFlame : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Flame");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.timeLeft = 45;
        }

        public override void AI()
        {
        	Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0f / 255f, (255 - projectile.alpha) * 0.2f / 255f, (255 - projectile.alpha) * 0.45f / 255f);
			if (projectile.timeLeft > 45)
			{
				projectile.timeLeft = 45;
			}
			if (projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				projectile.ai[0] += 1f;
				int num297 = ModContent.DustType<Dusts.YamataDust>();
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.75f);
						if (num297 == 66 && Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 2f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X *= 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y *= 2f;
						}
						else
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 0.8f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X *= 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y *= 1.2f;
						Main.dust[num299].scale *= num296;
						if (num297 == 66)
						{
							Main.dust[num299].velocity += projectile.velocity;
						}
					}
				}
			}
			else
			{
				projectile.ai[0] += 1f;
			}
			projectile.rotation += 0.3f * projectile.direction;
			return;	
        }

           public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(target.life<=0)
           {
              Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("AEBoom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);             
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 15;
            double offsetAngle;
            int i;
            for (i = 0; i < 7; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType("AcidBall"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 7f), (float)(-Math.Cos(offsetAngle) * 7f), mod.ProjectileType("AcidBall"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
           }
        }
    }
}
