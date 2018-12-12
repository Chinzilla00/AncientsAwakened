using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using AAMod.Tiles;
using BaseMod;
using AAMod.Worldgeneration;

namespace AAMod
{
    public class AAWorld : ModWorld
    {
        public static int SmashDragonEgg = 2;
        public static int SmashHydraPod = 2;
        //tile ints
        public static int mireTiles = 0;
        public static int infernoTiles = 0;
        public static int voidTiles = 0;
        public static int mushTiles = 0;
        //Worldgen
        public static bool Luminite;
        public static bool DarkMatter;
        public static bool FulguriteOre;
        public static bool HallowedOre;
        public static bool Dynaskull;
        public static bool ChaosOres;
        public static bool RadiumOre;
        private int infernoSide = 0;
        private Vector2 infernoPos = new Vector2(0, 0);
        private Vector2 mirePos = new Vector2(0, 0);
        //Messages
        public static bool Evil;
        //Boss Bools
        public static bool Chairlol;
        public static bool Ancients;
        public static bool downedMonarch;
        public static bool downedBrood;
        public static bool downedHydra;
        public static bool downedGrips;
        public static bool downedRetriever;
        public static bool downedOrthrus;
        public static bool downedRaider;
        public static bool downedStormAny;
        public static bool downedStormAll;
        public static bool downedDB;
        public static bool downedNC;
        public static bool downedEquinox;
        public static bool downedAncient;
        public static bool downedSAncient;
        public static bool downedAkuma;
        public static bool downedYamata;
        public static bool zeroUS;
        public static bool downedZero;
        public static bool downedShen;
        public static bool downedIZ;
        public static bool downedAllAncients;
        public static int downedIZnumber;
        //Stones
        public static bool RealityDropped;
        //Points
        public static Point WHERESDAVOIDAT;

        public string nums = "1234567890";

        public override void Initialize()
        {
            //Bosses
            Chairlol = false;
            downedMonarch = false;
            downedGrips = false;
            downedRetriever = false;
            downedOrthrus = false;
            downedRaider = false;
            downedStormAny = false;
            downedStormAll = false;
            downedDB = false;
            downedNC = false;
            downedEquinox = false;
            downedSAncient = false;
            downedAkuma = false;
            downedYamata = false;
            zeroUS = false;
            downedZero = false;
            downedShen = false;
            downedIZ = false;
            downedAllAncients = false;
            downedIZnumber = 0;
            //World Changes
            ChaosOres = downedGrips;
            Dynaskull = NPC.downedBoss3;
            FulguriteOre = downedStormAny;
            HallowedOre = NPC.downedMechBossAny;
            Evil = NPC.downedPlantBoss;
            Luminite = NPC.downedMoonlord;
            DarkMatter = downedNC;
            RadiumOre = downedDB;
            //Stones
            RealityDropped = false;
        }

        public static int Raycast(int x, int y)
        {
            while (!TileValid(x, y))
                y++;
            return y;
        }

        public static bool TileValid(int i, int j)
        {
            bool valid = false;
            try
            {
                valid = Main.tile[i, j].active() && Main.tileSolid[Main.tile[i, j].type];
            }
            catch (Exception e)
            {
                ErrorLogger.Log(e.ToString() + "\n" + i + " " + j);
            }
            return valid;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (Chairlol) downed.Add("lol");
            if (downedMonarch) downed.Add("Monarch");
            if (downedGrips) downed.Add("Grips");
            if (downedHydra) downed.Add("Hydra");
            if (downedBrood) downed.Add("Brood");
            if (NPC.downedBoss3) downed.Add("Dynaskull");
            if (downedRetriever) downed.Add("Storm1");
            if (downedOrthrus) downed.Add("Storm2");
            if (downedRaider) downed.Add("Storm3");
            if (NPC.downedMechBossAny) downed.Add("MechBoss");
            if (NPC.downedPlantBoss) downed.Add("Evil");
            if (NPC.downedMoonlord) downed.Add("MoonLord");
            if (downedNC) downed.Add("NC");
            if (downedDB) downed.Add("DB");
            if (downedEquinox) downed.Add("Equinox");
            if (Ancients) downed.Add("AA");
            if (downedAncient) downed.Add("A");
            if (downedSAncient) downed.Add("SA");
            if (downedAkuma) downed.Add("Akuma");
            if (downedYamata) downed.Add("Yamata");
            if (zeroUS) downed.Add("0U");
            if (downedZero) downed.Add("0");
            if (downedShen) downed.Add("Shen");
            if (downedIZ) downed.Add("IZ");
            if (downedAllAncients) downed.Add("DAA");

            return new TagCompound {
                {"downed", downed}
            };
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedMonarch;
            flags[1] = downedAncient;
            flags[2] = downedGrips;
            flags[3] = downedBrood;
            flags[4] = downedHydra;
            flags[5] = NPC.downedBoss3;
            flags[6] = downedRetriever;
            flags[7] = downedOrthrus;
            writer.Write(flags);

            BitsByte flags2 = new BitsByte();
            flags2[0] = downedRaider;
            flags2[1] = NPC.downedMechBossAny;
            flags2[2] = NPC.downedPlantBoss;
            flags2[3] = NPC.downedMoonlord;
            flags2[4] = downedDB;
            flags2[5] = downedNC;
            flags2[6] = downedEquinox;
            flags2[7] = downedAkuma;
            writer.Write(flags2);

            BitsByte flags3 = new BitsByte();
            flags3[0] = downedAllAncients;
            flags3[1] = downedYamata;
            flags3[2] = Chairlol;
            flags3[3] = zeroUS;
            flags3[4] = downedZero;
            flags3[5] = downedSAncient;
            flags3[6] = downedShen;
            flags3[7] = downedIZ;
            writer.Write(flags3);


            BitsByte flags4 = new BitsByte();
            flags4[0] = Ancients;
            writer.Write(flags4);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedMonarch = flags[0];
            downedAncient = flags[1];
            downedGrips = flags[2];
            downedBrood = flags[3];
            downedHydra = flags[4];
            NPC.downedBoss3 = flags[5];
            downedRetriever = flags[6];
            downedOrthrus = flags[7];

            BitsByte flags2 = reader.ReadByte();
            downedRaider = flags2[0];
            NPC.downedMechBossAny = flags2[1];
            NPC.downedPlantBoss = flags2[2];
            NPC.downedMoonlord = flags2[3];
            downedDB = flags2[4];
            downedNC = flags2[5];
            downedEquinox = flags2[6];
            downedAkuma = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            downedAllAncients = flags3[0];
            downedYamata = flags3[1];
            Chairlol = flags3[2];
            zeroUS = flags3[3];
            downedZero = flags3[4];
            downedSAncient = flags3[4];
            downedShen = flags3[6];
            downedIZ = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            Ancients = flags4[0];
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            //bosses
            Chairlol = downed.Contains("lol");
            downedMonarch = downed.Contains("Monarch");
            downedGrips = downed.Contains("Grips");
            NPC.downedBoss3 = downed.Contains("Dynaskull");
            downedRetriever = downed.Contains("Storm1");
            downedOrthrus = downed.Contains("Storm2");
            downedRaider = downed.Contains("Storm3");
            NPC.downedMechBossAny = downed.Contains("MechBoss");
            NPC.downedPlantBoss = downed.Contains("Evil");
            NPC.downedMoonlord = downed.Contains("MoonLord");
            downedDB = downed.Contains("DB");
            downedNC = downed.Contains("NC");
            downedEquinox = downed.Contains("Equinox");
            downedAncient = downed.Contains("A");
            downedSAncient = downed.Contains("SA");
            downedAkuma = downed.Contains("Akuma");
            downedYamata = downed.Contains("Yamata");
            zeroUS = downed.Contains("0U");
            downedZero = downed.Contains("0");
            downedShen = downed.Contains("Shen");
            downedIZ = downed.Contains("IZ");
            downedAllAncients = downed.Contains("DAA");
            Ancients = downed.Contains("AA");
            //World Changes
            Dynaskull = NPC.downedBoss3;
            FulguriteOre = downedRetriever;
            HallowedOre = NPC.downedMechBossAny;
            Evil = NPC.downedPlantBoss;
            Luminite = NPC.downedMoonlord;
            DarkMatter = downedNC;
            RadiumOre = downedDB;

        }

        private string NumberRand(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = nums[Main.rand.Next(nums.Length)];
            }
            return new string(chars);
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (shiniesIndex == -1)
            {
                tasks.Insert(shiniesIndex + 3, new PassLegacy("Generating AA Ores", delegate (GenerationProgress progress)
                {
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        int x = Main.maxTilesX;
                        int y = Main.maxTilesY;
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].type == TileID.Mud)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EverleafRoot"));
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("AbyssiumOre"));
                        }
                        if (Main.tile[tilesX, tilesY].type == TileID.Stone)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("IncineriteOre"));
                        }
                    }
                }));
            }
            tasks.Insert(shiniesIndex2 + 1, new PassLegacy("Void Islands", delegate (GenerationProgress progress)
            {
                VoidIslands(progress);
            }));
			int chaosBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            tasks.Insert(chaosBiomeIndex, new PassLegacy("Mire and Inferno", delegate (GenerationProgress progress)
            {
				MireAndInferno(progress);
            }));			
        }

        public void Mush(GenerationProgress progress)
        {
            for (int k = 0; k < (int)((double)(WorldGen.worldSurface * Main.maxTilesY) * 1E-05); k++)
            {
                int X = WorldGen.genRand.Next((Main.maxTilesX / 2) - 150, (Main.maxTilesX / 2) + 150);
                int Y = WorldGen.genRand.Next((int)WorldGen.worldSurface);
                WorldGen.OreRunner(X, Y, WorldGen.genRand.Next(1, 2), WorldGen.genRand.Next(1, 2), (ushort)mod.TileType("Mycelium"));
            }
        }

        public void VoidIslands(GenerationProgress progress) //method line
        {
            progress.Message = ("0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0");

            progress.Set(0f);
            int VoidHeight = 0;
            progress.Set(0.1f);
            VoidHeight = 120;
            progress.Set(0.4f);
            Point center = new Point((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, center.Y = VoidHeight);
            progress.Set(0.5f);
            Point oldposition = new Point(1, 1);
            progress.Set(0.6f);
            List<Point> posIslands = new List<Point>();
            progress.Set(0.7f);
            for (int i = 0; i < 4; i++)
            {
                Point position = new Point(
                    center.X + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                    center.Y + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)));
                WHERESDAVOIDAT = position;

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
            progress.Set(0.85f);
            for (int j = 0; j < posIslands.Count; ++j)
            {
                Point position = posIslands[j];
                position.X -= 4;
                position.Y -= 11;
                VoidHouses(position.X, position.Y, (ushort)mod.TileType("DoomstoneBrick"), 10, 7);
            }
            progress.Set(1f);
        }
        public int BlockLining(double x, double y, int repeats, int tileType, bool random, int max, int min = 3)
        {
            for (double i = x; i < x + repeats; i++)
            {
                if (random)
                {
                    for (double k = y; k < y + Main.rand.Next(min, max); k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
                else
                {
                    for (double k = y; k < y + max; k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
            }
            return repeats;
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
                    WorldGen.PlaceTile(position.X + i, position.Y + j, mod.TileType<Doomstone>());
                }
                int y = Raycast(position.X + i, position.Y - 5);
                WorldGen.PlaceObject(position.X + i, y, mod.TileType("OroborosTree"));
                WorldGen.GrowTree(position.X + i, y);
            }
        }

        /*public void ChaosChests(GenerationProgress progress)
        {
            Main.mouseRightRelease = false;
            int num60 = 0;
            int num61;
            for (num61 = (int)(Main.tile[myX, myY].frameX / 18); num61 > 1; num61 -= 2)
            {
            }
            num61 = myX - num61;
            int num62 = myY - (int)(Main.tile[myX, myY].frameY / 18);
            if (Main.tile[myX, myY].type == 29)
            {
                num60 = 1;
            }
            else if (Main.tile[myX, myY].type == 97)
            {
                num60 = 2;
            }
            else if (Main.tile[myX, myY].type == 463)
            {
                num60 = 3;
            }
            if (this.sign > -1)
            {
                Main.PlaySound(11, -1, -1, 1, 1f, 0f);
                this.sign = -1;
                Main.editSign = false;
                Main.npcChatText = string.Empty;
            }
            if (Main.editChest)
            {
                Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                Main.editChest = false;
                Main.npcChatText = string.Empty;
            }
            if (this.editedChestName)
            {
                NetMessage.SendData(33, -1, -1, Main.chest[this.chest].name, this.chest, 1f, 0f, 0f, 0, 0, 0);
                this.editedChestName = false;
            }
            if (Main.netMode == 1 && num60 == 0 && (Main.tile[num61, num62].frameX < 72 || Main.tile[num61, num62].frameX > 106) && (Main.tile[num61, num62].frameX < 144 || Main.tile[num61, num62].frameX > 178) && (Main.tile[num61, num62].frameX < 828 || Main.tile[num61, num62].frameX > 1006) && (Main.tile[num61, num62].frameX < 1296 || Main.tile[num61, num62].frameX > 1330) && (Main.tile[num61, num62].frameX < 1368 || Main.tile[num61, num62].frameX > 1402) && (Main.tile[num61, num62].frameX < 1440 || Main.tile[num61, num62].frameX > 1474))
            {
                if (num61 == this.chestX && num62 == this.chestY && this.chest != -1)
                {
                    this.chest = -1;
                    Recipe.FindRecipes();
                    Main.PlaySound(11, -1, -1, 1, 1f, 0f);
                }
                else
                {
                    NetMessage.SendData(31, -1, -1, "", num61, (float)num62, 0f, 0f, 0, 0, 0);
                    Main.stackSplit = 600;
                }
            }
            else
            {
                int num63 = -1;
                if (num60 == 1)
                {
                    num63 = -2;
                }
                else if (num60 == 2)
                {
                    num63 = -3;
                }
                else if (num60 == 3)
                {
                    num63 = -4;
                }
                else
                {
                    bool flag11 = false;
                    if (Chest.isLocked(num61, num62))
                    {
                        int num64 = 327;
                        if (Main.tile[num61, num62].frameX >= 144 && Main.tile[num61, num62].frameX <= 178)
                        {
                            num64 = 329;
                        }
                        if (Main.tile[num61, num62].frameX >= 828 && Main.tile[num61, num62].frameX <= 1006)
                        {
                            int num65 = (int)(Main.tile[num61, num62].frameX / 18);
                            int num66 = 0;
                            while (num65 >= 2)
                            {
                                num65 -= 2;
                                num66++;
                            }
                            num66 -= 23;
                            num64 = 1533 + num66;
                        }
                        flag11 = true;
                        for (int num67 = 0; num67 < 58; num67++)
                        {
                            if (this.inventory[num67].type == num64 && this.inventory[num67].stack > 0 && Chest.Unlock(num61, num62))
                            {
                                if (num64 != 329)
                                {
                                    this.inventory[num67].stack--;
                                    if (this.inventory[num67].stack <= 0)
                                    {
                                        this.inventory[num67] = new Item();
                                    }
                                }
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(52, -1, -1, "", this.whoAmI, 1f, (float)num61, (float)num62, 0, 0, 0);
                                }
                            }
                        }
                    }
                    if (!flag11)
                    {
                        num63 = Chest.FindChest(num61, num62);
                    }
                }
                if (num63 != -1)
                {
                    Main.stackSplit = 600;
                    if (num63 == this.chest)
                    {
                        this.chest = -1;
                        Main.PlaySound(11, -1, -1, 1, 1f, 0f);
                    }
                    else if (num63 != this.chest && this.chest == -1)
                    {
                        this.chest = num63;
                        Main.playerInventory = true;
                        if (PlayerInput.GrappleAndInteractAreShared)
                        {
                            PlayerInput.Triggers.JustPressed.Grapple = false;
                        }
                        Main.recBigList = false;
                        Main.PlaySound(10, -1, -1, 1, 1f, 0f);
                        this.chestX = num61;
                        this.chestY = num62;
                        if (Main.tile[num61, num62].frameX >= 36 && Main.tile[num61, num62].frameX < 72)
                        {
                            AchievementsHelper.HandleSpecialEvent(this, 16);
                        }
                    }
                    else
                    {
                        this.chest = num63;
                        Main.playerInventory = true;
                        if (PlayerInput.GrappleAndInteractAreShared)
                        {
                            PlayerInput.Triggers.JustPressed.Grapple = false;
                        }
                        Main.recBigList = false;
                        Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                        this.chestX = num61;
                        this.chestY = num62;
                    }
                    Recipe.FindRecipes();
                }
            }
        }*/

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
                WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChest"), true);
            }
            //Side holes
            for (int i = Y + sizeY - 4; i > Y + sizeY; --i)
                WorldGen.KillTile(X, i);
        }

        public override void PostUpdate()
        {
            if (downedDB == true)
            {
                if (RadiumOre == false)
                {
                    RadiumOre = true;
                    Main.NewText("Stars twinkle in the atmosphere...", Color.OrangeRed.R, Color.OrangeRed.G, Color.OrangeRed.B);
                    for (int i = 0; i < Main.maxTilesX / 28; ++i) //Repeats 700 times for small world, 1050 times for medium world, and 1400 times for large world.
                    {
                        int X = WorldGen.genRand.Next(50, (Main.maxTilesX / 10) * 9); //X position, centre.
                        int Y = WorldGen.genRand.Next(80); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 5); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                            for (int y = Y - radius; y <= Y + radius; y++)
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                    WorldGen.PlaceTile(x, y, mod.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                    }
                }
            }

            if (downedNC == true)
            {
                if (DarkMatter == false)
                {
                    DarkMatter = true;
                    Main.NewText("Darkness grows in the depths of the world...", Color.DarkBlue.R, Color.DarkBlue.G, Color.DarkBlue.B);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(11, 12), (ushort)mod.TileType("DarkmatterOre"));
                    }
                }
            }

            if (downedEquinox == true)
            {
                if (Ancients == false)
                {
                    Ancients = true;
                    Main.NewText("The Ancients have Awakened!", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B);
                }
            }

            if (NPC.downedMoonlord == true)
            {
                if (Luminite == false)
                {
                    Luminite = true;
                    Main.NewText("The Essence of the Moon Lord sparkles in the caves below...", Color.DarkSeaGreen.R, Color.DarkSeaGreen.G, Color.DarkSeaGreen.B);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(6, 10), (ushort)mod.TileType("LuminiteOre"));
                    }
                }
                
            }
            if (NPC.downedMechBossAny == true)
            {
                if (HallowedOre == false)
                {
                    HallowedOre = true;
                    Main.NewText("The caverns shine with light for a brief moment...", Color.Yellow.R, Color.Yellow.G, Color.Yellow.B);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next(0, y);
                    if (Main.tile[tilesX, tilesY].type == TileID.Pearlstone)
                    {
                        for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("HallowedOre"));
                        }
                    }
                }
            }
            if (NPC.downedBoss3 == true)
            {
                if (Dynaskull == false)
                {
                    Dynaskull = true;
                    Main.NewText("Bones of the ancient past burst with energy...", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next(0, y);
                    if (Main.tile[tilesX, tilesY].type == 54)
                    {
                        for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DynaskullOre"));
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss == true)
            {
                if (Evil == false)
                {
                    Evil = true;
                    Main.NewText("Devils in the underworld begin to plot...", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                }
            }
            if (downedRetriever || downedOrthrus || downedRaider)
            {
                downedStormAny = true;
                if (FulguriteOre == false)
                {
                    FulguriteOre = true;
                    Main.NewText("The clap of a thunderbolt roars in the caverns...", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B);
                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("FulguriteOre"));
                    }
                }
            }

            if (downedRetriever & downedOrthrus & downedRaider)
            {
                downedStormAll = true;
            }

            if (downedAkuma || downedYamata || downedZero)
            {
                downedAncient = true;
            }

            if (downedShen || downedIZ)
            {
                downedSAncient = true;
            }

            if (downedAkuma && downedYamata && downedZero)
            {
                if (downedAllAncients == false)
                {
                    downedAllAncients = true;
                    Main.NewText("Chaos begins to stir in the atmosphere around you", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                    Main.NewText("You feel as if you are being watched by something...malicious...", new Color(158, 3, 32));
                }
            }
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            mireTiles = tileCounts[mod.TileType<MireGrass>()]+ tileCounts[mod.TileType<Depthstone>()] + tileCounts[mod.TileType<Depthsand>()] + tileCounts[mod.TileType<Depthsandstone>()] + tileCounts[mod.TileType<DepthsandHardened>()] + tileCounts[mod.TileType<Depthice>()];
            infernoTiles = tileCounts[mod.TileType<InfernoGrass>()]+ tileCounts[mod.TileType<Torchstone>()] + tileCounts[mod.TileType<Torchsand>()] + tileCounts[mod.TileType<Torchsandstone>()] + tileCounts[mod.TileType<TorchsandHardened>()] + tileCounts[mod.TileType<Torchice>()];
            voidTiles = tileCounts[mod.TileType<Doomstone>()] + tileCounts[mod.TileType<Apocalyptite>()];
            mushTiles = tileCounts[mod.TileType<Mycelium>()];
        }

        private void MireAndInferno(GenerationProgress progress)
        {
            infernoSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
            infernoPos.X = ((Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            mirePos.X = ((Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            int j = (int)WorldGen.worldSurfaceLow - 10;
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
            int q = (int)WorldGen.worldSurfaceLow - 10;
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

            progress.Message = "Spreading Chaos";
            progress.Message = "Scorching the Inferno";
            InfernoVolcano();
            progress.Message = "Flooding the Mire";
            MireAbyss();
        }

        public void InfernoVolcano()
        {
            Point origin = new Point ((int)infernoPos.X, (int)infernoPos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);	
            InfernoBiome biome = new InfernoBiome();
            biome.Place(origin, WorldGen.structures);
        }

        public void MireAbyss()
        {
            Point origin = new Point ((int)mirePos.X, (int)mirePos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            MireBiome biome = new MireBiome();
            biome.Place(origin, WorldGen.structures);        
        }


        public override void ResetNearbyTileEffects()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>(mod);
            modPlayer.VoidUnit = false;
            modPlayer.SunAltar = false;
            modPlayer.MoonAltar = false;
            modPlayer.AkumaAltar = false;
            modPlayer.YamataAltar = false;
        }
    }
}