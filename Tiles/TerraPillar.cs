
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class TerraPillar : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMergeDirt[Type] = false;
            soundType = SoundID.Tink;
            Main.tileLighted[Type] = true;
            dustType = 107;
            AddMapEntry(Color.DarkGreen);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}