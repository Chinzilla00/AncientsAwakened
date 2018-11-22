using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DarkmatterOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            soundType = 21;
            dustType = mod.DustType("DarkmatterDust");
            drop = mod.ItemType("DarkmatterOre");
            AddMapEntry(new Color(20, 20, 21));
			minPick = 225;
        }
      
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = .30f;
            b = .60f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}