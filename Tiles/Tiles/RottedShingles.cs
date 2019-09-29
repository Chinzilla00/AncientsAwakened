using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Tiles
{
    public class RottedShingles : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            drop = mod.ItemType("RottedShingles");   //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 50));
			minPick = 0;
        }
    }
}