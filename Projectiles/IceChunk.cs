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
            projectile.width = 14;
            projectile.height = 18;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = projectile.position;
				dust = Main.dust[Dust.NewDust(position, 30, 30, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 0, default(Color), 1)];
				dust.noGravity = true;
			}
		}

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.SnowDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.IceDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Chunk");
		}


    }
}
