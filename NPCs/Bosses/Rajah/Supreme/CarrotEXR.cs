using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Rajah.Supreme
{
    public class CarrotEXR : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrot");
		}

		public override void SetDefaults()
		{
            projectile.melee = true;
			projectile.width = 10; 
			projectile.height = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;  
			projectile.timeLeft = 600;  
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }

        public override void PostAI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Gold, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
