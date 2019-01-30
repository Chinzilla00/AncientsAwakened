using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppy
{
    public class Pellet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pellet");
		}

		public override void SetDefaults()
		{
			projectile.width = 10; 
			projectile.height = 10; 
			projectile.aiStyle = 1;   
			projectile.friendly = true; 
			projectile.hostile = false; 
			projectile.magic = true;   
			projectile.penetrate = 1;  
			projectile.timeLeft = 600;  
			projectile.alpha = 255; 
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

        private bool Spawn = false;

        public override void AI()
        {
            projectile.rotation += .5f;
            if (Spawn == false)
            {
                projectile.alpha -= 10;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    Spawn = true;
                }
                if (Spawn == true)
                {
                    projectile.alpha += 10;
                    if (projectile.alpha >= 255)
                    {
                        projectile.active = false;
                    }
                }
            }
        }
    }
}
