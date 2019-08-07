using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class YtriumOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = false;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("YtriumOre");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Yttrium Ore");
            AddMapEntry(new Color(160, 150, 0), name);
            dustType = mod.DustType<Dusts.YtriumDust>();
			minPick = 65;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

    }
}