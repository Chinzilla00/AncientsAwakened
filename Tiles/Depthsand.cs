using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthsand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModCactus(new Bogtus());
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthsand");   //put your CustomBlock name
            AddMapEntry(new Color(0, 30, 127));
        }
    }
}