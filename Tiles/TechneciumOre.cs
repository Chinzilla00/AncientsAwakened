using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TechneciumOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("TechneciumOre");   //put your CustomBlock name
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Technecium Ore");
            AddMapEntry(new Color(100, 200, 200), name);
			minPick = 150;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}