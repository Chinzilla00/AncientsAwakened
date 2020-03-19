using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    public class Stars : ModProjectile
	{
        public override void SetDefaults()
		{
            projectile.width = 26;
            projectile.height = 26;
            projectile.alpha = 30;
            projectile.light = 0.2f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            int stardust = ModContent.DustType<Dusts.StarDust>();
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, stardust, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, stardust, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void Kill(int timeleft)
        {
            int stardust = mod.DustType("StarDust");
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, stardust, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, stardust, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
