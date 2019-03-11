using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class DiamondShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DiamondBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}