using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class DaybreakBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("DaybreakBrick");   
            AddMapEntry(AAColor.Akuma);
            dustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
        }
    }
}
