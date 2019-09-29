using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Tiles
{
    public class OroborosWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            soundType = 21;
            drop = mod.ItemType("OroborosWood");   //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(60, 60, 60));
        }
    }
}