using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Mycelium : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new MushroomTree());
            Main.tileBlendAll[this.Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = mod.DustType("MushDust");
            AddMapEntry(new Color(120, 60, 0));
            drop = ItemID.DirtBlock;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(50) == 0)
            {
                PlaceObject(i, j - 1, mod.TileType("Mushroom"));
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("Mushroom"), 0, 0, -1, -1);
            }

            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(200) == 0)
            {
                WorldGen.PlaceObject(i, j, mod.TileType("MushroomTree"));
                WorldGen.GrowTree(i, j);
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MushroomTree"), 0, 0, -1, -1);
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("M");
        }
    }
}