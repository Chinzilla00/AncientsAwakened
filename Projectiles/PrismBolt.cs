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
            int num338 = projectile.type - 121 + 86;
            for (int num339 = 0; num339 < 2; num339++)
            {
                int num340 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num338, projectile.velocity.X, projectile.velocity.Y, 50, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
                Main.dust[num340].noGravity = true;
                Main.dust[num340].velocity *= 0.3f;
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.position);
                return;
            }
        }

		public override void Kill(int timeLeft)
		{
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            int num505 = projectile.type - 121 + 86;
            for (int num506 = 0; num506 < 15; num506++)
            {
                int num507 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num505, projectile.oldVelocity.X, projectile.oldVelocity.Y, 50, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
                Main.dust[num507].noGravity = true;
                Main.dust[num507].scale *= 1.25f;
                Main.dust[num507].velocity *= 0.5f;
            }
        }
	}
}