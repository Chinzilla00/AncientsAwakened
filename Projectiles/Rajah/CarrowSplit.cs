using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah
{
    public class CarrowSplit : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrow");
		}

		public override void SetDefaults()
		{
            projectile.melee = true;
			projectile.width = 10; 
			projectile.height = 10; 
			projectile.aiStyle = 1;   
			projectile.friendly = true; 
			projectile.hostile = false;  
			projectile.penetrate = -1;  
			projectile.timeLeft = 600;  
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			aiType = ProjectileID.WoodenArrowFriendly;
            projectile.noDropItem = true;
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.CarrotDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
