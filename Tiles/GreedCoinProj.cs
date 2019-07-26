namespace AAMod.Tiles
{
    class GreedCoinProj : FallingProjectile
    {
        public override string name => "Covetite Coin";
        public override int Tile => mod.TileType("GreedCoin");
    }
}
