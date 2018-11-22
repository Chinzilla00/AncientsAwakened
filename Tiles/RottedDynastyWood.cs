using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RottedDynastyWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[this.Type] = false;
            Main.tileBlockLight[Type] = false;
            
            drop = mod.ItemType("RottedDynastyWood");   //put your CustomBlock name
            AddMapEntry(new Color(0, 30, 120));
			minPick = 0;
        }
    }
}