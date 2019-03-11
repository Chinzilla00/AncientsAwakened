using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class TopazShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Topaz Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TopazBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}