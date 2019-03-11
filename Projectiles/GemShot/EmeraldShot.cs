using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.GemShot
{
    class EmeraldShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emerald Bolt");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EmeraldBolt);
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}