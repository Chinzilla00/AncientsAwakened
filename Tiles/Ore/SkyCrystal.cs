using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Main.tileSolid[Type] = true;
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