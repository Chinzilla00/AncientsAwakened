using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class M79P : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("M79 Round");
		}
		
        public override void SetDefaults()
        {
            projectile.CloneDefaults(133);
			projectile.aiStyle = 16;
			aiType = 133;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.type = 133;
			projectile.timeLeft = 3;
			return true;
		}
		
		public override void Kill(int timeLeft)
		{
			projectile.type = 133;
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
			int num3;
			for (int num729 = 0; num729 < 30; num729 = num3 + 1)
			{
				int num730 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default, 1.5f);
				Dust dust = Main.dust[num730];
				dust.velocity *= 1.4f;
				num3 = num729;
			}
			for (int num731 = 0; num731 < 20; num731 = num3 + 1)
			{
				int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default, 3.5f);
				Main.dust[num732].noGravity = true;
				Dust dust = Main.dust[num732];
				dust.velocity *= 7f;
				num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default, 1.5f);
				dust = Main.dust[num732];
				dust.velocity *= 3f;
				num3 = num731;
			}
			for (int num733 = 0; num733 < 2; num733 = num3 + 1)
			{
				float scaleFactor9 = 0.4f;
				if (num733 == 1)
				{
					scaleFactor9 = 0.8f;
				}
				int num734 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
				Gore gore = Main.gore[num734];
				gore.velocity *= scaleFactor9;
				Gore var_503_191DA_cp_0_cp_0 = Main.gore[num734];
				var_503_191DA_cp_0_cp_0.velocity.X += 1f;
				Gore var_503_1920A_cp_0_cp_0 = Main.gore[num734];
				var_503_1920A_cp_0_cp_0.velocity.Y += 1f;
				num734 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num734];
				gore.velocity *= scaleFactor9;
				Gore var_503_192A4_cp_0_cp_0 = Main.gore[num734];
				var_503_192A4_cp_0_cp_0.velocity.X -= 1f;
				Gore var_503_192D4_cp_0_cp_0 = Main.gore[num734];
				var_503_192D4_cp_0_cp_0.velocity.Y += 1f;
				num734 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num734];
				gore.velocity *= scaleFactor9;
				Gore var_503_1936E_cp_0_cp_0 = Main.gore[num734];
				var_503_1936E_cp_0_cp_0.velocity.X += 1f;
				Gore var_503_1939E_cp_0_cp_0 = Main.gore[num734];
				var_503_1939E_cp_0_cp_0.velocity.Y -= 1f;
				num734 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num734];
				gore.velocity *= scaleFactor9;
				Gore var_503_19438_cp_0_cp_0 = Main.gore[num734];
				var_503_19438_cp_0_cp_0.velocity.X -= 1f;
				Gore var_503_19468_cp_0_cp_0 = Main.gore[num734];
				var_503_19468_cp_0_cp_0.velocity.Y -= 1f;
				num3 = num733;
			}
		}
    }
}