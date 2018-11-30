using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class PrismBolt : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.friendly = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prism Bolt");

		}

        public override void AI()
        {
            for (int num339 = 0; num339 < 4; num339++)
            {
                Dust dust1;
                Dust dust2;
                Dust dust3;
                Dust dust4;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust2 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust3 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust4 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.alpha = 10;
                dust2.alpha = 10;
                dust3.alpha = 10;
                dust4.alpha = 10;
                dust1.noGravity = true;
                dust2.noGravity = true;
                dust3.noGravity = true;
                dust4.noGravity = true;
            }
        }

		public override void Kill(int timeLeft)
		{
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust2 = Main.dust[Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
        }
	}
}