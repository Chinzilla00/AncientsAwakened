using Terraria.ID;

namespace AAMod.NPCs.Bosses.Greed
{
    class CoinRain : FallingProjectile
    {
        public override string name => "Covetite Coin";
        public override int Tile => TileID.GoldCoinPile;


        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.friendly = false;
            projectile.hostile = true;
        }
    }
}
