using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;

namespace AAMod.Tiles
{
    public class TorchAsh : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new RazewoodTree());
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.Snow[Type] = true;
            dustType = mod.DustType<Dusts.AshRain>();
            AddMapEntry(new Color(30, 30, 30));
            drop = mod.ItemType<Items.Blocks.TorchAsh>();
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(500) == 0)
            {
                PlaceObject(i, j - 1, mod.TileType("Hotshroom"));
                NetMessage.SendObjectPlacment(-1, i, j - 1, mod.TileType("Hotshroom"), 0, 0, -1, -1);
            }
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
            }
            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("RazewoodTree");
        }
    }
}