using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class SkyCrystal : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 825; 
            Main.tileBlendAll[Type] = false;
            soundType = 21;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("SkyCrystal"); 
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("SkyCrystal");
            dustType = DustID.BlueCrystalShard;
            AddMapEntry(Color.SkyBlue);
            minPick = 240;
        }
    }
}