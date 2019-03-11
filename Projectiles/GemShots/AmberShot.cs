using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class AmberShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.AmberBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}