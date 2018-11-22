using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class LivingBogleaves : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlendAll[this.Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            dustType = mod.DustType("BogleafDust");
            drop = mod.ItemType("");   //put your CustomBlock name
            AddMapEntry(new Color(70, 0, 127));
			minPick = 0;
        }
    }
}