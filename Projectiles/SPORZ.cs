using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles;
using AAMod.Dusts;

namespace AAMod.Projectiles
{
    internal class SPORZ : AASolution
    {
        public override string name => "Spores";
        public override int dust => mod.DustType<MushDust>();
        public override int size => 2;

        public override void Convert(int i, int j)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = (int)Main.tile[k, l].type;
                        if (TileID.Sets.Conversion.Grass[type])
                        {
                            Main.tile[k, l].type = (ushort)mod.TileType<Mycelium>();
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
            }
        }
    }
}

