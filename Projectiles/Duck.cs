using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class Duck : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 14;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.RocketSnowmanIV);
            aiType = ProjectileID.RocketSnowmanIII;
            projectile.width = 28;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.alpha = 0;
            projectile.penetrate = 1;
            projectile.timeLeft = 900;
            projectile.friendly = true;
            projectile.hostile = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(100, 200, 0, 0) * (1f - (projectile.alpha / 255f));
        }
    }
}