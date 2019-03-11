using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class AmethystShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amethyst Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.AmethystBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}