using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TheIlluminatiP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
			projectile.CloneDefaults(ProjectileID.ValkyrieYoyo);
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("illuminatiP");
    }


    }
}
