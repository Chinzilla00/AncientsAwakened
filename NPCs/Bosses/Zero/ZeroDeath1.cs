using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroDeath1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 178;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 24;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 10)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                   projectile.frame = 7;
                   
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y += 0.00f;
           
        }
        public override void Kill(int timeLeft)
        {
            if (!AAWorld.downedZeroA && Main.expertMode)
            {
                Main.NewText("SENDING...", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            Projectile.NewProjectile((new Vector2(projectile.position.X, projectile.position.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("ZeroDeath2"), 0, 0);
        }
    }
}