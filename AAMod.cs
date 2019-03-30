using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.UI;
using AAMod.Backgrounds;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System.Reflection;
using AAMod.Items.Materials;
using AAMod.Items.Armor.Darkmatter;
using AAMod.Items.Armor.Radium;
using AAMod.Items.Blocks;
using AAMod.Items.Vanity.Beg;
using AAMod;
using BaseMod;

namespace AAMod
{
    class AAMod : Mod
    {
        public static int GoblinSoul;
        public static ModHotKey InfinityHotKey;
        public static ModHotKey AbilityKey;
        internal static AAMod instance;
        internal UserInterface UserInterface;
        public static bool AkumaMusic = false;
        public static bool YamataMusic = false;
        public static bool AHIntro = false;
        public static bool Slayer = false;
        public static AAMod self = null;
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public static string BLANK_TEX = "AAMod/BlankTex";

        public static int[] SNAKETYPES = new int[0];
        public static int[] SERPENTTYPES = new int[0];

        #region mod loaded bools
        public static bool fargoLoaded = false;
        public static bool calamityLoaded = false;
        public static bool grealmLoaded = false;
        public static bool sacredToolsLoaded = false;
        public static bool spiritLoaded = false;
        public static bool thoriumLoaded = false;
        public static bool tremorLoaded = false;
        public static bool redemptionLoaded = false;
        public static bool cheatsheetLoaded = false;
        public static bool herosLoaded = false;
        #endregion

        public AAMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
            instance = this;
        }

        public static void SetupBannerItemTextures()
        {
            if (Main.netMode == 2 || Main.dedServ) return; //don't do any texture stuff on a server lol
            try
            {
                int fx = 16;
                Texture2D tex = Main.tileTexture[AAMod.instance.TileType("Banners")];

                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx);
                    if (name.Equals("DUMMY")) { fx += 16; continue; }
                    Main.itemTexture[AAMod.instance.ItemType(name + "Banner")] = BaseDrawing.GetCroppedTex(tex, new Rectangle(fx, 0, 16, 16 * 3));
                    fx += 16;
                }
            }
            catch (Exception e) { ErrorLogger.Log(e.Message); ErrorLogger.Log(e.StackTrace); }
        }

        public static FieldInfo _bannerField = null;
        public static IDictionary<int, int> bannerToItemDict
        {
            get
            {
                if (_bannerField == null)
                {
                    _bannerField = typeof(NPCLoader).GetField("bannerToItem", BindingFlags.NonPublic | BindingFlags.Static);
                }
                return (IDictionary<int, int>)_bannerField.GetValue(null);
            }
            set
            {
                if (_bannerField != null)
                {
                    _bannerField.SetValue(null, value);
                }
            }
        }

        public static void SetupBannerNPCs()
        {
            Mod mod = instance;
            try
            {
                IDictionary<int, int> bannerToItem = bannerToItemDict;
                int fx = 16;
                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx, false);
                    if (name.Equals("DUMMY")) { fx += 16; continue; }
                    if (name.Contains("Wyrmling"))
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "Wyrmling" : (m == 1 ? "WyrmlingBody" : (m == 2 ? "WyrmlingTail1" : "WyrmlingTail2")));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("Wyrmling");
                                npc.bannerItem = mod.ItemType("WyrmlingBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else
                    if (name.Contains("Wyrm"))
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "Wyrm" : (m == 1 ? "WyrmBody1" : (m == 2 ? "WyrmBody2" : (m == 3 ? "WyrmBody3" : "WyrmBody4"))));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("Wyrm");
                                npc.bannerItem = mod.ItemType("WyrmBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else
                    if (name.Contains("Snake"))
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            ModNPC npc = mod.GetNPC(m == 0 ? "SnakeHead" : (m == 1 ? "SnakeBody" : "SnakeTail"));
                            if (npc != null)
                            {
                                npc.banner = mod.NPCType("SnakeHead");
                                npc.bannerItem = mod.ItemType("SnakeBanner");
                                bannerToItem[npc.banner] = npc.bannerItem;
                            }
                        }
                    }
                    else
                    {
                        ModNPC npc = mod.GetNPC(name);
                        if (npc != null)
                        {
                            npc.banner = mod.NPCType(name);
                            npc.bannerItem = mod.ItemType(name + "Banner");
                            bannerToItem[npc.banner] = npc.bannerItem;
                        }
                    }
                    fx += 16;
                }
                bannerToItemDict = bannerToItem;
            }
            catch (Exception e) { ErrorLogger.Log(e.Message); ErrorLogger.Log(e.StackTrace); }
        }

        public override void PostSetupContent()
        {
            Mod DradonIsDum = ModLoader.GetMod("AchievementLibs");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            Mod Calamity = ModLoader.GetMod("CalamityMod");
            Mod Thorium = ModLoader.GetMod("ThoriumMod");
            Mod Spirit = ModLoader.GetMod("SpiritMod");
            Mod Fargos = ModLoader.GetMod("Fargowiltas");
            Mod GRealm = ModLoader.GetMod("GRealm");
            Mod SacredTools = ModLoader.GetMod("SacredTools");
            Mod Tremor = ModLoader.GetMod("Tremor");
            Mod Redemption = ModLoader.GetMod("Redemption");
            Mod CheatSheet = ModLoader.GetMod("CheatSheet");
            Mod HEROsMod = ModLoader.GetMod("HEROsMod");
            if (Calamity != null) calamityLoaded = true;
            if (Thorium != null) thoriumLoaded = true;
            if (Spirit != null) spiritLoaded = true;
            if (Fargos != null) fargoLoaded = true;
            if (GRealm != null) grealmLoaded = true;
            if (SacredTools != null) sacredToolsLoaded = true;
            if (Tremor != null) tremorLoaded = true;
            if (Redemption != null) redemptionLoaded = true;
            if (CheatSheet != null) cheatsheetLoaded = true;
            if (HEROsMod != null) herosLoaded = true;
            if (yabhb != null)
            {
                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF1"));

                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF2"));

                #region Healthbars


                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/MBarHead"),
                    GetTexture("Healthbars/MBarBody"),
                    GetTexture("Healthbars/MBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Firebrick,
                    Color.Firebrick,
                    Color.Firebrick);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("MushroomMonarch")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/FBarHead"),
                    GetTexture("Healthbars/FBarBody"),
                    GetTexture("Healthbars/FBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkCyan,
                    Color.DarkCyan,
                    Color.DarkCyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("FeudalFungus")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/RGCBarHead"),
                    GetTexture("Healthbars/RGCBarBody"),
                    GetTexture("Healthbars/RGCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("GripOfChaosRed")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/BGCBarHead"),
                    GetTexture("Healthbars/BGCBarBody"),
                    GetTexture("Healthbars/BGCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("GripOfChaosBlue")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/BMBarHead"),
                    GetTexture("Healthbars/BMBarBody"),
                    GetTexture("Healthbars/BMBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Broodmother")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/HydraBarHead"),
                    GetTexture("Healthbars/HydraBarBody"),
                    GetTexture("Healthbars/HydraBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Hydra")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SSBarHead"),
                    GetTexture("Healthbars/SSBarBody"),
                    GetTexture("Healthbars/SSBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("SerpentHead")));
                
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/DDBarHead"),
                    GetTexture("Healthbars/DDBarBody"),
                    GetTexture("Healthbars/DDBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.IndianRed,
                    Color.IndianRed,
                    Color.IndianRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Djinn")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Retriever")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Raider")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Orthrus")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/DBBarHead"),
                    GetTexture("Healthbars/DBBarBody"),
                    GetTexture("Healthbars/DBBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("DaybringerHead")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/NCBarHead"),
                    GetTexture("Healthbars/NCBarBody"),
                    GetTexture("Healthbars/NCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumBlue,
                    Color.MediumBlue,
                    Color.MediumBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("NightcrawlerHead")));
                
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/HarukaBarHead"),
                    GetTexture("Healthbars/HarukaBarBody"),
                    GetTexture("Healthbars/HarukaBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Haruka")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/YamataBarHead"),
                    GetTexture("Healthbars/YamataBarBody"),
                    GetTexture("Healthbars/YamataBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Purple,
                    Color.Purple,
                    Color.Purple);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("YamataHead")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/YamataABarHead"),
                    GetTexture("Healthbars/YamataABarBody"),
                    GetTexture("Healthbars/YamataABarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumVioletRed,
                    Color.MediumVioletRed,
                    Color.MediumVioletRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("YamataAHead")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/AkumaBarHead"),
                    GetTexture("Healthbars/AkumaBarBody"),
                    GetTexture("Healthbars/AkumaBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Yellow,
                    Color.Yellow,
                    Color.Yellow);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Akuma")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/AkumaABarHead"),
                    GetTexture("Healthbars/AkumaBarBody"),
                    GetTexture("Healthbars/AkumaABarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("AkumaA")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroBarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Zero")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroABarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroABarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("ZeroAwakened")));
                #endregion

            }
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Mushroom Monarch", 0f, (Func<bool>)(() => AAWorld.downedMonarch), "Use an [i:" + ItemType("IntimidatingMushroom") + "] during the day in the Surface Mushroom Biome");
                bossChecklist.Call("AddBossWithInfo", "Feudal Fungus", 0f, (Func<bool>)(() => AAWorld.downedFungus), "Use a [i:" + ItemType("ConfusingMushroom") + "] in a Glowing Mushroom Biome or at night");
                bossChecklist.Call("AddBossWithInfo", "Grips of Chaos", 2.00000000001f, (Func<bool>)(() => AAWorld.downedGrips), "Use a [i:" + ItemType("CuriousClaw") + "] or [i:" + ItemType("InterestingClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Broodmother", 4.00000000001f, (Func<bool>)(() => AAWorld.downedBrood), "Use a [i:" + ItemType("DragonBell") + "] in the Inferno during the day");
                bossChecklist.Call("AddBossWithInfo", "Hydra", 4.00000000001f, (Func<bool>)(() => AAWorld.downedHydra), "Use a [i:" + ItemType("HydraChow") + "] in the Mire at night");
                bossChecklist.Call("AddBossWithInfo", "Subzero Serpent", 5.5f, (Func<bool>)(() => AAWorld.downedSerpent), "Use a [i:" + ItemType("SubzeroCrystal") + "] in the Snow biome at night");
                bossChecklist.Call("AddBossWithInfo", "Desert Djinn", 5.5f, (Func<bool>)(() => AAWorld.downedDjinn), "Use a [i:" + ItemType("DjinnLamp") + "] in the Desert during the day");
                bossChecklist.Call("AddBossWithInfo", "Retriever", 9.5f, (Func<bool>)(() => AAWorld.downedRetriever), "Use a [i:" + ItemType("CyberneticClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Raider Ultima", 9.5f, (Func<bool>)(() => AAWorld.downedRaider), "Use a [i:" + ItemType("CyberneticBell") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Orthrus X", 9.5f, (Func<bool>)(() => AAWorld.downedOrthrus), "Use a [i:" + ItemType("ScrapHeap") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Nightcrawler & Daybringer", 15f, (Func<bool>)(() => AAWorld.downedEquinox), "Use a [i:" + ItemType("EquinoxWorm") + "]");
                bossChecklist.Call("AddBossWithInfo", "Sisters of Discord", 16.1f, (Func<bool>)(() => AAWorld.downedSisters), "Use the [i:" + ItemType("FlamesOfAnarchy") + "]");
                bossChecklist.Call("AddBossWithInfo", "Yamata", 16.2f, (Func<bool>)(() => AAWorld.downedYamata), "Use a [i:" + ItemType("DreadSigil") + "] in the Mire at night");
                bossChecklist.Call("AddBossWithInfo", "Akuma", 16.3f, (Func<bool>)(() => AAWorld.downedAkuma), "Use a [i:" + ItemType("DraconianSigil") + "] in the Inferno during the day");
                bossChecklist.Call("AddBossWithInfo", "Zero", 16.4f, (Func<bool>)(() => AAWorld.downedZero), "Use a [i:" + ItemType("ZeroTesseract") + "] in the Void");
                bossChecklist.Call("AddBossWithInfo", "Shen Doragon", 20f, (Func<bool>)(() => AAWorld.downedShen), "Use a [i:" + ItemType("ChaosSigil") + "]");


                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
            }
            /*if (DradonIsDum != null)
            {
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Doin' Shrooms", "Defeat the feudal fungus, the Mushroom Monarch", instance.GetTexture("BlankTex"), AAWorld.downedMonarch);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Get a Grip", "Defeat the claws of catastrophe, the Grips of Chaos", instance.GetTexture("Achievements/Grips"), AAWorld.downedGrips);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Magmatic Meltdown", "Defeat the magmatic matriarch, the Broodmother", instance.GetTexture("Achievements/Brood"), AAWorld.downedBrood);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Amphibious Atrocity", "Defeat the three-headed horror, the Hydra", instance.GetTexture("BlankTex"), AAWorld.downedHydra);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Slithering Snowmongerer", "Defeat the Snow-burrowing Snake, the Subzero Serpent", instance.GetTexture("BlankTex"), AAWorld.downedSerpent);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Sandskrit Sandman", "Defeat majin of magic, the Desert Djinn", instance.GetTexture("BlankTex"), AAWorld.downedDjinn);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Shocking", "Destroy any of the S.I.E.G.E. unit bosses", instance.GetTexture("Achievements/Storm"), AAWorld.downedStormAny);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Storming Smackdown", "Destroy all of the S.I.E.G.E. unit bosses", instance.GetTexture("Achievements/Storm"), AAWorld.downedStormAll);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Equinox Eradicator", "Defeat the time-turning worms, the Equinox Duo", instance.GetTexture("Achievements/Equinox"), AAWorld.downedEquinox);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Grip it and rip it", "Rematch the Grips of Chaos in their enhanced, discordian form", instance.GetTexture("Achievements/Grips"), AAWorld.downedGripsS);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Hurricane Horror", "Defeat the Spatial Squid of the Ocean, the Kraken", instance.GetTexture("BlankTex"), AAWorld.downedKraken);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Clockwork Catastrophe", "Defeat the destructive doomsday construct, Zero", instance.GetTexture("Achievements/Zero"), AAWorld.downedZero);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Doom Slayer", "Destroy Zero's true, dark form, Zero Protocol", instance.GetTexture("Achievements/ZeroA"), (AAWorld.downedZero && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Trial By Fire", "Defeat the draconian demon of the Inferno, Akuma", instance.GetTexture("Achievements/Akuma"), AAWorld.downedAkuma);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Serpent Slayer", "Slay Akuma's true, blazing form, Akuma Awakened", instance.GetTexture("Achievements/Akuma"), (AAWorld.downedAkuma && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Crescent of Madness", "Defeat the dread nightmare of the Mire, Yamata", instance.GetTexture("BlankTex"), AAWorld.downedYamata);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Hydra Slayer", "Slay Yamata's true, abyssal form, Yamata Awakened", instance.GetTexture("BlankTex"), (AAWorld.downedYamata && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Unyielding Discord", "Defeat the discordian doomsayer of chaos, Shen Doragon", instance.GetTexture("BlankTex"), AAWorld.downedShen);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Dragon Slayer", "Slay Shen Doragon's true, chaotic form, Shen Doragon Awakened", instance.GetTexture("BlankTex"), (AAWorld.downedShen && Main.expertMode));
            }*/
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }
        
        public static int GobSouls = -1;

        public override void Load()
        {
            instance = this;
            GobSouls = CustomCurrencyManager.RegisterCurrency(new Items.Currency.GSouls(ItemType<Items.Currency.GoblinSoul>()));
            if (Main.rand == null)
                Main.rand = new Terraria.Utilities.UnifiedRandom();


            GameShaders.Armor.BindShader(ItemType("BlazingDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(ItemType("AbyssalDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("DoomsdayDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(ItemType("DiscordianDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(ItemType("AbyssalWrathDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades").UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(ItemType("BlazingFuryDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

            InfinityHotKey = RegisterHotKey("Snap", "G");

            AbilityKey = RegisterHotKey("Armor Ability", "Y");

            if (!Main.dedServ)
            {
                AddEquipTexture(new Pony_Head(), null, EquipType.Head, "Pony_Head", "AAMod/Items/Vanity/Beg/Pony_Head");
                AddEquipTexture(new Pony_Body(), null, EquipType.Body, "Pony_Body", "AAMod/Items/Vanity/Beg/Pony_Body", "AAMod/Items/Vanity/Beg/Pony_Arms");
                AddEquipTexture(new Pony_Legs(), null, EquipType.Legs, "Pony_Legs", "AAMod/Items/Vanity/Beg/Pony_Legs");

                PremultiplyTexture(GetTexture("Backgrounds/VoidBH"));
                PremultiplyTexture(GetTexture("Backgrounds/Moon"));
                PremultiplyTexture(GetTexture("Backgrounds/Sun"));
                PremultiplyTexture(GetTexture("Backgrounds/fog"));
                PremultiplyTexture(GetTexture("Backgrounds/AkumaSun"));
                PremultiplyTexture(GetTexture("Backgrounds/YamataMoon"));
                PremultiplyTexture(GetTexture("Backgrounds/YamataBeam"));
                PremultiplyTexture(GetTexture("Backgrounds/AkumaAMeteor"));
                PremultiplyTexture(GetTexture("Backgrounds/AkumaMeteor"));
                PremultiplyTexture(GetTexture("Backgrounds/ShenSun"));
                PremultiplyTexture(GetTexture("Backgrounds/ShenMoon"));
                PremultiplyTexture(GetTexture("Backgrounds/ShenEclipse"));
                PremultiplyTexture(GetTexture("NPCs/Bosses/Zero/ZeroShield"));

                if (GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch") != 0) //ensure music was loaded!
                {
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch"), ItemType("MonarchBox"), TileType("MonarchBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Fungus"), ItemType("FungusBox"), TileType("FungusBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme"), ItemType("GripsBox"), TileType("GripsBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HydraTheme"), ItemType("HydraBox"), TileType("HydraBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BroodTheme"), ItemType("BroodBox"), TileType("BroodBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom"), ItemType("MushBox"), TileType("MushBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface"), ItemType("InfernoBox"), TileType("InfernoBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface"), ItemType("MireBox"), TileType("MireBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground"), ItemType("InfernoUBox"), TileType("InfernoUBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground"), ItemType("MireUBox"), TileType("MireUBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn"), ItemType("DjinnBox"), TileType("DjinnBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6"), ItemType("SerpentBox"), TileType("SerpentBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Siege"), ItemType("SiegeBox"), TileType("SiegeBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox"), ItemType("Equibox"), TileType("Equibox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Void"), ItemType("VoidBox"), TileType("VoidBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/VoidButNowItsSpooky"), ItemType("FateBox"), TileType("FateBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines"), ItemType("LakeBox"), TileType("LakeBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine"), ItemType("PagodaBox"), TileType("PagodaBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero"), ItemType("ZeroBox"), TileType("ZeroBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2"), ItemType("Zero2Box"), TileType("Zero2Box"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma"), ItemType("AkumaBox"), TileType("AkumaBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2"), ItemType("AkumaABox"), TileType("AkumaABox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata"), ItemType("YamataBox"), TileType("YamataBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2"), ItemType("YamataABox"), TileType("YamataABox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RayOfHope"), ItemType("RoHBox"), TileType("RoHBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium"), ItemType("TerrariumBox"), TileType("TerrariumBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shen"), ItemType("ShenBox"), TileType("ShenBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA"), ItemType("ShenABox"), TileType("ShenABox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand"), ItemType("SABox"), TileType("SABox"));
                }

                Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:MireSky"] = new MireSky();
                MireSky.PlanetTexture = GetTexture("Backgrounds/Moon");
                MireSky.SkyTexture = GetTexture("Backgrounds/MireSky");

                Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:VoidSky"] = new VoidSky();
                VoidSky.PlanetTexture = GetTexture("Backgrounds/VoidBH");
                VoidSky.Asteroids1 = GetTexture("Backgrounds/Asteroids1");
                VoidSky.Asteroids2 = GetTexture("Backgrounds/Asteroids2");
                VoidSky.Asteroids3 = GetTexture("Backgrounds/Asteroids3");
                VoidSky.Echo = GetTexture("Backgrounds/Echo");
                VoidSky.LB = GetTexture("Backgrounds/LB");
                VoidSky.boltTexture = GetTexture("Backgrounds/VoidBolt");
                VoidSky.flashTexture = GetTexture("Backgrounds/VoidFlash");
                VoidSky.Stars = GetTexture("Backgrounds/StarTex");
                VoidSky.SkyTexture = GetTexture("Backgrounds/Sky");

                Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:InfernoSky"] = new InfernoSky();
                InfernoSky.PlanetTexture = GetTexture("Backgrounds/Sun");
                InfernoSky.SkyTex = GetTexture("Backgrounds/Sky");
                AkumaSky.MeteorTexture = GetTexture("Backgrounds/AkumaMeteor");

                Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:AkumaSky"] = new AkumaSky();
                AkumaSky.PlanetTexture = GetTexture("Backgrounds/AkumaSun");
                AkumaSky.SkyTex = GetTexture("Backgrounds/Sky");
                AkumaSky.MeteorTexture = GetTexture("Backgrounds/AkumaAMeteor");

                Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();
                YamataSky.PlanetTexture = GetTexture("Backgrounds/YamataMoon");
                YamataSky.SkyTex = GetTexture("Backgrounds/StarTex");
                YamataSky.BeamTexture = GetTexture("Backgrounds/YamataBeam");

                Filters.Scene["AAMod:ShenSky"] = new Filter(new ShenSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:ShenSky"] = new ShenSky();
                ShenSky.Sun = GetTexture("Backgrounds/ShenSun");
                ShenSky.Moon = GetTexture("Backgrounds/ShenMoon");
                ShenSky.SkyTex = GetTexture("Backgrounds/Sky");

                Filters.Scene["AAMod:ShenASky"] = new Filter(new ShenASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:ShenASky"] = new ShenASky();
                ShenASky.PlanetTexture = GetTexture("Backgrounds/ShenEclipse");
                ShenASky.SkyTex = GetTexture("Backgrounds/StarTex");

                Main.itemTexture[3460] = GetTexture("Resprites/Luminite");
                Main.itemTexture[512] = GetTexture("Resprites/SoulOfNight");
            }
        }
        
        public override void Unload()
        {
            CleanupStaticArrays();
            instance = null;
            InfinityHotKey = null;
            AbilityKey = null;
        }

        public void CleanupStaticArrays()
        {
            if (Main.netMode != 2) //handle clearing all static texture arrays
            {
                precachedTextures.Clear();
                NPCs.Bosses.Yamata.Awakened.YamataSoul.glowTex = null;
                NPCs.Bosses.Yamata.Awakened.YamataSoul.glowTex2 = null;
                NPCs.Bosses.Akuma.Awakened.AkumaA.glowTex = null;
                NPCs.Bosses.Akuma.Awakened.AkumaA.glowTex2 = null;
                NPCs.Bosses.Akuma.Awakened.AkumaA.glowTex3 = null;
                NPCs.Bosses.Akuma.Awakened.AkumaA.glowTex4 = null;
                NPCs.Bosses.Akuma.Awakened.AkumaA.glowTex5 = null;
                AkumaSky.PlanetTexture = null;
                AkumaSky.BGTexture = null;
                AkumaSky.SkyTex = null;
                InfernoSky.PlanetTexture = null;
                InfernoSky.BGTexture = null;
                InfernoSky.SkyTex = null;
                MireSky.PlanetTexture = null;
                MireSky.SkyTexture = null;
                MireSky.BGTexture = null;
                VoidSky.PlanetTexture = null;
                VoidSky.BGTexture = null;
                VoidSky.Echo = null;
                VoidSky.Asteroids1 = null;
                VoidSky.Asteroids2 = null;
                VoidSky.Asteroids3 = null;
                VoidSky.LB = null;
                VoidSky.boltTexture = null;
                VoidSky.flashTexture = null;
                YamataSky.PlanetTexture = null;
                YamataSky.BGTexture = null;
                YamataSky.SkyTex = null;
                ShenSky.MeteorTexture = null;
                ShenSky.SkyTex = null;
                ShenSky.Sun = null;
                ShenSky.Moon = null;
                ShenSky.BGTexture = null;
                ShenASky.MeteorTexture = null;
                ShenASky.PlanetTexture = null;
                ShenASky.SkyTex = null;
                Items.Accessories.SoulStone._glow = null;
                Items.Pets.Mudkip.glowTex = null;
                Items.Pets.MudkipS.glowTex = null;
                NPCs.Bosses.Grips.GripOfChaosRed.glowTex = null;
                NPCs.Bosses.GripsShen.AbyssGrip.glowTex = null;
                NPCs.Bosses.GripsShen.BlazeGrip.glowTex = null;
                NPCs.Bosses.Raider.Raider.glowTex = null;
                NPCs.Bosses.Raider.Raider.glowTex1 = null;
                NPCs.Bosses.Raider.RaidEgg.glowTex = null;
                NPCs.Bosses.Raider.Raidmini.glowTex = null;
                NPCs.Bosses.Raider.Raidmini.glowTex1 = null;
                NPCs.Bosses.Retriever.Retriever.glowTex = null;
                NPCs.Bosses.Retriever.Retriever.glowTex = null;
                NPCs.Bosses.Zero.Zero.glowTex = null;
                NPCs.Bosses.Zero.VoidStar.glowTex = null;
                NPCs.Bosses.Zero.RealityCannon.glowTex = null;
                NPCs.Bosses.Zero.RiftShredder.glowTex = null;
                NPCs.Bosses.Zero.Taser.glowTex = null;
                NPCs.Bosses.Zero.TeslaHand.glowTex = null;
                NPCs.Bosses.Zero.SearcherZero.glowTex = null;
                NPCs.Bosses.Zero.Protocol.ZeroAwakened.glowTex = null;
                NPCs.Enemies.Void.Searcher.glowTex = null;
            }
        }

        public override void AddRecipeGroups()
        {
            // Registers the new recipe group with the specified name
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " darkmatter helmet", new int[]
            {
                ItemType<DarkmatterVisor>(),
                ItemType<DarkmatterHelm>(),
                ItemType<DarkmatterHelmet>(),
                ItemType<DarkmatterHeaddress>(),
                ItemType<DarkmatterMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " radium helmet", new int[]
            {
                ItemType<RadiumHat>(),
                ItemType<RadiumHelm>(),
                ItemType<RadiumHelmet>(),
                ItemType<RadiumHeadgear>(),
                ItemType<RadiumMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " gold bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:Gold", group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " astral crafting station", new int[]
            {
                ItemType("RadiantArcanum"),
                ItemType("QuantumFusionAccelerator"),
            });
            RecipeGroup.RegisterGroup("AAMod:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ancient material", new int[]
            {
                ItemType("UnstableSingularity"),
                ItemType("CrucibleScale"),
                ItemType("DreadScale")
            });
            RecipeGroup.RegisterGroup("AAMod:AncientMaterials", group4);

            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " superancient material", new int[]
            {
                ItemType("ChaosSoul")
            });
            RecipeGroup.RegisterGroup("AAMod:SuperAncientMaterials", group5);

            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " world evil material", new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);

            group6 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " hardmode forge", validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge,
                ItemType("TechneciumForge")
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group6);

            RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " chaos claw", new int[]
            {
                ItemType<DragonClaw>(),
                ItemType<HydraClaw>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosClaw", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " iron bar", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAMod:Iron", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " copper bar", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAMod:Copper", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " silver bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAMod:Silver", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " evil bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAMod:EvilBar", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " chaos bar", new int[]
            {
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " evil/chaos bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:EvilorChaosBar", group13);

            RecipeGroup group14 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " ancient crafting station", new int[]
            {
                ItemType<BinaryReassembler>(),
                ItemType<ChaosCrucible>()
            });
            RecipeGroup.RegisterGroup("AAMod:ACS", group14);

            RecipeGroup group15 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " evil summon staff", new int[]
            {
                ItemType<Items.Summoning.EaterStaff>(),
                ItemType<Items.Summoning.CrimsonStaff>()
            });
            RecipeGroup.RegisterGroup("AAMod:EvilStaff", group15);

            RecipeGroup group16 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " altar", new int[]
            {
                ItemType<MireAltar>(),
                ItemType<CrimsonAltar>(),
                ItemType<CorruptAltar>(),
                ItemType<InfernoAltar>()
            });
            RecipeGroup.RegisterGroup("AAMod:Altar", group16);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<OroborosWood>());
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.gameMenu)
                return;
            if (priority > MusicPriority.Environment)
                return;
            Player player = Main.LocalPlayer;
            if (!player.active)
                return;
            AAPlayer Ancients = player.GetModPlayer<AAPlayer>();
            bool zoneIZ = Ancients.ZoneVoid && !AAWorld.downedIZ;
            bool zoneShen = (Ancients.ZoneRisingSunPagoda || Ancients.ZoneRisingMoonLake) && !AAWorld.downedShen;
            bool zoneSoC = player.ZoneBeach && !AAWorld.downedSoC;
            if (zoneShen && AAWorld.downedAllAncients)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingDragon");
                return;
            }
            if (zoneIZ && AAWorld.downedZero)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingGiant");
                return;
            }
            if (AkumaMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (YamataMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (AHIntro)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");

                priority = (MusicPriority)10;
                return;
            }
            if (Ancients.ZoneVoid)
            {
                priority = MusicPriority.Event;
                if (NPC.downedMoonlord && !AAWorld.downedZero)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoidButNowItsSpooky");
                    return;
                }
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Void");
                return;
            }
            if (Ancients.ZoneShip)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Ship");
                return;
            }
            if (Ancients.ZoneInferno)
            {
                if (Ancients.ZoneRisingSunPagoda && NPC.downedMoonlord && !AAWorld.downedAkuma)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine");
                    return;
                }
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground");
                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface");
                    return;
                }
            }
            if (Ancients.ZoneMire)
            {
                if (Ancients.ZoneRisingMoonLake && NPC.downedMoonlord && !AAWorld.downedYamata)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines");
                    return;
                }
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground");

                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface");

                    return;
                }
            }
            if (Ancients.ZoneStars)
            {

            }
            if (Ancients.Terrarium)
            {

                priority = MusicPriority.BiomeHigh;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium");

                return;
            }
            if (Ancients.ZoneMush)
            {

                priority = MusicPriority.BiomeMedium;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shroom");
                
                return;
            }
            
        }

        public override void AddRecipes()
        {
            RecipeFinder finder = new RecipeFinder();
            {
                finder = new RecipeFinder();
                finder.AddIngredient(ItemID.BloodButcherer, 1);
                finder.AddIngredient(ItemID.FieryGreatsword, 1);
                finder.AddIngredient(ItemID.BladeofGrass, 1);
                finder.AddIngredient(ItemID.Muramasa, 1);
                finder.AddTile(TileID.DemonAltar);
                finder.SetResult(ItemID.NightsEdge, 1);
                Recipe recipe2 = finder.FindExactRecipe();
                if (recipe2 != null)
                {
                    RecipeEditor editor = new RecipeEditor(recipe2);
                    editor.DeleteRecipe();
                }
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "HallowedOre", 4);
                recipe.AddTile(null, "HallowedForge");
                recipe.SetResult(ItemID.HallowedBar, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddIngredient(ItemID.Hay, 5);
                recipe.AddTile(null, "HellstoneAnvil");
                recipe.SetResult(ItemID.GuideVoodooDoll, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.IronBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.LeadBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Lens, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxNight, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BottledWater, 5);
                recipe.AddIngredient(ItemID.UmbrellaHat, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxRain, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SnowBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSnow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxIce, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SandBlock, 40);
                recipe.AddIngredient(ItemID.Cactus, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDesert, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
                recipe.AddIngredient(ItemID.SharkFin, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSandstorm, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Coral, 3);
                recipe.AddIngredient(ItemID.Starfish, 3);
                recipe.AddIngredient(ItemID.Seashell, 3);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOcean, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Feather, 20);
                recipe.AddIngredient(ItemID.SunplateBlock, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSpace, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GlowingMushroom, 20);
                recipe.AddIngredient(ItemID.Mushroom, 10);
                recipe.AddIngredient(ItemID.MushroomGrassSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMushrooms, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MudBlock, 20);
                recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
                recipe.AddIngredient(ItemID.RichMahogany, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxJungle, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddIngredient(ItemID.CorruptSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.EbonstoneBlock, 30);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddIngredient(ItemID.CrimsonSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrimstoneBlock, 30);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrystalShard, 10);
                recipe.AddIngredient(ItemID.HallowedSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTheHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PearlstoneBlock, 30);
                recipe.AddIngredient(ItemID.UnicornHorn, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AshBlock, 20);
                recipe.AddIngredient(ItemID.Hellstone, 15);
                recipe.AddIngredient(ItemID.ObsidianBrick, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxHell, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BlueBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GreenBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PinkBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TempleKey, 1);
                recipe.AddIngredient(ItemID.LihzahrdBrick, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTemple, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 15);
                recipe.AddIngredient(ItemID.DemoniteBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofFright, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
                recipe.AddIngredient(null, "DevilSilk", 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofSight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TissueSample, 15);
                recipe.AddIngredient(ItemID.CrimtaneBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofMight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss3, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeetleHusk, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss4, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeeWax, 20);
                recipe.AddIngredient(ItemID.BottledHoney, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss5, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddIngredient(null, "PlanteraPetal", 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPlantera, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Meteorite, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Shackle, 1);
                recipe.AddIngredient(ItemID.MoneyTrough, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEclipse, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GoblinBattleStandard, 1);
                recipe.AddIngredient(ItemID.SpikyBall, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxGoblins, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PirateMap, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPirates, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MartianConduitPlating, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMartians, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PumpkinMoonMedallion, 30);
                recipe.AddIngredient(ItemID.SpookyWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPumpkinMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.NaughtyPresent, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxFrostMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 3);
                recipe.AddIngredient(ItemID.FragmentSolar, 3);
                recipe.AddIngredient(ItemID.FragmentVortex, 3);
                recipe.AddIngredient(ItemID.FragmentStardust, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTowers, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarOre, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxLunarBoss, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DefenderMedal, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDD2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Glass, 10);
                recipe.AddIngredient(ItemID.SnowBlock, 10);
                recipe.AddRecipeGroup("Wood");
                recipe.AddTile(TileID.GlassKiln);
                recipe.SetResult(ItemID.SnowGlobe, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SnowGlobe, 1);
                recipe.AddIngredient(ItemID.SoulofFlight, 5);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.SoulofLight, 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.GravityGlobe, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "MushiumBar", 1);
                recipe.AddIngredient(ItemID.GlowingMushroom, 5);
                recipe.AddTile(TileID.Autohammer);
                recipe.SetResult(ItemID.ShroomiteBar, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Deathweed, 1);
                recipe.AddIngredient(null, "DragonClaw", 3);
                recipe.AddIngredient(null, "DragonScale", 1);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.RagePotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Deathweed, 1);
                recipe.AddIngredient(null, "HydraClaw", 3);
                recipe.AddIngredient(null, "MirePod", 1);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.WrathPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Waterleaf, 1);
                recipe.AddIngredient(null, "MirePod", 2);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.WaterWalkingPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Waterleaf, 1);
                recipe.AddIngredient(null, "DragonScale", 2);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.ObsidianSkinPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Glass, 10);
                recipe.AddIngredient(ItemID.RecallPotion, 10);
                recipe.AddTile(TileID.GlassKiln);
                recipe.SetResult(ItemID.MagicMirror);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IceBrick, 10);
                recipe.AddIngredient(ItemID.RecallPotion, 10);
                recipe.AddTile(TileID.IceMachine);
                recipe.SetResult(ItemID.IceMirror);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddIngredient(ItemID.Diamond, 1);
                recipe.AddIngredient(ItemID.Sapphire, 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.IceBlade);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "MushroomBlock");
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(ItemID.Mushroom, 3);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PlatinumBroadsword);
                recipe.AddIngredient(ItemID.FallenStar, 10);
                recipe.AddIngredient(ItemID.ManaCrystal);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.Starfury);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.GoldBroadsword);
                recipe.AddIngredient(ItemID.FallenStar, 10);
                recipe.AddIngredient(ItemID.ManaCrystal);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.Starfury);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PlatinumBroadsword);
                recipe.AddIngredient(ItemID.ManaCrystal, 3);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.EnchantedSword);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.GoldBroadsword);
                recipe.AddIngredient(ItemID.ManaCrystal, 3);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.EnchantedSword);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.EnchantedSword);
                recipe.AddIngredient(ItemID.Muramasa);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.Arkhalis);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CobaltBar, 5);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.CobaltShield);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "PalladiumShield");
                recipe.AddIngredient(ItemID.ObsidianSkull);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.SetResult(ItemID.ObsidianShield);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Ebonwood, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.Shadewood);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Shadewood, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.Ebonwood);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.EbonstoneBlock, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CrimstoneBlock);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.CrimstoneBlock, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.EbonstoneBlock);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CopperBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TinBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TinBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CopperBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.LeadBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.IronBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IronBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.LeadBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SilverBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TungstenBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TungstenBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.SilverBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.GoldBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PlatinumBar);
                recipe.AddRecipe();

            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PlatinumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.GoldBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.DemoniteBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CrimtaneBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CrimtaneBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.DemoniteBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "AbyssiumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "IncineriteBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "IncineriteBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "AbyssiumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.ShadowScale, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TissueSample);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TissueSample, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.DemoniteBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "BroodScale", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "HydraHide");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "HydraHide", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "BroodScale");
                recipe.AddRecipe();
            }
            #region Cobalt

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CobaltBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PalladiumBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PalladiumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CobaltBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CobaltBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PalladiumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PalladiumBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CobaltBar);
                recipe.AddRecipe();
            }

            #endregion

            #region Mythril


            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MythrilBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.OrichalcumBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.OrichalcumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.MythrilBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MythrilBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "UraniumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.OrichalcumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "UraniumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "UraniumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.MythrilBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "UraniumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.OrichalcumBar);
                recipe.AddRecipe();
            }
            #endregion

            #region Adamantite
            
            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.AdamantiteBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TitaniumBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TitaniumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.AdamantiteBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.AdamantiteBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TitaniumBar, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumBar");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.AdamantiteBar);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumBar", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TitaniumBar);
                recipe.AddRecipe();
            }

                #endregion

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Ichor, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CursedFlame);
                recipe.AddRecipe();

            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CursedFlame, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.Ichor);
                recipe.AddRecipe();

            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "DragonFire", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "HydraToxin");
                recipe.AddRecipe();

            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "HydraToxin", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "DragonFire");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CrimsonKey, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CorruptionKey);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CorruptionKey, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CrimsonKey);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SoulofNight, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.SoulofLight);
                recipe.AddRecipe();

                recipe.AddIngredient(ItemID.SoulofLight, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.SoulofNight);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "SoulOfSmite", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "SoulOfSpite");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "SoulOfSpite", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "SoulOfSmite");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "InfernoKey", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "MireKey");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "MireKey", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "InfernoKey");
                recipe.AddRecipe();

            }
            //Ores

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CopperOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TinOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TinOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CopperOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.LeadOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.IronOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IronOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.LeadOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SilverOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TungstenOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TungstenOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.SilverOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.GoldOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PlatinumOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PlatinumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.GoldOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.DemoniteOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CrimtaneOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CrimtaneOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.DemoniteOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "Abyssium", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "Incinerite");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "Incinerite", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "Abyssium");
                recipe.AddRecipe();
            }

            #region Cobalt
            

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CobaltOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PalladiumOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PalladiumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CobaltOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.CobaltOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumOre");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.PalladiumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "YtriumOre");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumOre", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.PalladiumOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "YtriumOre", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.CobaltOre);
                recipe.AddRecipe();
            }
            #endregion
            
            #region Mythril
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MythrilOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.OrichalcumOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.OrichalcumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.MythrilOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.MythrilOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "Uranium");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(ItemID.OrichalcumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "Uranium");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(null, "Uranium", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.MythrilOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);

                recipe.AddIngredient(null, "Uranium", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.OrichalcumOre);
                recipe.AddRecipe();
            }

            #endregion

            #region Adamantite
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.AdamantiteOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TitaniumOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TitaniumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.AdamantiteOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.AdamantiteOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "TechneciumOre");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.TitaniumOre, 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(null, "TechneciumOre");
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TechneciumOre", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.AdamantiteOre);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TechneciumOre", 2);
                recipe.AddTile(this, "Transmuter");
                recipe.SetResult(ItemID.TitaniumOre);
                recipe.AddRecipe();
            }
                #endregion

        }   

        //Stuff 4 Grox
        public override object Call(params object[] args)
        {
            if (args.Length <= 0 || !(args[0] is string)) return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");
            string methodName = (string)args[0];
            if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on player and name.
            {
                Func<string, bool> downed = (name) =>
                {
                    name = name.ToLower();
                    switch (name)
                    {
                        default: return false;
                        case "mushroommonarch": return AAWorld.downedMonarch;
                        case "broodmother": return AAWorld.downedBrood;
                        case "hydra": return AAWorld.downedHydra;
                        case "grips":
                        case "gripsofchaos": return AAWorld.downedGrips;
                        case "tode": return AAWorld.downedToad;
                        case "retriever": return AAWorld.downedRetriever;
                        case "orthrus": return AAWorld.downedOrthrus;
                        case "raider": return AAWorld.downedRaider;
                        case "stormany": return AAWorld.downedStormAny;
                        case "stormall": return AAWorld.downedStormAll;
                        case "daybringer": return AAWorld.downedDB;
                        case "nightcrawler": return AAWorld.downedNC;
                        case "equinox": return AAWorld.downedEquinox;
                        case "ancient":
                        case "ancientany": return AAWorld.downedAncient;
                        case "sancient":
                        case "sancientany": return AAWorld.downedSAncient;
                        case "gripsS":
                        case "discordgrips": return AAWorld.downedGripsS;
                        case "kraken": return AAWorld.downedKraken;
                        case "akuma": return AAWorld.downedAkuma;
                        case "yamata": return AAWorld.downedYamata;
                        case "zero": return AAWorld.downedZero;
                        case "shen":
                        case "shendoragon": return AAWorld.downedShen;
                        case "iz":
                        case "infinityzero": return AAWorld.downedIZ;
                        case "soc":
                        case "soulofcthulhu": return AAWorld.downedSoC;
                    }
                };
                return downed;
            }
            else
            if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
            {
                Func<Player, string, bool> inZone = (p, name) =>
                {
                    name = name.ToLower();
                    AAPlayer aap = p.GetModPlayer<AAPlayer>();
                    switch (name)
                    {
                        default: return false;
                        case "mire": return aap.ZoneMire;
                        case "lake": return aap.ZoneRisingMoonLake;
                        case "inferno": return aap.ZoneInferno;
                        case "pagoda": return aap.ZoneRisingSunPagoda;
                        case "ship": return aap.ZoneShip;
                        case "storm": return aap.ZoneStorm;
                        case "void": return aap.ZoneVoid;
                        case "mush": return aap.ZoneMush;
                        case "terrarium": return aap.Terrarium;
                    }
                };
                return inZone;
            }
            return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD FOUND: " + methodName);
        }
    }

    public class RuneRecipe : ModRecipe
    {
        public bool IsExpert;

        public RuneRecipe(Mod mod) : base(mod)
        {
            IsExpert = Main.expertMode;
        }

        public override bool RecipeAvailable()
        {

            if (!IsExpert)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
