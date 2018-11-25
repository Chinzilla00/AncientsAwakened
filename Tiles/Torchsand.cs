using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchsand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModCactus(new Razetus());
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchsandstone");   //put your CustomBlock name
            AddMapEntry(new Color(50, 35, 22));
        }
    }
}