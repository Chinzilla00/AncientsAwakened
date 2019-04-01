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
			minPick = 100;
        }
      
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.5f;
            g = 0.5f;
            b = 0f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

    }
}