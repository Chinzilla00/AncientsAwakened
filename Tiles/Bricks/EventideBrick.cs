using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class EventideBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("EventideBrick");   
            AddMapEntry(Globals.AAColor.Yamata);
            dustType = ModContent.DustType<Dusts.AbyssDust>();
        }
    }
}
