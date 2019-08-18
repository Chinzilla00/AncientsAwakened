using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 10;
            projectile.aiStyle = 1;
            projectile.extraUpdates = 1;
        }
    }
}