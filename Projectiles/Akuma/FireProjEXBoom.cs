using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class FireProjEXBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamesplosion");     //The English name of the projectile
            Main.projFrames[projectile.type] = 5;     //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 80;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

    }
}
