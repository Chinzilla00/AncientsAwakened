using Microsoft.Xna.Framework;
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
            Main.tileMergeDirt[Type] = false;
			Main.tileBlendAll[this.Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = ItemID.MudBlock;
			AddMapEntry(new Color(0, 20, 100));
		}

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(100) == 0)
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
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
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