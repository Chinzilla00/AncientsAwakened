using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RadiumArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Arrow");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.penetrate = 1;
            projectile.timeLeft = 600;
			projectile.light = 2f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.JestersArrow;
            projectile.arrow = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.StarDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
