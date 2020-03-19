using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Crystal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TerraBeam);
            projectile.penetrate = 2;  
            projectile.width = 20;
            projectile.height = 20;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
            projectile.melee = false;
            projectile.magic = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 0, 0, 27, 4.736842f, 0f, 46, new Color(30, 30, 30), 1.184211f)];
                dust.fadeIn = 0.9868421f;
                dust.noGravity = false;
            }
		}

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 27, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 46, new Color(30, 30, 30), 1.184211f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 27, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 46, new Color(30, 30, 30), 1.184211f);
                Main.dust[num469].velocity *= 2f;
            }
        }


        public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Crystal");
		}


    }
}
