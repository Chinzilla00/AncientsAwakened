using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class OmegaBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Bullet");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            projectile.friendly = false;
            projectile.hostile = true;
            aiType = ProjectileID.Bullet;
        }
    }
}
