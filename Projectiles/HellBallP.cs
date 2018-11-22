using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HellBallP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LightDisc);
            projectile.penetrate = 1;  
            projectile.width = 32;
            projectile.height = 32;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("HellBallP");
    }


    }
}
