using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Djinn
{
    public class DesertGust : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Gust");
            Main.projFrames[projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -11;
            projectile.extraUpdates = 1;
            projectile.scale = 1.1f;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.98f;
            projectile.alpha += 2;
            if (projectile.alpha > 255)
            {
                projectile.Kill();
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4) 
                    projectile.frame = 0; 
            }
            return true;
        }
    }
}