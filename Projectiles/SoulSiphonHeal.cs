using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SoulSiphonHeal : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heal");
		}

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.extraUpdates = 10;
        }

        public override void AI()
        {
            int player = (int)projectile.ai[0];
            float num488 = 5.5f;
            Vector2 vector36 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num489 = Main.player[player].Center.X - vector36.X;
            float num490 = Main.player[player].Center.Y - vector36.Y;
            float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);
            if (num491 < 50f && projectile.position.X < Main.player[player].position.X + Main.player[player].width && projectile.position.X + projectile.width > Main.player[player].position.X && projectile.position.Y < Main.player[player].position.Y + Main.player[player].height && projectile.position.Y + projectile.height > Main.player[player].position.Y)
            {
                if (projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int Heal = (int)projectile.ai[1];
                    Main.player[player].HealEffect(Heal, false);
                    Main.player[player].statLife += Heal;
                    if (Main.player[player].statLife > Main.player[player].statLifeMax2)
                    {
                        Main.player[player].statLife = Main.player[player].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, player, Heal, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }
            num491 = num488 / num491;
            num489 *= num491;
            num490 *= num491;
            projectile.velocity.X = (projectile.velocity.X * 15f + num489) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num490) / 16f;
            for (int num493 = 0; num493 < 3; num493++)
            {
                float num494 = projectile.velocity.X * 0.334f * num493;
                float num495 = -(projectile.velocity.Y * 0.334f) * num493;
                int num496 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 100, default(Color), 1.1f);
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
                int num500 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 100, default(Color), 1.3f);
                Main.dust[num500].noGravity = true;
                Main.dust[num500].velocity *= 0f;
                Dust expr_154F9_cp_0 = Main.dust[num500];
                expr_154F9_cp_0.position.X -= num498;
                Dust expr_15518_cp_0 = Main.dust[num500];
                expr_15518_cp_0.position.Y -= num499;
            }
        }
    }
}