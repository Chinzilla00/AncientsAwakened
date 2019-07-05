using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class HallowedOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("HallowedOre"); 
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Hallowed Ore");
            dustType = DustID.Gold;
            AddMapEntry(new Color(160, 160, 50), name);
			minPick = 180;
        }
      
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.5f;
            g = 0.5f;
            b = 0;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}