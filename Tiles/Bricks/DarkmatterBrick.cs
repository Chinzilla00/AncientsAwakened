using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class DarkmatterBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("DarkmatterBrick");   
            AddMapEntry(new Color(30, 30, 51));
            dustType = ModContent.DustType<Dusts.DarkmatterDust>();
        }
    }
}
