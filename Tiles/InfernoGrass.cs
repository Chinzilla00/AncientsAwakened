using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class InfernoGrass : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new RazewoodTree());
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = mod.DustType("RazeleafDust");
            AddMapEntry(new Color(255, 153, 51));
            drop = ItemID.DirtBlock;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(500) == 0)
            {
                PlaceObject(i, j - 1, mod.TileType("Hotshroom"));
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("Hotshroom"), 0, 0, -1, -1);

            }
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(40) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        PlaceObject(i, j - 1, mod.TileType("InfernoFoliage1"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("InfernoFoliage1"), 0, 0, -1, -1);
                        break;
                    case 1:
                        PlaceObject(i, j - 1, mod.TileType("InfernoFoliage2"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("InfernoFoliage2"), 0, 0, -1, -1);
                        break;
                    case 2:
                        PlaceObject(i, j - 1, mod.TileType("InfernoFoliage3"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("InfernoFoliage3"), 0, 0, -1, -1);
                        break;
                    case 3:
                        PlaceObject(i, j - 1, mod.TileType("InfernoFoliage4"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("InfernoFoliage4"), 0, 0, -1, -1);
                        break;

                    default:
                        PlaceObject(i, j - 1, mod.TileType("InfernoFoliage5"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("InfernoFoliage5"), 0, 0, -1, -1);
                        break;
                }
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
            return mod.TileType("RazewoodSapling");
        }
    }
}