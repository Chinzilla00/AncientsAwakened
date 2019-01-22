using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.SoC
{
    public class RealityBurstMed : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Burst");     //The English name of the projectile
            Main.projFrames[projectile.type] = 4;     //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 112;
            projectile.height = 112;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150);
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }
        
        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

    }
}
