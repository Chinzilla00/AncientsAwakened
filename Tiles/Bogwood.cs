using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class Bogwood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("Bogwood");   //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 51));
            minPick = 65;
            dustType = mod.DustType("BogwoodDust");
        }
    }
}
