namespace AAMod.Tiles
{
    class DiamondCoinProjectile : FallingProjectile
    {
        public override string name => "Covetite Coin";
        public override int Tile => mod.TileType("GreedCoin");
    }
}
