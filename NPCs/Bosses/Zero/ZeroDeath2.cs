using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroDeath2 : ModProjectile
    {
        bool slayer = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.projFrames[projectile.type] = 30;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            AAWorld.downedZeroA = true;
            if (++projectile.frameCounter >= 15)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 30)
                {
                    projectile.frame = 29;
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y += 0.00f;
            if (projectile.timeLeft == 913)
            {
                Main.NewText("DISTRESS SIGNAL RECIEVED.", Color.Red.R, Color.Red.G, Color.Red.B);
                
            }
        }
        public override void Kill(int timeLeft)
        {
            AAMod.Slayer = false;
            
        }
    }
}