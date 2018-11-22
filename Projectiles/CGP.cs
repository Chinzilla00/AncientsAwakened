using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CGP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.NightBeam);
            projectile.penetrate = 3;  
            projectile.width = 24;
            projectile.height = 24;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("CGP");
    }


    }
}
