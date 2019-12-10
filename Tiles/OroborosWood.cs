using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class OroborosWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            soundType = 21;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("OroborosWood");   
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(60, 60, 60));
        }
    }
}