using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DeathDagger : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.VampireKnife);
            projectile.friendly = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Dagger");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}