using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BaneR: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of the Bunny");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.aiStyle = 1;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.BoneJavelin;
        }

        public override void PostAI()
        {
            projectile.damage = 100;
        }
    }
}
