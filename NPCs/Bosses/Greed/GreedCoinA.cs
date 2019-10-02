using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedCoinA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Platinum Coin");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GoldCoin);
            projectile.friendly = false;
            projectile.hostile = true;
        }
    }
}