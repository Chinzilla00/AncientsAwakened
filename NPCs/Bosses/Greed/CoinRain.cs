using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    class CoinRain : ModProjectile
    { 

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coin");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.damage = 0;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.aiStyle = 10;
            aiType = ProjectileID.GoldCoinsFalling;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override void Kill(int timeLeft)
        {
            WorldGen.PlaceTile((int)(projectile.position.X / 16), (int)(projectile.position.Y / 16), TileID.GoldCoinPile);
        }
    }
}
