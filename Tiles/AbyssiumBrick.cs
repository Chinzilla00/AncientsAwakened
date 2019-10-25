using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class AbyssiumBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("AbyssiumBrick");   //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 51));
            dustType = ModContent.DustType<Dusts.AbyssiumDust>();
        }
    }
}
