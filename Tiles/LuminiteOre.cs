using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
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
            drop = ItemID.LunarOre;
            dustType = mod.DustType<Dusts.LuminiteDust>();
            soundType = 21;
            AddMapEntry(new Color(0, 90, 60));
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