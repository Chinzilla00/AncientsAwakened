using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class IceChunk : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LightBeam);
            projectile.penetrate = 1;  
            projectile.width = 18;
            projectile.height = 18;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 300;
        }
		
		public override void AI()
		{
            projectile.alpha = 0;
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = projectile.position;
				dust = Main.dust[Dust.NewDust(position, 30, 30, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 0, default, 1)];
				dust.noGravity = true;
			}
            projectile.timeLeft--;
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.SnowDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.IceDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Chunk");
		}


    }
}
