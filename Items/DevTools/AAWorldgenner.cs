using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Worldgeneration;
using System.Linq;

namespace AAMod.Items.DevTools
{
	public class AAWorldgenner : ModItem
    {
        private int infernoSide = 0;

        private Vector2 infernoPos = new Vector2(0, 0);

        private int mireSide = 0;

        private Vector2 mirePos = new Vector2(0, 0);

        public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("AA Worldgenner");

            Tooltip.SetDefault(@"Spawns all AA worldgen at once
You have this item because either
A: This is an old world w/o AA worldgen in it
B: The thing this item is for failed to spawn.
No need to worry. Just use this item and you'll be good as new.
Careful though, this WILL lag your game for a bit.
Not recommended for weaker computers. Use the individual ones alternatively.");
        }		
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 10;
            item.value = BaseUtility.CalcValue(0, 0, 0, 0);

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.autoReuse = false;
            item.consumable = true;	
        }

		public override bool UseItem(Player player)
		{
            if (!AAWorld.InfernoGenerated)
            {
                infernoSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
                infernoPos.X = ((Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
                int j = (int)WorldGen.worldSurfaceLow - 30;
                while (Main.tile[(int)(infernoPos.X), j] != null && !Main.tile[(int)(infernoPos.X), j].active())
                {
                    j++;
                }
                for (int l = (int)(infernoPos.X) - 25; l < (int)(infernoPos.X) + 25; l++)
                {
                    for (int m = j - 6; m < j + 90; m++)
                    {
                        if (Main.tile[l, m] != null && Main.tile[l, m].active())
                        {
                            int type = Main.tile[l, m].type;
                            if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                            {
                                j++;
                                if (!Main.tile[l, m].active())
                                {
                                    j++;
                                }
                            }
                        }
                    }
                }
                infernoPos.Y = j;
                int q = (int)WorldGen.worldSurfaceLow - 30;
                Point origin = new Point((int)infernoPos.X, (int)infernoPos.Y);
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                InfernoBiome biome = new InfernoBiome();
                InfernoDelete delete = new InfernoDelete();
                delete.Place(origin, WorldGen.structures);
                biome.Place(origin, WorldGen.structures);
                AAWorld.InfernoGenerated = true;
            }

            if (!AAWorld.MireGenerated)
            {
                infernoSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
                infernoPos.X = ((Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
                mirePos.X = ((Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));

                int q = (int)WorldGen.worldSurfaceLow - 30;
                while (Main.tile[(int)(mirePos.X), q] != null && !Main.tile[(int)(mirePos.X), q].active())
                {
                    q++;
                }
                for (int l = (int)(mirePos.X) - 25; l < (int)(mirePos.X) + 25; l++)
                {
                    for (int m = q - 6; m < q + 90; m++)
                    {
                        if (Main.tile[l, m] != null && Main.tile[l, m].active())
                        {
                            int type = Main.tile[l, m].type;
                            if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                            {
                                q++;
                            }
                        }
                    }
                }
                mirePos.Y = q;
                Point origin = new Point((int)mirePos.X, (int)mirePos.Y);
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                MireDelete delete = new MireDelete();
                MireBiome biome = new MireBiome();
                delete.Place(origin, WorldGen.structures);
                biome.Place(origin, WorldGen.structures);
                AAWorld.MireGenerated = true;
            }


            if (!AAWorld.VoidGenerated)
            {
                int VoidHeight = 0;
                VoidHeight = 120;
                Point center = new Point((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, center.Y = VoidHeight);
                AAWorld.WHERESDAVOIDAT = center;
                Point oldposition = new Point(1, 1);
                List<Point> posIslands = new List<Point>();
                int IslandNumber = 2;
                if (AAWorld.GetWorldSize() != 1)
                {
                    IslandNumber = 4;
                }

                for (int i = 0; i < IslandNumber; i++)
                {
                    Point position = new Point(
                        center.X + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                        center.Y + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)));

                    while (posIslands.Any(x => Vector2.Distance(x.ToVector2(), position.ToVector2()) < 35))
                    {
                        for (int k = 0; k < posIslands.Count; ++k)
                        {
                            while ((int)Vector2.Distance(posIslands[k].ToVector2(), position.ToVector2()) < 35)
                            {
                                position = new Point(center.X + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                                  center.Y + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)));
                            }
                        }
                    }
                    MiniIsland(position, 60);
                    posIslands.Add(position);
                    oldposition = position;
                    for (int k = 0; k < posIslands.Count; ++k)
                    {
                        for (int FuckWorldGen = 0; FuckWorldGen < 6; ++FuckWorldGen)
                        {
                            Point randompoint = new Point(
                                posIslands[k].X + WorldGen.genRand.Next(-30, 31),
                                posIslands[k].Y + WorldGen.genRand.Next(7, 42));
                            WorldGen.TileRunner(randompoint.X, randompoint.Y, WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(6, 13), mod.TileType("Apocalyptite"), false, 0f, 0f, false, true);
                        }
                    }
                }
                for (int j = 0; j < posIslands.Count; ++j)
                {
                    Point position = posIslands[j];
                    position.X -= 4;
                    position.Y -= 11;
                    VoidHouses(position.X, position.Y, (ushort)mod.TileType("DoomstoneBrick"), 10, 7);
                }
                AAWorld.VoidGenerated = true;
            }


            if (!AAWorld.TerrariumGenerated)
            {
                Point origin = new Point((int)(Main.maxTilesX * 0.5f), (int)(Main.maxTilesY * 0.4f)); ;
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                TerrariumDelete delete = new TerrariumDelete();
                TerrariumSphere biome = new TerrariumSphere();
                delete.Place(origin, WorldGen.structures);
                biome.Place(origin, WorldGen.structures);

                AAWorld.TerrariumGenerated = true;
            }


            if (!AAWorld.OresGenerated)
            {
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                    if (Main.tile[tilesX, tilesY].type == 59)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EverleafRoot"));
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("AbyssiumOre"));
                    }
                    if (Main.tile[tilesX, tilesY].type == 1)
                    {
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("IncineriteOre"));
                    }
                }
                AAWorld.OresGenerated = true;
            }

            return true;
		}

        private void MiniIsland(Point position, int size)
        {
            for (int i = -size / 2; i < size / 2; ++i)
            {
                int repY = (size / 2) - (Math.Abs(i));
                int offset = repY / 5;
                repY += WorldGen.genRand.Next(4);
                for (int j = -offset; j < repY; ++j)
                {
                    WorldGen.PlaceTile(position.X + i, position.Y + j, mod.TileType<Tiles.Doomstone>());
                }
                int y = AAWorld.Raycast(position.X + i, position.Y - 5);
                WorldGen.PlaceObject(position.X + i, y, mod.TileType("OroborosTree"));
                WorldGen.GrowTree(position.X + i, y);
            }
        }

        public int ChestNumber = 0;

        public void VoidHouses(int X, int Y, int type = 30, int sizeX = 10, int sizeY = 7)
        {
            int wallID = (ushort)mod.WallType("DoomstoneBrickWall");
            //Clear area
            for (int i = X; i < X + sizeX - 1; ++i)
            {
                for (int j = Y - 1; j < Y + sizeY; ++j)
                {
                    WorldGen.KillTile(i, j);
                }
            }
            //Wall Placement
            for (int i = X + 1; i < X + sizeX - 2; ++i)
            {
                for (int j = Y + 1; j < Y + sizeY - 1; ++j)
                {
                    if (WorldGen.genRand.Next(5) >= 1)
                    {
                        WorldGen.KillWall(i, j);
                        WorldGen.PlaceWall(i, j, wallID);
                    }
                }
            }
            int chestType = 1;
            //Side placements
            for (int i = Y; i < Y + sizeY - 1; ++i)
            {
                WorldGen.PlaceTile(X, i, type);
                WorldGen.PlaceTile(X + (sizeX - 2), i, (ushort)mod.TileType("DoomstoneBrick"));
            }
            //Roof-floor placements
            for (int i = X; i < X + sizeX - 2; ++i)
            {
                WorldGen.PlaceTile(i, Y, type);
                WorldGen.PlaceTile(i, Y + (sizeY - 1), (ushort)mod.TileType("Doomstone"));
            }
            WorldGen.PlaceTile(X + sizeX - 2, Y + (sizeY) - 1, (ushort)mod.TileType("Doomstone"));
            if (chestType == 1)
            {
                ChestNumber = Main.rand.Next(4);
                if (ChestNumber == 0)
                {
                    WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChestC1"), true);
                }
                else if (ChestNumber == 1)
                {
                    WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChestC2"), true);
                }
                else if (ChestNumber == 2)
                {
                    WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChestC3"), true);
                }
                else
                {
                    WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChestC4"), true);
                }
            }
            //Side holes
            for (int i = Y + sizeY - 4; i > Y + sizeY; --i)
                WorldGen.KillTile(X, i);
        }

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}