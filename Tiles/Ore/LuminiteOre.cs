using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles.Ore
{
    public class LuminiteOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileValue[Type] = 820; 
            drop = ItemID.LunarOre;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Luminite Ore");
            dustType = ModContent.DustType<Dusts.LuminiteDust>();
            soundType = SoundID.Tink;
            AddMapEntry(new Color(0, 90, 60), name);
			minPick = 225;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = .90f;
            b = .60f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}