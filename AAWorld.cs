using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using AAMod.Tiles;
using AAMod.Walls;
using Terraria.GameContent.Achievements;
using BaseMod;

namespace AAMod
{
    public class AAWorld : ModWorld
    {
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
        public static bool downedGripRed;
        public static bool downedGripBlue;
        public static bool downedGrips = downedGripRed && downedGripBlue;
        public static bool downedRetriever;
        public static bool downedOrthrus;
        public static bool downedRaider;
        public static bool downedStormAny;
        public static bool downedStormAll;
        public static bool downedDB;
        public static bool downedNC;
        public static bool downedEquinox = downedNC && downedDB;
        public static bool downedAncient;
        public static bool downedSAncient;
        public static bool downedAkuma;
        public static bool downedAkumaA;
        public static bool downedYamata;
        public static bool downedYamataA;
        public static bool zeroUS;
        public static bool downedZero;
        public static bool downedZeroA;
        public static bool downedShen;
        public static bool downedShenA;
        //Stones
        public static bool RealityDropped;

        public string nums = "1234567890";

        public override void Initialize()
        {
            //Bosses
            Chairlol = false;
            downedMonarch = false;
            downedGripRed = false;
            downedGripBlue = false;
            downedGrips = downedGripRed && downedGripBlue;
            downedRetriever = false;
            downedOrthrus = false;
            downedRaider = false;
            downedStormAny = downedRaider || downedOrthrus || downedRetriever;
            downedStormAll = downedRaider && downedOrthrus && downedRetriever;
            downedDB = false;
            downedNC = false;
            downedEquinox = downedDB && downedNC;
            if (Main.expertMode == false)
            {
                downedAncient = downedAkuma || downedYamata || downedZero;
                downedSAncient = downedShen;
            }
            if (Main.expertMode == true)
            {
                downedAncient = downedAkumaA || downedYamataA || downedZeroA;
                downedSAncient = downedShenA;
            }
            downedAkuma = false;
            downedAkumaA = false;
            downedYamata = false;
            downedYamataA = false;
            zeroUS = false;
            downedZero = false;
            downedZeroA = false;
            downedShen = false;
            downedShenA = false;
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
            if (downedGripRed) downed.Add("GripRed");
            if (downedGripBlue) downed.Add("GripBlue");
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
            if (downedAncient) downed.Add("A");
            if (downedSAncient) downed.Add("SA");
            if (downedAkuma) downed.Add("Akuma");
            if (downedYamata) downed.Add("Yamata");
            if (zeroUS) downed.Add("0U");
            if (downedZero) downed.Add("0");
            if (downedZeroA) downed.Add("0A");
            if (downedShen) downed.Add("Shen");
            if (downedAkumaA) downed.Add("AkumaA");
            if (downedYamataA) downed.Add("YamataA");

            return new TagCompound {
                {"downed", downed}
            };
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedGripRed;
            flags[1] = downedGripBlue;
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
            flags3[0] = downedAkumaA;
            flags3[1] = downedYamata;
            flags3[2] = downedYamataA;
            flags3[3] = zeroUS;
            flags3[4] = downedZero;
            flags3[5] = downedZeroA;
            flags3[6] = downedShen;
            flags3[7] = downedShenA;
            writer.Write(flags3);


            BitsByte flags4 = new BitsByte();
            flags4[0] = downedMonarch;
            flags4[1] = downedAncient;
            flags4[2] = downedSAncient;
            flags4[3] = Chairlol;
            writer.Write(flags4);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedGripRed = flags[0];
            downedGripBlue = flags[1];
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
            downedAkumaA = flags3[1];
            downedYamata = flags3[2];
            downedYamataA = flags3[3];
            zeroUS = flags3[4];
            downedZero = flags3[5];
            downedZeroA = flags3[6];
            downedShen = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            downedMonarch = flags4[0];
            downedAncient = flags4[1];
            downedSAncient = flags4[2];
            Chairlol = flags4[3];
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            //bosses
            Chairlol = downed.Contains("lol");
            downedMonarch = downed.Contains("Monarch");
            downedGripRed = downed.Contains("GripRed");
            downedGripBlue = downed.Contains("GripBlue");
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
            downedZeroA = downed.Contains("0A");
            downedShen = downed.Contains("Shen");
            downedAkumaA = downed.Contains("AkumaA");
            downedYamataA = downed.Contains("YamataA");
            //World Changes
            downedGrips = downedGripRed && downedGripBlue;
            ChaosOres = downedGrips;
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
                        if (Main.tile[tilesX, tilesY].type == 59)
                        {
                            WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EverleafRoot"));
                            WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("AbyssiumOre"));
                        }
                        if (Main.tile[tilesX, tilesY].type == 1)
                        {
                            WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("IncineriteOre"));
                        }
                    }
                }));
            }
            tasks.Insert(shiniesIndex + 4, new PassLegacy("Void Islands", delegate (GenerationProgress progress)
            {
                VoidIslands(progress);
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
            progress.Set(0.8f);
            for (int j = 0; j < posIslands.Count; ++j)
            {
                Point position = posIslands[j];
                position.X -= 4;
                position.Y -= 11;
                VoidHouses(position.X, position.Y, (ushort)mod.TileType("DoomstoneBrick"), 10, 7);
            }
            progress.Set(0.9f);
            AAWorldGen(progress);
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
                    Main.NewText("Stars twinkle in the atmosphere", Color.OrangeRed.R, Color.OrangeRed.G, Color.OrangeRed.B);
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
                    Main.NewText("Darkness grows in the depths of the world", Color.DarkBlue.R, Color.DarkBlue.G, Color.DarkBlue.B);
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
                    Main.NewText("The Ancients have Awakened", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B);
                }
            }

            if (NPC.downedMoonlord == true)
            {
                if (Luminite == false)
                {
                    Luminite = true;
                    Main.NewText("The Essence of the Moon Lord sparkles in the caves below", Color.DarkSeaGreen.R, Color.DarkSeaGreen.G, Color.DarkSeaGreen.B);
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
                    Main.NewText("The Caverns shine with the light of the radiant sun for a brief moment", Color.Yellow.R, Color.Yellow.G, Color.Yellow.B);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next(0, y);
                    if (Main.tile[tilesX, tilesY].type == 118)
                    {
                        for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                        {
                            WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("HallowedOreTile"));
                        }
                    }
                }
            }
            if (NPC.downedBoss3 == true)
            {
                if (Dynaskull == false)
                {
                    Dynaskull = true;
                    Main.NewText("Bones of the ancient past burst with energy", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    int tilesX = WorldGen.genRand.Next(0, x);
                    int tilesY = WorldGen.genRand.Next(0, y);
                    if (Main.tile[tilesX, tilesY].type == 54)
                    {
                        for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                        {
                            WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(7, 9), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DynaskullOre"));
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss == true)
            {
                if (Evil == false)
                {
                    Evil = true;
                    Main.NewText("Devils in the underworld begin to plot", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                }
            }
            if (downedRetriever == true)
            {
                if (FulguriteOre == false)
                {
                    FulguriteOre = true;
                    Main.NewText("The clap of a thunderbolt roars in the caverns", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B);
                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), (double)WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("FulguriteOre"));
                    }
                }
            }
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            mireTiles = tileCounts[mod.TileType("MireGrass")]+ tileCounts[mod.TileType("Depthstone")];
            infernoTiles = tileCounts[mod.TileType("InfernoGrass")]+ tileCounts[mod.TileType("Torchstone")];
            voidTiles = tileCounts[mod.TileType("Doomstone")] + tileCounts[mod.TileType("Apocalyptite")];
            mushTiles = tileCounts[mod.TileType("Mycelium")];
        }

        private void AAWorldGen(GenerationProgress progress)
        {
            infernoSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
            infernoPos.X = ((Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            mirePos.X = ((Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            int j = 40;
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
            int q = 40;
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
            Start();
            progress.Message = "Melting the Inferno";
            InfernoVolcano();
            progress.Message = "Flooding the Mire";
            MireAbyss();
        }

        private void Start()
        {
            int H = (int)(mirePos.X) + 25;
            int E = (int)(mirePos.Y);
            int radiusSwamp = 150;
            for (int h = H - radiusSwamp; h <= H + radiusSwamp; h++)
            {
                for (int e = E - radiusSwamp; e <= E + radiusSwamp; e++)
                {
                    if (Vector2.Distance(new Vector2(H, E), new Vector2(h, e)) <= radiusSwamp)
                    {
                        if (Main.tile[h, e] != null && Main.tile[h, e].active())
                        {
                            if (Main.tile[h, e] != null && (Main.tile[h, e].type == TileID.Stone || Main.tile[h, e].type == TileID.HardenedSand || Main.tile[h, e].type == TileID.CrimsonSandstone || Main.tile[h, e].type == TileID.CrimsonHardenedSand || Main.tile[h, e].type == TileID.Sandstone || Main.tile[h, e].type == TileID.CorruptSandstone || Main.tile[h, e].type == TileID.CorruptHardenedSand || Main.tile[h, e].type == TileID.Crimstone || Main.tile[h, e].type == TileID.Ebonstone))
                            {
                                Framing.GetTileSafely(h, e).type = (ushort)(mod.TileType("Depthstone"));
                                Framing.GetTileSafely(h, e).active(true);
                            }
                            if (Main.tile[h, e] != null && (Main.tile[h, e].type == TileID.Sand || Main.tile[h, e].type == TileID.Dirt || Main.tile[h, e].type == TileID.Grass || Main.tile[h, e].type == TileID.FleshGrass || Main.tile[h, e].type == TileID.CorruptGrass || Main.tile[h, e].type == TileID.Mud || Main.tile[h, e].type == TileID.JungleGrass || Main.tile[h, e].type == TileID.Crimsand || Main.tile[h, e].type == TileID.Ebonsand))
                            {
                                Framing.GetTileSafely(h, e).type = TileID.Mud;
                                Framing.GetTileSafely(h, e).active(true);
                                if (!Main.tile[h, e - 1].active() || !Main.tile[h, e + 1].active() || !Main.tile[h - 1, e].active() || !Main.tile[h + 1, e].active())
                                {
                                    Framing.GetTileSafely(h, e).type = (ushort)(mod.TileType("MireGrass"));
                                    Framing.GetTileSafely(h, e).active(true);
                                }
                            }
                            if (Main.tile[h, e] != null && (Main.tile[h, e].type == TileID.Tin || Main.tile[h, e].type == TileID.Copper || Main.tile[h, e].type == TileID.Iron || Main.tile[h, e].type == TileID.Lead || Main.tile[h, e].type == TileID.Silver || Main.tile[h, e].type == TileID.Tungsten || Main.tile[h, e].type == TileID.Gold || Main.tile[h, e].type == TileID.Platinum || Main.tile[h, e].type == TileID.Amethyst || Main.tile[h, e].type == TileID.Topaz || Main.tile[h, e].type == TileID.Sapphire || Main.tile[h, e].type == TileID.Emerald || Main.tile[h, e].type == TileID.Ruby || Main.tile[h, e].type == TileID.Diamond))
                            {
                                Framing.GetTileSafely(h, e).type = (ushort)(mod.TileType("AbyssiumOre"));
                                Framing.GetTileSafely(h, e).active(true);
                            }
                        }
                        if (Main.tile[h, e] != null && (Main.tile[h, e].wall == WallID.Stone || Main.tile[h, e].wall == WallID.EbonstoneUnsafe || Main.tile[h, e].wall == WallID.CorruptionUnsafe1 || Main.tile[h, e].wall == WallID.CorruptionUnsafe2 || Main.tile[h, e].wall == WallID.CorruptionUnsafe3 || Main.tile[h, e].wall == WallID.CorruptionUnsafe4 || Main.tile[h, e].wall == WallID.CorruptSandstone || Main.tile[h, e].wall == WallID.CrimstoneUnsafe || Main.tile[h, e].wall == WallID.CrimsonUnsafe1 || Main.tile[h, e].wall == WallID.CrimsonUnsafe2 || Main.tile[h, e].wall == WallID.CrimsonUnsafe3 || Main.tile[h, e].wall == WallID.CrimsonUnsafe4 || Main.tile[h, e].wall == WallID.CrimsonSandstone || Main.tile[h, e].wall == WallID.Sandstone))
                        {
                            Framing.GetTileSafely(h, e).wall = (ushort)(mod.WallType("DepthstoneWall"));
                        }
                        if (Main.tile[h, e] != null && (Main.tile[h, e].wall == WallID.Dirt || Main.tile[h, e].wall == WallID.DirtUnsafe || Main.tile[h, e].wall == WallID.DirtUnsafe1 || Main.tile[h, e].wall == WallID.DirtUnsafe2 || Main.tile[h, e].wall == WallID.DirtUnsafe3 || Main.tile[h, e].wall == WallID.DirtUnsafe4 || Main.tile[h, e].wall == WallID.Grass || Main.tile[h, e].wall == WallID.GrassUnsafe || Main.tile[h, e].wall == WallID.CorruptGrassUnsafe || Main.tile[h, e].wall == WallID.CrimsonGrassUnsafe || Main.tile[h, e].wall == WallID.HardenedSand || Main.tile[h, e].wall == WallID.CrimsonHardenedSand || Main.tile[h, e].wall == WallID.CorruptHardenedSand))
                        {
                            Framing.GetTileSafely(h, e).wall = (ushort)(mod.WallType("MireGrassWall"));
                        }
                        if (Main.tile[h, e] != null && (Main.tile[h, e].wall == WallID.Jungle || Main.tile[h, e].wall == WallID.JungleUnsafe || Main.tile[h, e].wall == WallID.JungleUnsafe1 || Main.tile[h, e].wall == WallID.JungleUnsafe2 || Main.tile[h, e].wall == WallID.JungleUnsafe3 || Main.tile[h, e].wall == WallID.JungleUnsafe4))
                        {
                            Framing.GetTileSafely(h, e).wall = (ushort)(mod.WallType("MireJungleWall"));
                        }
                    }
                }
            }
            int X = (int)(infernoPos.X) + 25;
            int Y = (int)(infernoPos.Y);
            int radiusVolcano = 150;
            for (int x = X - radiusVolcano; x <= X + radiusVolcano; x++)
            {
                for (int y = Y - radiusVolcano; y <= Y + radiusVolcano; y++)
                {
                    if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radiusVolcano)
                    {
                        if (Main.tile[x, y] != null && Main.tile[x, y].active())
                        {
                            if (Main.tile[x, y] != null && (Main.tile[x, y].type == TileID.Stone || Main.tile[x, y].type == TileID.HardenedSand || Main.tile[x, y].type == TileID.CrimsonSandstone || Main.tile[x, y].type == TileID.CrimsonHardenedSand || Main.tile[x, y].type == TileID.Sandstone || Main.tile[x, y].type == TileID.CorruptSandstone || Main.tile[x, y].type == TileID.CorruptHardenedSand || Main.tile[x, y].type == TileID.Crimstone || Main.tile[x, y].type == TileID.Ebonstone))
                            {
                                Framing.GetTileSafely(x, y).type = (ushort)(mod.TileType("Torchstone"));
                                Framing.GetTileSafely(x, y).active(true);
                            }
                            if (Main.tile[x, y] != null && (Main.tile[x, y].type == TileID.Sand || Main.tile[x, y].type == TileID.Dirt || Main.tile[x, y].type == TileID.Grass || Main.tile[x, y].type == TileID.FleshGrass || Main.tile[x, y].type == TileID.CorruptGrass || Main.tile[x, y].type == TileID.Mud || Main.tile[x, y].type == TileID.JungleGrass || Main.tile[x, y].type == TileID.Crimsand || Main.tile[x, y].type == TileID.Ebonsand))
                            {
                                Framing.GetTileSafely(x, y).type = TileID.Dirt;
                                Framing.GetTileSafely(x, y).active(true);
                                if (!Main.tile[x, y - 1].active() || !Main.tile[x, y + 1].active() || !Main.tile[x - 1, y].active() || !Main.tile[x + 1, y].active())
                                {
                                    Framing.GetTileSafely(x, y).type = (ushort)(mod.TileType("InfernoGrass"));
                                    Framing.GetTileSafely(x, y).active(true);
                                }
                            }
                            if (Main.tile[x, y] != null && (Main.tile[x, y].type == TileID.Tin || Main.tile[x, y].type == TileID.Copper || Main.tile[x, y].type == TileID.Iron || Main.tile[x, y].type == TileID.Lead || Main.tile[x, y].type == TileID.Silver || Main.tile[x, y].type == TileID.Tungsten || Main.tile[x, y].type == TileID.Gold || Main.tile[x, y].type == TileID.Platinum || Main.tile[x, y].type == TileID.Amethyst || Main.tile[x, y].type == TileID.Topaz || Main.tile[x, y].type == TileID.Sapphire || Main.tile[x, y].type == TileID.Emerald || Main.tile[x, y].type == TileID.Ruby || Main.tile[x, y].type == TileID.Diamond))
                            {
                                Framing.GetTileSafely(x, y).type = (ushort)(mod.TileType("IncineriteOre"));
                                Framing.GetTileSafely(x, y).active(true);
                            }
                        }
                        if (Main.tile[x, y] != null && (Main.tile[x, y].wall == WallID.Stone || Main.tile[x, y].wall == WallID.EbonstoneUnsafe || Main.tile[x, y].wall == WallID.CorruptionUnsafe1 || Main.tile[x, y].wall == WallID.CorruptionUnsafe2 || Main.tile[x, y].wall == WallID.CorruptionUnsafe3 || Main.tile[x, y].wall == WallID.CorruptionUnsafe4 || Main.tile[x, y].wall == WallID.CorruptSandstone || Main.tile[x, y].wall == WallID.CrimstoneUnsafe || Main.tile[x, y].wall == WallID.CrimsonUnsafe1 || Main.tile[x, y].wall == WallID.CrimsonUnsafe2 || Main.tile[x, y].wall == WallID.CrimsonUnsafe3 || Main.tile[x, y].wall == WallID.CrimsonUnsafe4 || Main.tile[x, y].wall == WallID.CrimsonSandstone || Main.tile[x, y].wall == WallID.Sandstone))
                        {
                            Framing.GetTileSafely(x, y).wall = (ushort)(mod.WallType("TorchstoneWall"));
                        }
                        if (Main.tile[x, y] != null && (Main.tile[x, y].wall == WallID.Dirt || Main.tile[x, y].wall == WallID.DirtUnsafe || Main.tile[x, y].wall == WallID.DirtUnsafe1 || Main.tile[x, y].wall == WallID.DirtUnsafe2 || Main.tile[x, y].wall == WallID.DirtUnsafe3 || Main.tile[x, y].wall == WallID.DirtUnsafe4 || Main.tile[x, y].wall == WallID.Grass || Main.tile[x, y].wall == WallID.GrassUnsafe || Main.tile[x, y].wall == WallID.CorruptGrassUnsafe || Main.tile[x, y].wall == WallID.CrimsonGrassUnsafe || Main.tile[x, y].wall == WallID.HardenedSand || Main.tile[x, y].wall == WallID.CrimsonHardenedSand || Main.tile[x, y].wall == WallID.CorruptHardenedSand))
                        {
                            Framing.GetTileSafely(x, y).wall = (ushort)(mod.WallType("InfernoGrassWall"));
                        }
                    }
                }
            }
        }

        public void InfernoVolcano()
        {
            Mod mod = AAMod.instance;
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("Torchstone");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Torchstone");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("TorchstoneWall");
            colorToWall[Color.Black] = -1; //don't touch when genning		
            
            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgen/Volcano"), colorToTile, mod.GetTexture("Worldgen/VolcanoWalls"), colorToWall, mod.GetTexture("Worldgen/VolcanoLava"));
            Point origin = new Point ((int)infernoPos.X, (int)infernoPos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            gen.Generate(origin.X, origin.Y + 2, true, true);
        }

        public void MireAbyss()
        {
            Mod mod = AAMod.instance;
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Depthstone");
            colorToTile[new Color(255, 128, 0)] = TileID.Mud;
            colorToTile[new Color(0, 255, 0)] = mod.TileType("MireGrass");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 0, 255)] = mod.WallType("DepthstoneWall");
            colorToWall[Color.Black] = -1; //don't touch when genning			

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgen/Lake"), colorToTile, mod.GetTexture("Worldgen/LakeWalls"), colorToWall, mod.GetTexture("Worldgen/LakeWater"));
            Point origin = new Point ((int)mirePos.X, (int)mirePos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y + 20, true);
            gen.Generate(origin.X, origin.Y + 2, true, true);
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