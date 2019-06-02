using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Cloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Starfury);
            projectile.penetrate = 14;  
            projectile.width = 14;
            projectile.height = 18;
            projectile.melee = false;
            projectile.magic = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CGP");
        }


    }
}
