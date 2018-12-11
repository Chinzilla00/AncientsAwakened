using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Infinity
{
    public class Supernova : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supernova");     //The English name of the projectile
            Main.projFrames[projectile.type] = 7;     //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 4;
            projectile.scale *= .2f;
            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            projectile.alpha = 0;
        }

        public override void AI()
        {
            projectile.alpha += 10;
            projectile.scale += .2f;
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X += 0.2f;
            projectile.velocity.Y = 0.00f;

        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

    }
}
