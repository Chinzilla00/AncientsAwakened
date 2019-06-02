using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SpookerangP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.PossessedHatchet);
            projectile.penetrate = 6;  
            projectile.width = 32;
            projectile.height = 32;
            aiType = ProjectileID.PossessedHatchet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SpookerangP");
        }


    }
}
