using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class RubyShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.RubyBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}