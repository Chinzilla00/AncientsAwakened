using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Monarch
{
    public class SporeCloud : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fungus Cloud");
            Main.projFrames[projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.scale = .8f;
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            projectile.velocity *= 0;
            projectile.alpha += 3;
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