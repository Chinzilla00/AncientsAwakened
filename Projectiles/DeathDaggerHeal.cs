using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DeathDaggerHeal : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heal");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 480;
        }

        public override void AI()
        {
			projectile.velocity.X *= 1.01f;
			projectile.velocity.Y *= 1.01f;
			int num487 = (int)projectile.ai[0];
			Vector2 vector36 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num490 = Main.player[num487].Center.Y - vector36.Y;
			float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);
			if (num491 < 50f && projectile.position.X < Main.player[num487].position.X + Main.player[num487].width && projectile.position.X + projectile.width > Main.player[num487].position.X && projectile.position.Y < Main.player[num487].position.Y + Main.player[num487].height && projectile.position.Y + projectile.height > Main.player[num487].position.Y)
			{
				if (projectile.owner == Main.myPlayer)
				{
					Main.player[num487].HealEffect(1, false);
					Main.player[num487].statLife += 1;
					if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
					{
						Main.player[num487].statLife = Main.player[num487].statLifeMax2;
					}
					NetMessage.SendData(66, -1, -1, null, num487, 1, 0f, 0f, 0, 0, 0);
				}
				projectile.Kill();
            }
            float num488 = 5.5f;
            num491 = num488 / num491;
            num489 *= num491;
            num490 *= num491;
            projectile.velocity.X = (projectile.velocity.X * 15f + num489) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num490) / 16f;
            for (int num493 = 0; num493 < 3; num493++)
            {
                float num494 = projectile.velocity.X * 0.334f * num493;
                float num495 = -(projectile.velocity.Y * 0.334f) * num493;
                int num496 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 1.1f);
                Main.dust[num496].noGravity = true;
                Main.dust[num496].velocity *= 0f;
                Dust expr_153E2_cp_0 = Main.dust[num496];
                expr_153E2_cp_0.position.X -= num494;
                Dust expr_15401_cp_0 = Main.dust[num496];
                expr_15401_cp_0.position.Y -= num495;
            }
            for (int num497 = 0; num497 < 5; num497++)
            {
                float num498 = projectile.velocity.X * 0.2f * num497;
                float num499 = -(projectile.velocity.Y * 0.2f) * num497;
                int num500 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 1.3f);
                Main.dust[num500].noGravity = true;
                Main.dust[num500].velocity *= 0f;
                Dust expr_154F9_cp_0 = Main.dust[num500];
                expr_154F9_cp_0.position.X -= num498;
                Dust expr_15518_cp_0 = Main.dust[num500];
                expr_15518_cp_0.position.Y -= num499;
            }
        }

        public override void Kill(int timeLeft)
        {
        	Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
			projectile.position.X = projectile.position.X + projectile.width / 2;
			projectile.position.Y = projectile.position.Y + projectile.height / 2;
			projectile.width = 50;
			projectile.height = 50;
			projectile.position.X = projectile.position.X - projectile.width / 2;
			projectile.position.Y = projectile.position.Y - projectile.height / 2;
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, default, 2f);
				Main.dust[num624].velocity *= 2f;
			}
        }
    }
}