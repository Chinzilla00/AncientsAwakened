using Microsoft.Xna.Framework;
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
            TileID.Sets.Conversion.Grass[Type] = true;
            SetModTree(new MushroomTree());
            Main.tileBlendAll[this.Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            dustType = mod.DustType("Mushdust");
			AddMapEntry(new Color(100, 100, 0));
		}
        
		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return mod.TileType("MushroomTree");
		}

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(50) == 0)
            {
                PlaceObject(i, j - 1, mod.TileType("Mushroom"));
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("Mushroom"), 0, 0, -1, -1);
            }
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(500) == 0)
            {
                int style = Main.rand.Next(5);
                if (PlaceObject(i, j - 1, mod.TileType<Tiles.MadnessShroom>(), false, style))
                    NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType<MadnessShroom>(), style, 0, -1, -1);
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
    }
}