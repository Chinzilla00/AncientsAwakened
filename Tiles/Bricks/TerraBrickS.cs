using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    public class TerraBrickS : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = true;
            soundType = 21;
            Main.tileBlockLight[Type] = true;
            dustType = 107;
            AddMapEntry(new Color(40, 80, 40));
        }
    }
}