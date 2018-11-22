using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroDeath2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.projFrames[projectile.type] = 29;
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 44;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 87;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 10)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 29)
                {
                    projectile.frame = 28;
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y += 0.00f;
        }
        public override void Kill(int timeLeft)
        {
            if (!AAWorld.downedZeroA && Main.expertMode)
            {
                Main.NewText("DISTRESS SIGNAL RECIEVED.", Color.Red.R, Color.Red.G, Color.Red.B);
                AAWorld.downedZeroA = true;
            }
        }
    }
}