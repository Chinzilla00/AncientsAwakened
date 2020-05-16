using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class Doomstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Apocalyptite")] = true;
            Main.tileMergeDirt[Type] = true;
            SetModTree(new OroborosTree());
            soundType = SoundID.Tink;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("Doomstone");   
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(21, 21, 31));
			minPick = 225;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return AAWorld.downedZero;
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}