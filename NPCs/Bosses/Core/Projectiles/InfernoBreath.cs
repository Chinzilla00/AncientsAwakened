using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    internal class InfernoBreath : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Breath");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.damage = 35;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 100;
        }

        public override void AI()
		{
			if (projectile.timeLeft > 60)
			{
				projectile.timeLeft = 60;
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
				int num297 = 6;
				for (int num298 = 0; num298 < 1; num298++)
				{
					int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default, 1f);
					if (Main.rand.Next(3) != 0)
					{
						Main.dust[num299].noGravity = true;
						Main.dust[num299].scale *= 3f;
						Dust expr_DD5D_cp_0 = Main.dust[num299];
						expr_DD5D_cp_0.velocity.X = expr_DD5D_cp_0.velocity.X * 2f;
						Dust expr_DD7D_cp_0 = Main.dust[num299];
						expr_DD7D_cp_0.velocity.Y = expr_DD7D_cp_0.velocity.Y * 2f;
					}

					Main.dust[num299].scale *= 1.5f;

					Dust expr_DDE2_cp_0 = Main.dust[num299];
					expr_DDE2_cp_0.velocity.X = expr_DDE2_cp_0.velocity.X * 1.2f;
					Dust expr_DE02_cp_0 = Main.dust[num299];
					expr_DE02_cp_0.velocity.Y = expr_DE02_cp_0.velocity.Y * 1.2f;
					Main.dust[num299].scale *= num296;
				}
			}
			else
			{
				projectile.ai[0] += 1f;
			}
			projectile.rotation += 0.3f * (float)projectile.direction;
			return;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 300);
        }
    }
}