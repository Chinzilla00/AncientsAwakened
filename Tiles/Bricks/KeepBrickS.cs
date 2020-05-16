using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    public class KeepBrickS : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = true;
            soundType = SoundID.Tink;
            Main.tileBlockLight[Type] = true;
            dustType = DustID.Stone;
            AddMapEntry(new Color(40, 50, 40));
        }
    }
}