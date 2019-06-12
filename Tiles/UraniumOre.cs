using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class UraniumOre : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            soundType = 21;
            drop = mod.ItemType("Uranium");   //put your CustomBlock name
            dustType = mod.DustType("UraniumDust");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Uranium Ore");
            AddMapEntry(new Color(2, 150, 0), name);
            minPick = 110;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}