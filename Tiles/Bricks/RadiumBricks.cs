using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class RadiumBricks : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("RadiumBricks");   
            AddMapEntry(Color.DarkGoldenrod);
            dustType = ModContent.DustType<Dusts.RadiumDust>();
        }
    }
}
