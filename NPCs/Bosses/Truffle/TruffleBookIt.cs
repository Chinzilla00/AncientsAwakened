using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleBookIt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TruffleBookIt");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.damage = 24;
            projectile.width = 66;
            projectile.height = 104;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y -= .1f;
        }
    }
}