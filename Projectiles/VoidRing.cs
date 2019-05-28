using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class VoidRing : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Electrosphere);
            projectile.ranged = false;
            projectile.melee = true;
            projectile.timeLeft = 120;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Nightcrawler;
        }
    }
}