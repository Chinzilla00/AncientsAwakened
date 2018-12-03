using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DepthsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileLighted[Type] = false;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthstone");   //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 127));
			minPick = 65;
        }
    }
}