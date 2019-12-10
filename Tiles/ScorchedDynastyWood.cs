using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class ScorchedDynastyWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("ScorchedDynastyWood");   
            AddMapEntry(new Color(153, 100, 0));
			minPick = 0;
        }
    }
}