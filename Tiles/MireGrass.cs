using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class MireGrass : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new BogwoodTree());
            Main.tileBlendAll[this.Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            dustType = mod.DustType("AbyssiumDust");
            AddMapEntry(new Color(0, 50, 140));
            drop = ItemID.MudBlock;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(500) == 0)
            {
                PlaceObject(i, j - 1, mod.TileType("Darkshroom"));
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("Darkshroom"), 0, 0, -1, -1);

            }
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(40) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        PlaceObject(i, j - 1, mod.TileType("MireFoliage1"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MireFoliage1"), 0, 0, -1, -1);
                        break;
                    case 1:
                        PlaceObject(i, j - 1, mod.TileType("MireFoliage2"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MireFoliage2"), 0, 0, -1, -1);
                        break;
                    case 2:
                        PlaceObject(i, j - 1, mod.TileType("MireFoliage3"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MireFoliage3"), 0, 0, -1, -1);
                        break;
                    case 3:
                        PlaceObject(i, j - 1, mod.TileType("MireFoliage4"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MireFoliage4"), 0, 0, -1, -1);
                        break;

                    default:
                        PlaceObject(i, j - 1, mod.TileType("MireFoliage5"));
                        NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("MireFoliage5"), 0, 0, -1, -1);
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
            return mod.TileType("BogwoodSapling");
        }
    }
}