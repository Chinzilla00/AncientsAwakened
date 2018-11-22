using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CloudEdgeP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Starfury);
            projectile.penetrate = 14;  
            projectile.width = 14;
            projectile.height = 18;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("CGP");
    }


    }
}
