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
using AAMod.Tiles.Ore;
using AAMod.Tiles;
using AAMod.Tiles.Crafters;
using AAMod.Worldgeneration;
using AAMod.Worldgen;
using Terraria.Utilities;
using Terraria.Localization;
using AAMod.Walls;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod
{
    public class AAWorld : ModWorld
    {
        #region Variables
        public static int SmashDragonEgg = 2;
        public static int SmashHydraPod = 2;
        public static int OpenedChest = 2;
        //tile ints
        public static int mireTiles = 0;
        public static int infernoTiles = 0;
        public static int voidTiles = 0;
        public static int mushTiles = 0;
        public static int terraTiles = 0;
        public static int stormTiles = 0;
        public static int pagodaTiles = 0;
        public static int lakeTiles = 0;
        public static int shipTiles = 0;
        public static int Radium = 0;
        public static int Darkmatter = 0;
        public static int DiscoBall = 0;
        public static int HoardTiles = 0;
        public static int CloudTiles = 0;
        //Worldgen
        public static bool TerrariumEnemies;
        public static bool Luminite;
        public static bool DarkMatter;
        public static bool HallowedOre;
        public static bool Dynaskull;
        public static bool ChaosOres;
        public static bool RadiumOre;
        public static bool AltarSmashed;
        public static int ChaosAltarsSmashed;
        public static int OreCount;
        public static bool DiscordOres;
        public static bool InfernoStripe;
        public static bool MireStripe;
        private int infernoSide = 0;
        private Vector2 infernoPos = new Vector2(0, 0);
        private Vector2 mirePos = new Vector2(0, 0);
        private Vector2 InfernoCenter = -Vector2.One;
        private Vector2 MireCenter = -Vector2.One;
        public static Vector2 shipPos = new Vector2(0, 0);
        public string nums = "1234567890";
        public static bool ModContentGenerated;

        public static bool Terra1;
        public static bool Terra2;
        public static bool Terra3;

        //Messages
        public static bool AMessage;
        public static bool Empowered;
        //Boss Bools
        public static bool Ancients;
        public static bool downedMonarch;
        public static bool downedGrips;
        public static bool downedBrood;
        public static bool downedHydra;
        public static bool downedSerpent;
        public static bool downedDjinn;
        public static bool downedRajah;
        public static bool downedDB;
        public static bool downedNC;
        public static bool downedEquinox;
        public static bool downedAncient;
        public static bool downedSAncient;
        public static bool downedAkuma;
        public static bool downedYamata;
        public static bool zeroUS;
        public static bool downedZero;
        public static bool downedAllAncients;
        public static bool ShenSummoned;
        public static bool downedShen;
        public static bool downedToad;
        public static bool downedFungus;
        public static bool downedAshe;
        public static bool downedHaruka;
        public static bool downedSisters;
        public static bool downedSag;
        public static bool SistersSummoned;
        public static bool downedRajahsRevenge;
        public static bool downedAnubis;
        public static bool downedAthena;
        public static bool downedAthenaA;
        public static bool downedGreed;
        public static bool downedGreedA;
        public static bool AthenaHerald;
        public static bool downedAnubisA;
        public static bool downedAABoss;
        public static bool downedLucifer;
        public static bool downedCore;

        public static bool AnubisAwakened;
        public static bool WormActive;
        public static bool StarActive;
        public static bool GravActive;

        public static bool spawnGrips;
        //Points
        public static Point WHERESDAVOIDAT;

        //Squid Lady
        public static int squid1 = 0;
        public static int squid2 = 0;
        public static int squid3 = 0;
        public static int squid4 = 0;
        public static int squid5 = 0;
        public static int squid6 = 0;
        public static int squid7 = 0;
        public static int squid8 = 0;
        public static int squid9 = 0;
        public static int squid10 = 0;
        public static int squid11 = 0;
        public static int squid12 = 0;
        public static int squid13 = 0;
        public static int squid14 = 0;
        public static int squid15 = 0;
        public static int squid16 = 0;

        //Other
        public static bool Suncaller = false;
        public static bool Mooncaller = false;
        public static int RabbitKills = 0;
        public static bool TimeStopped = false;
        public static double PausedTime = 0;

        public static bool continueWorldGen = false;
        public static bool GenAAContent = true;
        #endregion

        #region Save/Load
        public override void Initialize()
        {
            //Bosses
            downedAnubis = false;
            downedAnubisA = false;
            downedAthena = false;
            downedAthenaA = false;
            downedGreed = false;
            downedGreedA = false;
            downedMonarch = false;
            downedGrips = false;
            downedEquinox = false;
            downedSAncient = false;
            downedAkuma = false;
            downedYamata = false;
            zeroUS = false;
            downedZero = false;
            downedShen = false;
            downedAllAncients = false;
            ShenSummoned = false;
            downedToad = false;
            downedFungus = false;
            downedDjinn = false;
            downedSerpent = false;
            downedBrood = false;
            downedHydra = false;
            downedAshe = false;
            downedHaruka = false;
            downedSisters = false;
            downedSag = false;
            SistersSummoned = false;
            downedRajah = false;
            AthenaHerald = false;
            downedAABoss = false;
            downedLucifer = false;
            downedCore = false;

            AnubisAwakened = false;
            WormActive = false;
            StarActive = false;
            GravActive = false;

            spawnGrips = false;
            //World Changes
            TerrariumEnemies = NPC.downedBoss2;
            ChaosOres = downedGrips;
            Dynaskull = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            AMessage = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            Luminite = NPC.downedMoonlord;
            RadiumOre = downedEquinox;
            DiscordOres = downedSisters;
            InfernoStripe = Main.hardMode;
            MireStripe = Main.hardMode;
            ModContentGenerated = false;
            Empowered = downedShen;
            mirePos = new Vector2(0, 0);
            infernoPos = new Vector2(0, 0);
            InfernoCenter = -Vector2.One;
            MireCenter = -Vector2.One;
            SmashDragonEgg = 2;
            SmashHydraPod = 2;
            //Squid Lady
            squid1 = 0;
            squid2 = 0;
            squid3 = 0;
            squid4 = 0;
            squid5 = 0;
            squid6 = 0;
            squid7 = 0;
            squid8 = 0;
            squid9 = 0;
            squid10 = 0;
            squid11 = 0;
            squid12 = 0;
            squid13 = 0;
            squid14 = 0;
            squid15 = 0;
            squid16 = 0;
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
                AAMod.instance.Logger.Error($"{e} \n{i}, {j}");
            }
            return valid;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedMonarch) downed.Add("MUSHMAN");
            if (downedGrips) downed.Add("GrabbyHands");
            if (downedHydra) downed.Add("Hydra");
            if (downedBrood) downed.Add("Nacho");
            if (AMessage) downed.Add("AMessage");
            if (downedEquinox) downed.Add("Equinox");
            if (Ancients) downed.Add("AA");
            if (downedAncient) downed.Add("A");
            if (downedSAncient) downed.Add("SA");
            if (downedAkuma) downed.Add("Akuma");
            if (downedYamata) downed.Add("Yamata");
            if (downedZero) downed.Add("0");
            if (downedShen) downed.Add("Shen");
            if (downedAllAncients) downed.Add("DAA");
            if (ShenSummoned) downed.Add("ShenS");
            if (downedSerpent) downed.Add("Serpent");
            if (downedDjinn) downed.Add("JojoReference");
            if (downedToad) downed.Add("Toad");
            if (downedFungus) downed.Add("Fungus");
            if (InfernoStripe) downed.Add("IStripe");
            if (MireStripe) downed.Add("MStripe");
            if (downedAshe) downed.Add("BetterDragonWaifu");
            if (downedHaruka) downed.Add("TrashDragonWaifu");
            if (downedSisters) downed.Add("Sisters");
            if (downedSag) downed.Add("Sag");
            if (ModContentGenerated) downed.Add("WorldGenned");
            if (SistersSummoned) downed.Add("Summoned");
            if (downedRajah) downed.Add("Rajah");
            if (downedRajahsRevenge) downed.Add("Rajah2");
            if (zeroUS) downed.Add("ZUS");
            if (downedAnubis) downed.Add("Doggo");
            if (downedAnubisA) downed.Add("AngryDoggo");
            if (downedAthena) downed.Add("BirdBitch");
            if (downedAthenaA) downed.Add("BirdBitchA");
            if (downedGreed) downed.Add("GimmeGimme");
            if (downedGreedA) downed.Add("WOOOORMS");
            if (AthenaHerald) downed.Add("BitchBird");
            if (downedLucifer) downed.Add("L");
            if (downedCore) downed.Add("Core");

            if (AnubisAwakened) downed.Add("AnuA");
            if (WormActive) downed.Add("WormA");
            if (StarActive) downed.Add("StarA");
            if (GravActive) downed.Add("GravA");

            return new TagCompound {
                {"downed", downed},
                {"MCenter", MireCenter },
                {"ICenter", InfernoCenter },
                {"squid1", squid1},
                {"squid2", squid2},
                {"squid3", squid3},
                {"squid4", squid4},
                {"squid5", squid5},
                {"squid6", squid6},
                {"squid7", squid7},
                {"squid8", squid8},
                {"squid9", squid9},
                {"squid10", squid10},
                {"squid11", squid11},
                {"squid12", squid12},
                {"squid13", squid13},
                {"squid14", squid14},
                {"squid15", squid15},
                {"squid16", squid16},
                {"Bunny", RabbitKills},
                {"Egg", SmashDragonEgg},
                {"Pod", SmashHydraPod}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            //bosses
            downedMonarch = downed.Contains("MUSHMAN");
            downedGrips = downed.Contains("GrabbyHands");
            downedBrood = downed.Contains("Nacho");
            downedHydra = downed.Contains("Hydra");
            AMessage = downed.Contains("AMessage");
            downedEquinox = downed.Contains("Equinox");
            downedAncient = downed.Contains("A");
            downedSAncient = downed.Contains("SA");
            downedAkuma = downed.Contains("Akuma");
            downedYamata = downed.Contains("Yamata");
            downedZero = downed.Contains("0");
            downedShen = downed.Contains("Shen");
            downedAllAncients = downed.Contains("DAA");
            Ancients = downed.Contains("AA");
            ShenSummoned = downed.Contains("ShenS");
            downedSerpent = downed.Contains("Serpent");
            downedDjinn = downed.Contains("JojoReference");
            downedToad = downed.Contains("Toad");
            downedFungus = downed.Contains("Fungus");
            downedAshe = downed.Contains("BetterDragonWaifu");
            downedHaruka = downed.Contains("TrashDragonWaifu");
            downedSisters = downed.Contains("Sisters");
            downedSag = downed.Contains("Sag");
            SistersSummoned = downed.Contains("Summoned");
            downedRajah = downed.Contains("Rajah");
            downedRajahsRevenge = downed.Contains("Rajah2");
            zeroUS = downed.Contains("ZUS");
            downedAnubis = downed.Contains("Doggo");
            downedAnubisA = downed.Contains("AngryDoggo");
            downedAthena = downed.Contains("BirdBitch");
            downedAthenaA = downed.Contains("BirdBitchA");
            downedGreed = downed.Contains("GimmeGimme");
            downedGreedA = downed.Contains("WOOOORMS");
            AthenaHerald = downed.Contains("BitchBird");
            downedLucifer = downed.Contains("L");
            downedCore = downed.Contains("Core");

            AnubisAwakened = downed.Contains("AnuA");
            WormActive = downed.Contains("WormA");
            WormActive = downed.Contains("StarA");
            WormActive = downed.Contains("GravA");
            //World Changes
            ChaosOres = downedGrips;
            Dynaskull = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            AMessage = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            Luminite = NPC.downedMoonlord;
            RadiumOre = downedEquinox;
            DiscordOres = downedSisters;
            InfernoStripe = downed.Contains("IStripe");
            MireStripe = downed.Contains("MStripe");
            ModContentGenerated = downed.Contains("WorldGenned");

            Terra1 = downedBrood || downedHydra || NPC.downedBoss2;
            Terra2 = NPC.downedPlantBoss;
            Terra3 = downedShen;

            if (tag.ContainsKey("MCenter")) // check if the altar coordinates exist in the save file
            {
                MireCenter = tag.Get<Vector2>("MCenter");
            }
            if (tag.ContainsKey("ICenter")) // check if the altar coordinates exist in the save file
            {
                InfernoCenter = tag.Get<Vector2>("ICenter");
            }
            //Squid Lady

            squid1 = tag.GetInt("squid1");
            squid2 = tag.GetInt("squid2");
            squid3 = tag.GetInt("squid3");
            squid4 = tag.GetInt("squid4");
            squid5 = tag.GetInt("squid5");
            squid6 = tag.GetInt("squid6");
            squid7 = tag.GetInt("squid7");
            squid8 = tag.GetInt("squid8");
            squid9 = tag.GetInt("squid9");
            squid10 = tag.GetInt("squid10");
            squid11 = tag.GetInt("squid11");
            squid12 = tag.GetInt("squid12");
            squid13 = tag.GetInt("squid13");
            squid14 = tag.GetInt("squid14");
            squid15 = tag.GetInt("squid15");
            squid16 = tag.GetInt("squid16");
            RabbitKills = tag.GetInt("Bunny");
            SmashDragonEgg = tag.GetInt("Egg");
            SmashHydraPod = tag.GetInt("Pod");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedMonarch;
            flags[1] = downedAncient;
            flags[2] = downedGrips;
            flags[3] = downedBrood;
            flags[4] = downedHydra;
            flags[5] = ModContentGenerated;
            flags[6] = downedRajah;
            flags[7] = downedRajahsRevenge;
            writer.Write(flags);

            BitsByte flags2 = new BitsByte();
            flags2[0] = zeroUS;
            flags2[1] = downedAshe;
            flags2[2] = downedHaruka;
            flags2[3] = SistersSummoned;
            flags2[4] = downedSisters;
            flags2[5] = downedSag;
            flags2[6] = downedEquinox;
            flags2[7] = downedAkuma;
            writer.Write(flags2);

            BitsByte flags3 = new BitsByte();
            flags3[0] = downedAllAncients;
            flags3[1] = downedYamata;
            flags3[2] = InfernoStripe;
            flags3[3] = MireStripe;
            flags3[4] = downedZero;
            flags3[5] = downedSAncient;
            flags3[6] = downedShen;
            flags3[7] = downedFungus;
            writer.Write(flags3);


            BitsByte flags4 = new BitsByte();
            flags4[0] = Ancients;
            flags4[1] = ShenSummoned;
            flags4[2] = downedSerpent;
            flags4[3] = downedDjinn;
            flags4[4] = downedToad;
            flags4[5] = downedAnubis;
            flags4[6] = downedAthena;
            flags4[7] = downedGreed;
            writer.Write(flags4);


            BitsByte flags5 = new BitsByte();
            flags5[0] = AthenaHerald;
            flags5[1] = downedAthenaA;
            flags5[2] = downedGreedA;
            flags5[3] = AnubisAwakened;
            flags5[4] = downedAnubisA;
            flags5[5] = WormActive;
            flags5[6] = StarActive;
            flags5[7] = GravActive;
            writer.Write(flags5);


            BitsByte flags6 = new BitsByte();
            flags6[0] = downedLucifer;
            flags6[1] = downedCore;
            writer.Write(flags6);

            writer.WriteVector2(MireCenter);
            writer.WriteVector2(InfernoCenter);

            writer.Write(squid1);
            writer.Write(squid2);
            writer.Write(squid3);
            writer.Write(squid4);
            writer.Write(squid5);
            writer.Write(squid6);
            writer.Write(squid7);
            writer.Write(squid8);
            writer.Write(squid9);
            writer.Write(squid10);
            writer.Write(squid11);
            writer.Write(squid12);
            writer.Write(squid13);
            writer.Write(squid14);
            writer.Write(squid15);
            writer.Write(squid16);
            writer.Write(RabbitKills);
            writer.Write(SmashDragonEgg);
            writer.Write(SmashHydraPod);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedMonarch = flags[0];
            downedAncient = flags[1];
            downedGrips = flags[2];
            downedBrood = flags[3];
            downedHydra = flags[4];
            ModContentGenerated = flags[5];
            downedRajah = flags[6];
            downedRajahsRevenge = flags[7];

            BitsByte flags2 = reader.ReadByte();
            zeroUS = flags2[0];
            downedAshe = flags2[1];
            downedHaruka = flags2[2];
            SistersSummoned = flags2[3];
            downedSisters = flags2[4];
            downedSag = flags2[5];
            downedEquinox = flags2[6];
            downedAkuma = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            downedAllAncients = flags3[0];
            downedYamata = flags3[1];
            InfernoStripe = flags3[2];
            MireStripe = flags3[3];
            downedZero = flags3[4];
            downedSAncient = flags3[5];
            downedShen = flags3[6];
            downedFungus = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            Ancients = flags4[0];
            ShenSummoned = flags4[1];
            downedSerpent = flags4[2];
            downedDjinn = flags4[3];
            downedToad = flags4[4];
            downedAnubis = flags4[5];
            downedAthena = flags4[6];
            downedGreed = flags4[7];

            BitsByte flags5 = reader.ReadByte();
            AthenaHerald = flags5[0];
            downedAthenaA = flags5[1];
            downedGreedA = flags5[2];
            AnubisAwakened = flags5[3];
            downedAnubisA = flags5[4];
            WormActive = flags5[5];
            StarActive = flags5[6];
            GravActive = flags5[7];

            BitsByte flags6 = reader.ReadByte();
            downedLucifer = flags6[0];
            downedCore = flags6[1];

            MireCenter = reader.ReadVector2();
            InfernoCenter = reader.ReadVector2();

            squid1 = reader.ReadInt32();
            squid2 = reader.ReadInt32();
            squid3 = reader.ReadInt32();
            squid4 = reader.ReadInt32();
            squid5 = reader.ReadInt32();
            squid6 = reader.ReadInt32();
            squid7 = reader.ReadInt32();
            squid8 = reader.ReadInt32();
            squid9 = reader.ReadInt32();
            squid10 = reader.ReadInt32();
            squid11 = reader.ReadInt32();
            squid12 = reader.ReadInt32();
            squid13 = reader.ReadInt32();
            squid14 = reader.ReadInt32();
            squid15 = reader.ReadInt32();
            squid16 = reader.ReadInt32();
            RabbitKills = reader.ReadInt32();
            SmashHydraPod = reader.ReadInt32();
            SmashDragonEgg = reader.ReadInt32();
        }

        #endregion

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
            if (!GenAAContent && Main.maxTilesX <= 4200)
            {
                return;
            }

            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (shiniesIndex > -1)
            {
                tasks.Insert(shiniesIndex + 2, new PassLegacy("SmallWorldWarning", delegate (GenerationProgress progress)
                {
                    SmallWarning();
                }));
                tasks.Insert(shiniesIndex + 2, new PassLegacy("Prisms", delegate (GenerationProgress progress)
                {
                    GenPrisms(progress);
                }));
                tasks.Insert(shiniesIndex + 3, new PassLegacy("Abyssium", delegate (GenerationProgress progress)
                {
                    GenAbyssium();
                }));
                tasks.Insert(shiniesIndex + 4, new PassLegacy("Incinerite", delegate (GenerationProgress progress)
                {
                    GenIncinerite();
                }));
                tasks.Insert(shiniesIndex + 5, new PassLegacy("Everleaf", delegate (GenerationProgress progress)
                {
                    GenEverleaf();
                }));
                tasks.Insert(shiniesIndex + 6, new PassLegacy("Relic", delegate (GenerationProgress progress)
                {
                    GenRelicOre();
                }));
            }

            int ChaosIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (ChaosIndex > -1)
            {
                tasks.Insert(ChaosIndex + 1, new PassLegacy("Mire and Inferno", delegate (GenerationProgress progress)
                {
                    MireAndInferno(progress);
                }));
            }


            int shiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Larva"));

            if (shiniesIndex1 > -1)
            {
                tasks.Insert(ChaosIndex + 2, new PassLegacy("The Pit", delegate (GenerationProgress progress)
                {
                    ThePitTeaser(progress);
                }));
            }

            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (shiniesIndex2 > -1)
            {

                tasks.Insert(shiniesIndex2, new PassLegacy("Ender", delegate (GenerationProgress progress)
                {
                    EnderShrine();
                }));

                tasks.Insert(shiniesIndex2 + 1, new PassLegacy("LivingBogwoodConvert", delegate (GenerationProgress progress)
                {
                    BogwoodConvert(progress);
                }));

                tasks.Insert(shiniesIndex2 + 2, new PassLegacy("Hoard", delegate (GenerationProgress progress)
                {
                    Hoard(progress);
                }));

                tasks.Insert(shiniesIndex2 + 3, new PassLegacy("Terrarium", delegate (GenerationProgress progress)
                {
                    Terrarium(progress);
                }));

                tasks.Insert(shiniesIndex2 + 4, new PassLegacy("Acropolis", delegate (GenerationProgress progress)
                {
                    Acropolis(progress);
                }));

                tasks.Insert(shiniesIndex2 + 5, new PassLegacy("Void Islands", delegate (GenerationProgress progress)
                {
                    VoidIslands(progress);
                }));

                tasks.Insert(shiniesIndex2 + 6, new PassLegacy("Altars", delegate (GenerationProgress progress)
                {
                    Altars(progress);
                }));

                tasks.Insert(shiniesIndex2 + 7, new PassLegacy("Equinox", delegate (GenerationProgress progress)
                {
                    EquinoxAlt(progress);
                }));
            }

            int DungeonChests = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Dungeon"));
            if (DungeonChests >= 0)
            {
                tasks.Insert(DungeonChests + 1, new PassLegacy("InfernoChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("InfernoChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("DragonriderStaff"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("RadiantIncinerite") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("DragonfireFlask") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));

                tasks.Insert(DungeonChests + 2, new PassLegacy("MireChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("MireChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("BogBomb"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("DeepAbyssium") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("HydratoxinFlask") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));


                tasks.Insert(DungeonChests + 3, new PassLegacy("VoidChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("DoomsdayChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("SingularityCannon"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("DoomiteScrap") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("Doomite") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));
            }

            ModContentGenerated = true;
        }

        public void SmallWarning()
        {
            Main.MenuUI.SetState(new WarningUI());
            while (true)
            {
                if (continueWorldGen)
                {
                    Main.MenuUI.GoBack();
                    continueWorldGen = false;
                    break;
                }
            }
        }

        class WarningUI : UIState
        {
            public UIPanel coinCounterPanel;
            private UIText buttonLabel;

            public override void OnInitialize()
            {
                coinCounterPanel = new UIPanel();
                coinCounterPanel.SetPadding(0);
                coinCounterPanel.HAlign = .5f;
                coinCounterPanel.VAlign = .5f;
                coinCounterPanel.Width.Set(300f, 0f);
                coinCounterPanel.Height.Set(100f, 0f);
                coinCounterPanel.BackgroundColor = new Color(73, 94, 171);

                UITextPanel<string> text = new UITextPanel<string>("Ancients Awakened does not work well at all on small worlds. It is highly recommended you generate on a medium or larger world! Do you wish to continue?")
                {
                    HAlign = 0.5f
                };
                text.Top.Set(5f, 0f);
                coinCounterPanel.Append(text);

                buttonLabel = new UIText("", 1f, false)
                {
                    VAlign = .8f,
                    HAlign = .5f
                };
                coinCounterPanel.Append(buttonLabel);

                Texture2D buttonPlayTexture = ModContent.GetTexture("Terraria/UI/ButtonPlay");
                UIImageButton playButton = new UIImageButton(buttonPlayTexture);
                playButton.Width.Set(22, 0f);
                playButton.Height.Set(22, 0f);
                playButton.VAlign = .8f;
                playButton.HAlign = .25f;
                playButton.OnClick += new MouseEvent(PlayButtonClicked);
                playButton.OnMouseOver += new MouseEvent(PlayMouseOver);
                playButton.OnMouseOut += new MouseEvent(ButtonMouseOut);
                coinCounterPanel.Append(playButton);

                Texture2D buttonDeleteTexture = ModContent.GetTexture("Terraria/UI/ButtonDelete");
                UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
                closeButton.Width.Set(22, 0f);
                closeButton.Height.Set(22, 0f);
                closeButton.VAlign = .8f;
                closeButton.HAlign = .75f;
                closeButton.OnClick += new MouseEvent(CloseButtonClicked);
                closeButton.OnMouseOver += new MouseEvent(DeleteMouseOver);
                closeButton.OnMouseOut += new MouseEvent(ButtonMouseOut);
                coinCounterPanel.Append(closeButton);

                Append(coinCounterPanel);
            }

            private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
            {
                Main.PlaySound(SoundID.MenuOpen, -1, -1, 1);
                GenAAContent = true;
                continueWorldGen = true;
            }

            private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
            {
                Main.PlaySound(SoundID.MenuOpen, -1, -1, 1);
                GenAAContent = true;
                continueWorldGen = false;
            }

            private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
            {
                buttonLabel.SetText("I like suffering");
            }

            private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
            {
                buttonLabel.SetText("Thanks for Letting me know, I'll generate a larger world.");
            }

            private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
            {
                buttonLabel.SetText("");
            }
        }


        private void GenIncinerite()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 1)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("IncineriteOre"));
                }
            }
        }

        private void GenEverleaf()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, x);
                int tilesY = WorldGen.genRand.Next(0, y);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)mod.TileType("EverleafRoot"));
                }
            }
        }

        private void GenAbyssium()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("AbyssiumOre"));
                }
            }
        }

        private void GenRelicOre()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-04); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next(0, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == TileID.IceBlock)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("RelicOre"));
                }
            }
        }

        private void GenPrisms(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("LegacyWorldGen.23");
            int amount = (int)(Main.maxTilesX * 0.4f * 0.2f);
            for (int k = 0; k < amount; k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                while (Main.tile[x, y].type != 1)
                {
                    x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                }
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), ModContent.TileType<PrismOre>());
            }
        }

        public void VoidIslands(GenerationProgress progress)
        {
            progress.Message = "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0";

            progress.Set(0f);
            int VoidHeight = 0;
            progress.Set(0.1f);
            VoidHeight = 120;
            progress.Set(0.4f);
            Point center = new Point((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, center.Y = VoidHeight);
            WHERESDAVOIDAT = center;
            progress.Set(0.5f);
            Point oldposition = new Point(1, 1);
            progress.Set(0.6f);
            List<Point> posIslands = new List<Point>();
            progress.Set(0.7f);
            int IslandNumber = 2;
            if (GetWorldSize() != 1)
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
            progress.Set(0.85f);
            for (int j = 0; j < posIslands.Count; ++j)
            {
                Point position = posIslands[j];
                position.X -= 4;
                position.Y -= 11;
                VoidHouses(position.X, position.Y, (ushort)mod.TileType("DoomitePlate"), 10, 7);
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
                int repY = (size / 2) - Math.Abs(i);
                int offset = repY / 5;
                repY += WorldGen.genRand.Next(4);
                for (int j = -offset; j < repY; ++j)
                {
                    WorldGen.PlaceTile(position.X + i, position.Y + j, ModContent.TileType<Doomstone>());
                }
                int y = Raycast(position.X + i, position.Y - 5);
                WorldGen.PlaceObject(position.X + i, y, mod.TileType("OroborosTree"));
                WorldGen.GrowTree(position.X + i, y);
            }
        }

        private void Altars(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildAltars");
            for (int num = 0; num < Main.maxTilesX / 500; num++)
            {
                int xAxis = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                int yAxis = WorldGen.genRand.Next((int)WorldGen.rockLayer + 150, Main.maxTilesY - 250);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        Tile tile = Main.tile[AltarX, AltarY];
                        int Altar = Main.rand.Next(2);

                        if (Altar == 0)
                        {
                            Altar = ModContent.TileType<ChaosAltar1>();
                        }
                        else
                        {
                            Altar = ModContent.TileType<ChaosAltar2>();
                        }

                        if ((tile.type == ModContent.TileType<Torchstone>() ||
                            tile.type == ModContent.TileType<Torchsand>() ||
                            tile.type == ModContent.TileType<Torchice>() ||
                            tile.type == ModContent.TileType<Torchsandstone>() ||
                            tile.type == ModContent.TileType<Torchsand>() ||
                            tile.type == ModContent.TileType<InfernoGrass>())
                            && Altar == ModContent.TileType<ChaosAltar1>())
                        {
                            Altar = ModContent.TileType<ChaosAltar2>();
                        }
                        if ((tile.type == ModContent.TileType<Depthstone>() ||
                            tile.type == ModContent.TileType<Depthsand>() ||
                            tile.type == ModContent.TileType<IndigoIce>() ||
                            tile.type == ModContent.TileType<Depthsandstone>() ||
                            tile.type == ModContent.TileType<Depthsand>() ||
                            tile.type == ModContent.TileType<MireGrass>())
                            && Altar == ModContent.TileType<ChaosAltar2>())
                        {
                            Altar = ModContent.TileType<ChaosAltar1>();
                        }

                        if (Main.rand.Next(15) == 0 && tile.type != ModContent.TileType<KeepBrick>() && tile.type != ModContent.TileType<TerraBrick>())
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, Altar);
                        }
                    }
                }
            }
        }

        public int ChestNumber = 0;

        public void VoidHouses(int X, int Y, int type = 30, int sizeX = 10, int sizeY = 7)
        {
            int wallID = (ushort)mod.WallType("DoomiteWall");
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
            };
            //Side placements
            for (int i = Y; i < Y + sizeY - 1; ++i)
            {
                WorldGen.PlaceTile(X, i, type);
                WorldGen.PlaceTile(X + (sizeX - 2), i, (ushort)mod.TileType("DoomitePlate"));
            }
            //Roof-floor placements
            for (int i = X; i < X + sizeX - 2; ++i)
            {
                WorldGen.PlaceTile(i, Y, type);
                WorldGen.PlaceTile(i, Y + (sizeY - 1), (ushort)mod.TileType("DoomitePlate"));
            }
            WorldGen.PlaceTile(X + sizeX - 2, Y + sizeY - 1, (ushort)mod.TileType("DoomitePlate"));

            int PlacementSuccess = WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChest"), true);
            if (PlacementSuccess >= 0)
            {
                Chest chest = Main.chest[PlacementSuccess];
                if (ChestNumber == 0)
                {
                    VoidLoot(mod.ItemType("Voidsaber"), chest);
                }
                else if (ChestNumber == 1)
                {
                    VoidLoot(mod.ItemType("DoomGun"), chest);
                }
                else if (ChestNumber == 2)
                {
                    VoidLoot(mod.ItemType("DoomStaff"), chest);

                }
                else if (ChestNumber == 3)
                {
                    VoidLoot(mod.ItemType("ProbeControlUnit"), chest);
                }
                ChestNumber += 1;
            }
            //Side holes
            for (int i = Y + sizeY - 4; i > Y + sizeY; --i)
            {
                WorldGen.KillTile(X, i);
            }
        }

        public void VoidLoot(int Item, Chest chest)
        {
            chest.item[0].SetDefaults(Item, false);
            chest.item[1].SetDefaults(mod.ItemType("DoomiteScrap"), false);
            chest.item[1].stack = WorldGen.genRand.Next(4, 6);
            Item item = chest.item[2];
            UnifiedRandom genRand = WorldGen.genRand;
            int[] array2 = new int[]
            { 302, 2327, 2351, 304, 2329 };
            item.SetDefaults(Utils.Next(genRand, array2), false);
            chest.item[2].stack = WorldGen.genRand.Next(1, 3);
            chest.item[3].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
            { 282, 286 }), false);
            chest.item[3].stack = WorldGen.genRand.Next(15, 31);
            chest.item[4].SetDefaults(73, false);
            chest.item[4].stack = WorldGen.genRand.Next(1, 3);
        }

        public override void PostWorldGen()
        {
            int[] itemsToPlaceInDungeonChests = new int[] { mod.ItemType("SkullStaff") };
            int itemsToPlaceInDungeonChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice]);
                                itemsToPlaceInDungeonChestsChoice = (itemsToPlaceInDungeonChestsChoice + 1) % itemsToPlaceInDungeonChests.Length;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
        public int HeraldTimer = 1200;

        public override void PostUpdate()
        {
            if (downedAnubisA && !AthenaHerald && !downedAthenaA)
            {
                if (HeraldTimer > 0)
                {
                    HeraldTimer--;
                }
                else
                {
                    Player player = Main.player[BaseAI.GetPlayer(new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2), -1)];
                    Vector2 spawnpoint = player.Center - new Vector2(250, 200);
                    int Seraph = NPC.NewNPC((int)spawnpoint.X, (int)spawnpoint.Y, ModContent.NPCType<NPCs.Bosses.Athena.SeraphHerald>());
                    NPC Seraph1 = Main.npc[Seraph];
                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDust(Seraph1.position, Seraph1.height, Seraph1.width, ModContent.DustType<NPCs.Bosses.Athena.Feather>(), Main.rand.Next(-1, 2), 1, 0);
                    }
                    AthenaHerald = true;
                }
            }

            if (!Main.dayTime)
            {
                if (!Main.fastForwardTime)
                {
                    if (Main.time == 1 && !WorldGen.spawnEye)
                    {
                        if (!downedGrips && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool flag3 = false;
                            for (int n = 0; n < 255; n++)
                            {
                                if (Main.player[n].active && Main.player[n].statLifeMax >= 200 && Main.player[n].statDefense > 10)
                                {
                                    flag3 = true;
                                    break;
                                }
                            }
                            if (flag3 && Main.rand.Next(3) == 0)
                            {
                                int num8 = 0;
                                for (int num9 = 0; num9 < 200; num9++)
                                {
                                    if (Main.npc[num9].active && Main.npc[num9].townNPC)
                                    {
                                        num8++;
                                    }
                                }
                                if (num8 >= 4)
                                {
                                    spawnGrips = true;
                                    if (Main.netMode == NetmodeID.SinglePlayer)
                                    {
                                        Main.NewText(Lang.BossSummonsInfo("GripsAwoken"), 50, 255, 130, false);
                                    }
                                    else if (Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.SendData(MessageID.ChatText, -1, -1, null, 255, 50f, 255f, 130f, 0, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                    if (spawnGrips && Main.netMode != NetmodeID.MultiplayerClient && Main.time > 4860.0)
                    {
                        for (int k = 0; k < 255; k++)
                        {
                            if (Main.player[k].active && !Main.player[k].dead && Main.player[k].position.Y < Main.worldSurface * 16.0)
                            {
                                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
                                else if (Main.netMode == NetmodeID.Server)
                                    if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
                                    else if (Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAMod.Grips.GripsofChaosAwoken")), new Color(175, 75, 255), -1);
                                    }
                                AAModGlobalNPC.SpawnBoss(Main.player[k], mod.NPCType("GripOfChaosBlue"), false, 1, 0);
                                AAModGlobalNPC.SpawnBoss(Main.player[k], mod.NPCType("GripOfChaosRed"), false, -1, 0);
                                spawnGrips = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (downedEquinox)
            {
                if (RadiumOre == false)
                {
                    RadiumOre = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedEquinoxInfo"), Color.Violet);
                    for (int i = 0; i < Main.maxTilesX / 50; ++i)
                    {
                        int X = WorldGen.genRand.Next(Main.maxTilesX / 10 * 2, (int)(Main.maxTilesX / 10 * 4.5f));
                        int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 6); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                        {
                            for (int y = Y - radius; y <= Y + radius; y++)
                            {
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                {
                                    WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                                }
                            }
                        }
                    }
                    for (int i = 0; i < Main.maxTilesX / 50; ++i)
                    {
                        int X = WorldGen.genRand.Next((int)(Main.maxTilesX / 10 * 5.5f), Main.maxTilesX / 10 * 8);
                        int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 6); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                        {
                            for (int y = Y - radius; y <= Y + radius; y++)
                            {
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                {
                                    WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                                }
                            }
                        }
                    }
                }
            }
            if (NPC.downedMoonlord)
            {
                if (Ancients == false)
                {
                    Ancients = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedMoonlordInfo1"), Color.ForestGreen);
                }
                if (Luminite == false)
                {
                    Luminite = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedMoonlordInfo2"), Color.DarkSeaGreen);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(6, 10), (ushort)mod.TileType("LuminiteOre"));
                    }
                    return;
                }
            }
            if (NPC.downedMechBossAny)
            {
                if (HallowedOre == false)
                {
                    HallowedOre = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedMechBossAnyInfo"), Color.Goldenrod);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(4, 9), (ushort)ModContent.TileType<HallowedOre>());
                    }
                }
            }

            if (downedSisters)
            {
                if (!DiscordOres)
                {
                    DiscordOres = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedSistersInfo"), Color.Magenta);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].type == 59)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EventideAbyssiumOre"));
                        }
                    }
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].type == 1)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DaybreakIncineriteOre"));
                        }
                    }
                }
            }

            if (NPC.downedBoss2)
            {
                if (!Terra1)
                {
                    Terra1 = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedBoss2Info"), Color.LimeGreen);
                    for (int j = 0; j < Main.maxTilesX; j++)
                    {
                        for (int k = 0; k < Main.maxTilesY; k++)
                        {
                            if (Main.tile[j, k].active() && Main.tile[j, k].type == (ushort)ModContent.TileType<TerraDoor>())
                            {
                                WorldGen.KillTile(j, k, false, false, true);
                            }
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss)
            {
                if (!Terra2)
                {
                    Terra2 = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedPlantBossInfo"), Color.LimeGreen);
                    for (int j = 0; j < Main.maxTilesX; j++)
                    {
                        for (int k = 0; k < Main.maxTilesY; k++)
                        {
                            if (Main.tile[j, k].active() && Main.tile[j, k].type == (ushort)ModContent.TileType<TerraGate>())
                            {
                                WorldGen.KillTile(j, k, false, false, true);
                            }
                        }
                    }
                }
            }
            if (downedShen)
            {
                if (!Terra3)
                {
                    Terra3 = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.Worldtext("downedShenInfo"), Color.LimeGreen);
                    for (int j = 0; j < Main.maxTilesX; j++)
                    {
                        for (int k = 0; k < Main.maxTilesY; k++)
                        {
                            if (Main.tile[j, k].active() && Main.tile[j, k].type == (ushort)ModContent.TileType<TerraVault>())
                            {
                                WorldGen.KillTile(j, k, false, false, true);
                            }
                        }
                    }
                }
            }

            if (NPC.downedBoss3)
            {
                if (!Dynaskull)
                {
                    Dynaskull = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedBoss3Info1"), Color.DarkOrange);
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedBoss3Info2"), Color.Orange);
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedBoss3Info3"), Color.Cyan);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next(0, y);
                        if (Main.tile[tilesX, tilesY].type == 397)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DynaskullOre"));
                        }
                    }
                }
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                if (!AMessage)
                {
                    AMessage = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedMechBossInfo"), Color.Gold.R, Color.Gold.G, Color.Gold.B);
                }
            }

            if (downedAkuma || downedYamata || downedZero)
            {
                downedAncient = true;
            }

            if (downedShen)
            {
                downedSAncient = true;
            }

            if (downedAkuma && downedYamata)
            {
                if (downedAllAncients == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.downedAllAncientsInfo"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    downedAllAncients = true;
                }
            }
            if (Main.hardMode)
            {
                if (InfernoStripe == false)
                {
                    InfernoStripe = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.hardModeInfo"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                    ConversionHandler.ConvertDown((int)InfernoCenter.X, 0, 120, ConversionType.INFERNO);
                }
                if (MireStripe == false)
                {
                    MireStripe = true;

                    ConversionHandler.ConvertDown((int)MireCenter.X, 0, 120, ConversionType.MIRE);
                }
            }
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            Main.sandTiles += tileCounts[ModContent.TileType<Torchsand>()] + tileCounts[ModContent.TileType<Torchsandstone>()] + tileCounts[ModContent.TileType<TorchsandHardened>()] + tileCounts[ModContent.TileType<Depthsand>()] + tileCounts[ModContent.TileType<Depthsandstone>()] + tileCounts[ModContent.TileType<DepthsandHardened>()];
            Main.snowTiles += tileCounts[ModContent.TileType<Torchice>()] + tileCounts[ModContent.TileType<IndigoIce>()] + tileCounts[ModContent.TileType<TorchAsh>()];
            mireTiles = tileCounts[ModContent.TileType<MireGrass>()] + tileCounts[ModContent.TileType<Depthstone>()] + tileCounts[ModContent.TileType<Depthsand>()] + tileCounts[ModContent.TileType<Depthsandstone>()] + tileCounts[ModContent.TileType<DepthsandHardened>()] + tileCounts[ModContent.TileType<IndigoIce>()];
            infernoTiles = tileCounts[ModContent.TileType<InfernoGrass>()] + tileCounts[ModContent.TileType<Torchstone>()] + tileCounts[ModContent.TileType<Torchsand>()] + tileCounts[ModContent.TileType<Torchsandstone>()] + tileCounts[ModContent.TileType<TorchsandHardened>()] + tileCounts[ModContent.TileType<Torchice>()] + tileCounts[ModContent.TileType<TorchAsh>()];
            voidTiles = tileCounts[ModContent.TileType<Doomstone>()] + tileCounts[ModContent.TileType<Apocalyptite>()] + tileCounts[ModContent.TileType<Doomgrass>()] + tileCounts[ModContent.TileType<DoomstoneB>()];
            mushTiles = tileCounts[ModContent.TileType<Mycelium>()];
            Main.jungleTiles += mireTiles;
            pagodaTiles = tileCounts[ModContent.TileType<ScorchedDynastyWoodS>()] + tileCounts[ModContent.TileType<ScorchedShinglesS>()];
            lakeTiles = tileCounts[ModContent.TileType<Darkmud>()] + tileCounts[ModContent.TileType<AbyssGrass>()] + tileCounts[ModContent.TileType<AbyssWood>()] + tileCounts[ModContent.TileType<AbyssWoodSolid>()];
            terraTiles = tileCounts[ModContent.TileType<TerraCrystal>()] + tileCounts[ModContent.TileType<TerraWood>()] + tileCounts[ModContent.TileType<TerraLeaves>()] + tileCounts[ModContent.TileType<KeepBrick>()] + +tileCounts[ModContent.TileType<TerraBrick>()];
            Radium = tileCounts[ModContent.TileType<RadiumOre>()] + tileCounts[ModContent.TileType<Tiles.Altar.DaybringerBrick>()] + tileCounts[ModContent.TileType<Tiles.Altar.NightcrawlerBrick>()];
            HoardTiles = tileCounts[ModContent.TileType<GreedBrick>()] + tileCounts[ModContent.TileType<GreedStone>()];
            CloudTiles = tileCounts[ModContent.TileType<AcropolisBlock>()] + tileCounts[ModContent.TileType<AcropolisBlock2>()];
        }

        private void MireAndInferno(GenerationProgress progress)
        {
            infernoSide = (Main.dungeonX > Main.maxTilesX / 2) ? (-1) : 1;
            infernoPos.X = (Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
            mirePos.X = (Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
            int j = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)infernoPos.X, j] != null && !Main.tile[(int)infernoPos.X, j].active())
            {
                j++;
            }
            for (int l = (int)infernoPos.X - 25; l < (int)infernoPos.X + 25; l++)
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
            while (Main.tile[(int)mirePos.X, q] != null && !Main.tile[(int)mirePos.X, q].active())
            {
                q++;
            }
            for (int l = (int)mirePos.X - 25; l < (int)mirePos.X + 25; l++)
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

            InfernoCenter = infernoPos;

            MireCenter = mirePos;

            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildChaos");

            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildInferno");

            {
                Point origin = new Point((int)infernoPos.X, (int)infernoPos.Y);
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                InfernoBiome biome = new InfernoBiome();
                InfernoDelete delete = new InfernoDelete();
                delete.Place(origin, WorldGen.structures);
                biome.Place(origin, WorldGen.structures);
            }

            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildMire");

            {
                Point origin = new Point((int)mirePos.X, (int)mirePos.Y);
                origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
                MireDelete delete = new MireDelete();
                MireBiome biome = new MireBiome();
                delete.Place(origin, WorldGen.structures);
                biome.Place(origin, WorldGen.structures);
            }
        }

        private void BogwoodConvert(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildMire");
            Point origin = new Point((int)mirePos.X, (int)mirePos.Y);
            BogwoodCon biome = new BogwoodCon();
            biome.Place(origin, WorldGen.structures);
        }

        private void Terrarium(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildTerrarium");

            Point origin = new Point((int)(Main.maxTilesX * 0.35f), (int)(Main.maxTilesY * 0.38f));
            if (Main.dungeonX < Main.maxTilesX / 2)
            {
                origin = new Point((int)(Main.maxTilesX * 0.65f), (int)(Main.maxTilesY * 0.38f));
            }
            Keep biome = new Keep();
            biome.Place(origin, WorldGen.structures);
        }

        private void Acropolis(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildAcropolis");
            Point origin = new Point((int)(Main.maxTilesX * 0.65f), 100);
            Acropolis biome = new Acropolis();
            biome.Place(origin, WorldGen.structures);
        }

        private void Hoard(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildHoard");
            Point origin = new Point((int)(Main.maxTilesX * 0.3f), (int)(Main.maxTilesY * 0.65f));
            if (Main.dungeonX > Main.maxTilesX / 2)
            {
                origin = new Point((int)(Main.maxTilesX * 0.7f), (int)(Main.maxTilesY * 0.65f));
            }
            Hoard biome = new Hoard();
            HoardClear delete = new HoardClear();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

        private void EquinoxAlt(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAMod.Common.AAWorldBuildEquinoxAlt");
            Point origin = new Point((int)(Main.maxTilesX * 0.15f), 100);
            Equinox biome = new Equinox();
            biome.Place(origin, WorldGen.structures);
        }

        private void EnderShrine()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.2f), (int)(Main.maxTilesY * 0.75f));
            if (Main.dungeonX > Main.maxTilesX / 2)
            {
                origin = new Point((int)(Main.maxTilesX * 0.8f), (int)(Main.maxTilesY * 0.75f));
            }
            Crystal biome = new Crystal();
            biome.Place(origin, WorldGen.structures);
        }

        /*
        private void ThePit(GenerationProgress progress)
        {
            progress.Message = "Sinking the Pit";
            Point origin = new Point(Main.maxTilesX - 500, Main.maxTilesY - 200);
            Pit biome = new Pit();
            biome.Place(origin, WorldGen.structures);
        }
        */

        private void ThePitTeaser(GenerationProgress progress)
        {
            progress.Message = "Sinking the Pit";
            Point origin = new Point(Main.maxTilesX - 500, Main.maxTilesY - 170);
            PitTeaser biome = new PitTeaser();
            biome.Place(origin, WorldGen.structures);
        }


        public static int GetWorldSize()
        {
            if (Main.maxTilesX <= 4200) { return 1; }
            else if (Main.maxTilesX <= 6400) { return 2; }
            else if (Main.maxTilesX <= 8400) { return 3; }
            return 1;
        }

        public override void ResetNearbyTileEffects()
        {
            AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
            modPlayer.VoidUnit = false;
            modPlayer.SunAltar = false;
            modPlayer.MoonAltar = false;
            modPlayer.AkumaAltar = false;
            modPlayer.YamataAltar = false;
        }

        public static void AAConvert(int i, int j, int conversionType, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        bool sendNet = false;
                        if (conversionType == 1) //Inferno
                        {
                            if (WallID.Sets.Conversion.Stone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<TorchstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<TorchsandstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<TorchsandHardenedWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Grass[wall])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.WallType<InfernoGrassWall>();
                                WorldGen.SquareWallFrame(k, l);
                                sendNet = true;
                            }
                            if (TileID.Sets.Conversion.Stone[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Grass[type] && type != TileID.JungleGrass)
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<InfernoGrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Ice[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchice>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sand[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchsand>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<TorchsandHardened>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchsandstone>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (type == TileID.SnowBlock)
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<TorchAsh>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            if (sendNet)
                                NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (conversionType == 2) //Mire
                        {
                            if (WallID.Sets.Conversion.Stone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<DepthstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<DepthsandstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall])
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<DepthsandHardenedWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Grass[wall] || wall == WallID.JungleUnsafe || wall == WallID.JungleUnsafe1 || wall == WallID.JungleUnsafe2 || wall == WallID.JungleUnsafe3 || wall == WallID.JungleUnsafe4)
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<MireJungleWall>();
                                WorldGen.SquareWallFrame(k, l);
                                sendNet = true;
                            }
                            else if (wall == WallID.LivingLeaf)
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<LivingBogleafWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (wall == WallID.LivingWood)
                            {
                                Main.tile[k, l].wall = (ushort)ModContent.WallType<LivingBogwoodWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            if (TileID.Sets.Conversion.Stone[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Depthstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (type == TileID.JungleGrass)
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<MireGrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Ice[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<IndigoIce>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sand[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Depthsand>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<DepthsandHardened>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Depthsandstone>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (type == TileID.LivingMahogany)
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<LivingBogwood>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (type == TileID.LivingMahoganyLeaves)
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<LivingBogleaves>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            if (sendNet)
                                NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (conversionType == 3) //Void
                        {
                            if (TileID.Sets.Conversion.Stone[type] || TileID.Sets.Conversion.Moss[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<DoomstoneB>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Doomgrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 4) //Mushroom
                        {
                            if (WallID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.WallType<Mushwall>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Mycelium>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 5) //Fungicide
                        {
                            if (wall == WallID.Mushroom)
                            {
                                Main.tile[k, l].type = WallID.Jungle;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == WallID.MushroomUnsafe)
                            {
                                Main.tile[k, l].type = WallID.JungleUnsafe;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<Mushwall>())
                            {
                                Main.tile[k, l].type = WallID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }


                            if (type == TileID.MushroomGrass)
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == (ushort)ModContent.TileType<Mycelium>())
                            {
                                Main.tile[k, l].type = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 6) //Jungle
                        {
                            if (wall == 2)
                            {
                                Main.tile[k, l].wall = 15;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == 63)
                            {
                                Main.tile[k, l].wall = 64;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Stone[wall] && wall != WallID.Stone)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != WallID.HardenedSand)
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall] && wall != WallID.Sandstone)
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == 0 && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = TileID.Mud;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Grass[type] || type == TileID.MushroomGrass)
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Stone[type] && type != TileID.Stone)
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 3)
                            {
                                Main.tile[k, l].type = 61;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 52)
                            {
                                Main.tile[k, l].type = 62;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 73)
                            {
                                Main.tile[k, l].type = 74;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 7) //Jungle Removal
                        {
                            if (wall == 15)
                            {
                                Main.tile[k, l].wall = 2;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == 64)
                            {
                                Main.tile[k, l].wall = 63;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == TileID.Mud && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = 0;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            else if (type == 60)
                            {
                                Main.tile[k, l].type = 2;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 61)
                            {
                                Main.tile[k, l].type = 3;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 62)
                            {
                                Main.tile[k, l].type = 52;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 74)
                            {
                                Main.tile[k, l].type = 73;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 8) //Snow
                        {
                            if (wall == 2 || wall == 63 || wall == 65)
                            {
                                Main.tile[k, l].wall = 40;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == 0 && Main.tile[k, l].active() || type == 2 || type == 23 || type == 109 || type == 199)
                            {
                                Main.tile[k, l].type = 147;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 1)
                            {
                                Main.tile[k, l].type = 161;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 25)
                            {
                                Main.tile[k, l].type = 163;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 117)
                            {
                                Main.tile[k, l].type = 164;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 203)
                            {
                                Main.tile[k, l].type = 200;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<InfernoGrass>())
                            {
                                Main.tile[k, l].type = TileID.SnowBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchstone>())
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchice>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Depthstone>())
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<IndigoIce>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 9) //Snowmelt
                        {
                            if (wall == WallID.SnowWallUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.GrassUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == WallID.IceUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == TileID.SnowBlock)
                            {
                                if ((WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1].type == TileID.Trees) || (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1].type == TileID.Trees) ||
                                    (WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1] == null) ||
                                    (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null))
                                {
                                    Main.tile[k, l].type = 2;
                                }
                                else
                                {
                                    Main.tile[k, l].type = 0;
                                }
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.IceBlock)
                            {
                                Main.tile[k, l].type = 1;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.CorruptIce)
                            {
                                Main.tile[k, l].type = TileID.Ebonstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.HallowedIce)
                            {
                                Main.tile[k, l].type = TileID.Pearlstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.FleshIce)
                            {
                                Main.tile[k, l].type = TileID.Crimstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchice>())
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Torchstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<IndigoIce>())
                            {
                                Main.tile[k, l].type = (ushort)ModContent.TileType<Depthstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 11) //Order
                        {
                            if (wall == ModContent.WallType<TorchstoneWall>() || wall == ModContent.WallType<DepthstoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<InfernoGrassWall>())
                            {
                                Main.tile[k, l].wall = WallID.GrassUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<MireJungleWall>())
                            {
                                Main.tile[k, l].wall = WallID.JungleUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<TorchsandHardenedWall>() || wall == ModContent.WallType<DepthsandHardenedWall>())
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<TorchsandstoneWall>() || wall == ModContent.WallType<DepthsandstoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == ModContent.WallType<LivingBogwoodWall>())
                            {
                                Main.tile[k, l].wall = WallID.LivingWood;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == ModContent.TileType<InfernoGrass>() || type == ModContent.TileType<Doomgrass>())
                            {
                                Main.tile[k, l].type = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchstone>() || type == ModContent.TileType<Depthstone>())
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<MireGrass>())
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<TorchAsh>())
                            {
                                Main.tile[k, l].type = TileID.SnowBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchsand>() || type == ModContent.TileType<Depthsand>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<TorchsandHardened>() || type == ModContent.TileType<DepthsandHardened>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchsandstone>() || type == ModContent.TileType<Depthsandstone>())
                            {
                                Main.tile[k, l].type = TileID.Sandstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == ModContent.TileType<Torchice>() || type == ModContent.TileType<IndigoIce>())
                            {
                                Main.tile[k, l].type = TileID.IceBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                    }
                }
            }
        }
    }

    
}
