using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class FungusCloud : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fungus Cloud");
            Main.projFrames[projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -11;
            projectile.extraUpdates = 1;
            projectile.scale = 1.1f;
            projectile.penetrate = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
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

        public override bool PreDraw(SpriteBatch sb, Color lightColor) //this is where the animation happens
        {
            projectile.frameCounter++; //increase the frameCounter by one
            if (projectile.frameCounter >= 5) //once the frameCounter has reached 10 - change the 10 to change how fast the projectile animates
            {
                projectile.frame++; //go to the next frame
                projectile.frameCounter = 0; //reset the counter
                if (projectile.frame > 4) //if past the last frame
                    projectile.frame = 0; //go back to the first frame
            }
            return true;
        }
    }
}