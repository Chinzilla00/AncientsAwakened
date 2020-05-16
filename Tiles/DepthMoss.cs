using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles.Plants;

namespace AAMod.Tiles
{
    public class DepthMoss : ModTile
    {
        public static int _type;

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new BogwoodTree());
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            dustType = mod.DustType("AbyssiumDust");
            soundType = SoundID.Tink;
            minPick = 65;
            AddMapEntry(new Color(0, 50, 140));
            drop = ModContent.ItemType<Items.Blocks.Depthstone>();
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
                if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(20) == 0)
                {
                    int style = Main.rand.Next(23);
                    if (PlaceObject(i, j - 1, MireFoliage._type, false, style))
                        NetMessage.SendObjectPlacment(-1, i, j - 1, MireFoliage._type, style, 0, -1, -1);
                }
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
            return mod.TileType("BogwoodSapling");
        }
    }
}