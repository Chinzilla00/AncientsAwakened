using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class FulgurGlass : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            //true for block to emit light
            soundType = 21;
            drop = mod.ItemType("FulguritePlating");   //put your CustomBlock name
            dustType = mod.DustType("FulguriteDust");
            AddMapEntry(new Color(90, 20, 120));
			minPick = 200;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}