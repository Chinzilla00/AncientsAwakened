using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class CovetiteOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("CovetiteOre"); 
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Covetite");
            dustType = DustID.Gold;
            AddMapEntry(new Color(150, 130, 50), name);
			minPick = 180;
            soundType = 21;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}