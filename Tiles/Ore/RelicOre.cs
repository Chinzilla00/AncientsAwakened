using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class RelicOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileValue[Type] = 370; 
            Main.tileSpelunker[Type] = true;
            drop = mod.ItemType("VikingRelic");   
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Viking Relic");
            AddMapEntry(new Color(58, 68, 102), name);
			minPick = 65;
        }
    }
}