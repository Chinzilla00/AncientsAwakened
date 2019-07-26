using Microsoft.Xna.Framework;
using AAMod;

namespace AAMod.Tiles
{
    class GreedCoin : FallingBlock
    {
        public override int ItemDropID => mod.ItemType("CovetiteCoin");
        public override int ItemProjectileID => mod.ProjectileType("GreedCoinProj");
        public override bool SandTile => false;
        public override Color MapColor => Color.Goldenrod;
        public override string MapLegend => "Covetite Coin";
    }
}
