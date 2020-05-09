﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Mask;
using AAMod.Items.BossSummons;
using AAMod.Items.Blocks.Boxes;
using AAMod.Items.Blocks;
using AAMod.Items.Flasks;
using AAMod.Items.Usable;
using AAMod.Items.Materials;
using Terraria;

namespace AAMod.Globals
{
    internal class WeakReferences
    {
        public static void PerformModSupport()
        {
            PerformHealthBarSupport();
            PerformBossChecklistSupport();
            PerformCencusSupport();
            PerformFargosSetup();
        }

        private static void PerformHealthBarSupport()
        {
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");

            if (yabhb != null)
            {
                // Mushroom Monarch
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/MBarHead"),
                    AAMod.instance.GetTexture("Healthbars/MBarBody"),
                    AAMod.instance.GetTexture("Healthbars/MBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Firebrick,
                    Color.Firebrick,
                    Color.Firebrick);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("MushroomMonarch"));

                // Feudal Fungus
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/FBarHead"),
                    AAMod.instance.GetTexture("Healthbars/FBarBody"),
                    AAMod.instance.GetTexture("Healthbars/FBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkCyan,
                    Color.DarkCyan,
                    Color.DarkCyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("FeudalFungus"));

                // Grip of Chaos (Red)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/RGCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/RGCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/RGCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("GripOfChaosRed"));

                // Grip of Chaos (Blue)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/BGCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/BGCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/BGCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("GripOfChaosBlue"));

                // The Broodmother
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/BMBarHead"),
                    AAMod.instance.GetTexture("Healthbars/BMBarBody"),
                    AAMod.instance.GetTexture("Healthbars/BMBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Broodmother"));

                // Hydra
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/HydraBarHead"),
                    AAMod.instance.GetTexture("Healthbars/HydraBarBody"),
                    AAMod.instance.GetTexture("Healthbars/HydraBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Hydra"));

                // Subzero Serpent
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SSBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SSBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SSBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("SerpentHead"));

                // Desert Djinn
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/DDBarHead"),
                    AAMod.instance.GetTexture("Healthbars/DDBarBody"),
                    AAMod.instance.GetTexture("Healthbars/DDBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.IndianRed,
                    Color.IndianRed,
                    Color.IndianRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Djinn"));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SagBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SagBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SagBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Sag"));

                //Anubis
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AnuBarHead"),
                    AAMod.instance.GetTexture("Healthbars/AnuBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AnuBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Anubis"));

                // Greed
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/GreedBarHead"),
                    AAMod.instance.GetTexture("Healthbars/GreedBarBody"),
                    AAMod.instance.GetTexture("Healthbars/GreedBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Goldenrod,
                    Color.Goldenrod,
                    Color.Goldenrod);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Greed"));

                // Rajah
                    yabhb.Call("hbStart");
                    yabhb.Call("hbSetTexture",
                        AAMod.instance.GetTexture("Healthbars/RajahBarHead"),
                        AAMod.instance.GetTexture("Healthbars/RajahBarBody"),
                        AAMod.instance.GetTexture("Healthbars/RajahBarTail"),
                        AAMod.instance.GetTexture("Healthbars/BarFill"));
                    yabhb.Call("hbSetColours",
                        Color.Orange,
                        Color.Orange,
                        Color.Orange);
                    yabhb.Call("hbSetMidBarOffset", -30, 10);
                    yabhb.Call("hbSetBossHeadCentre", 50, 32);
                    yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                    yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Rajah"));
                
                //Forsaken Anubis
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/FAnuBarHead"),
                    AAMod.instance.GetTexture("Healthbars/FAnuBarBody"),
                    AAMod.instance.GetTexture("Healthbars/FAnuBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumAquamarine,
                    Color.MediumAquamarine,
                    Color.MediumAquamarine);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("ForsakenAnubis"));

                // Worm King Greed
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/WKGBarHead"),
                    AAMod.instance.GetTexture("Healthbars/WKGBarBody"),
                    AAMod.instance.GetTexture("Healthbars/WKGBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Goldenrod,
                    Color.Goldenrod,
                    Color.Goldenrod);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Greed"));

                // Daybringer
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/DBBarHead"),
                    AAMod.instance.GetTexture("Healthbars/DBBarBody"),
                    AAMod.instance.GetTexture("Healthbars/DBBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("DaybringerHead"));

                // Nightcrawler
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/NCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/NCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/NCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumBlue,
                    Color.MediumBlue,
                    Color.MediumBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("NightcrawlerHead"));

                // Haruka Yamata
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/HarukaBarHead"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Haruka"));

                // Haruka Yamata (Awakened)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("HarukaY"));

                // Wrath Haruka
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("WrathHaruka"));

                // Ashe Akuma
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("AsheA"));

                // Fury Ashe
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("FuryAshe"));

                // Yamata
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/YamataBarHead"),
                    AAMod.instance.GetTexture("Healthbars/YamataBarBody"),
                    AAMod.instance.GetTexture("Healthbars/YamataBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Purple,
                    Color.Purple,
                    Color.Purple);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Yamata"));

                // Yamata Awakened
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/YamataABarHead"),
                    AAMod.instance.GetTexture("Healthbars/YamataABarBody"),
                    AAMod.instance.GetTexture("Healthbars/YamataABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumVioletRed,
                    Color.MediumVioletRed,
                    Color.MediumVioletRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("YamataA"));

                // Akuma; Draconian Demon
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AkumaBarHead"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Yellow,
                    Color.Yellow,
                    Color.Yellow);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Akuma"));

                // Akuma Awakened; Blazing Fury Incarnate
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AkumaABarHead"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AkumaABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("AkumaA"));

                // Zero
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ZeroBarHead"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Zero"));

                // ZER0 PR0T0C0L
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ZeroABarHead"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ZeroABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("ZeroProtocol"));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SRajahBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SRajahBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SRajahBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Gold,
                    Color.Gold,
                    Color.Gold);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("SupremeRajah"));

                // Shen
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ShenBarHead"),
                    AAMod.instance.GetTexture("Healthbars/ShenBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ShenBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Purple,
                    Color.Purple,
                    Color.Purple);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("Shen"));

                //Shen Awakened
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ShenABarHead"),
                    AAMod.instance.GetTexture("Healthbars/ShenABarBody"),
                    AAMod.instance.GetTexture("Healthbars/ShenABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Silver,
                    Color.Silver,
                    Color.Silver);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.NPCType("ShenA"));
            }
        }

        private static void PerformBossChecklistSupport()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");

            AAMod mod = AAMod.instance;

            if (bossChecklist != null)
            {
                #region Mushroom Monarch
                bossChecklist.Call("AddBoss", 0f, mod.NPCType("MushroomMonarch"), mod,
                    Lang.BossCheck("MushroomMonarch"),
                    (Func<bool>)(() => AAWorld.downedMonarch),
                    ModContent.ItemType<IntimidatingMushroom>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.MonarchTrophy>(),
                        ModContent.ItemType<MonarchMask>(),
                        ModContent.ItemType<MonarchBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.MonarchBag>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.HeartyTruffle>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.Mushium>(),
                        ModContent.ItemType<SporeSac>()
                    },
                    Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<IntimidatingMushroom>() + "]",
                    Lang.BossCheck("MushroomMonarchInfo2"),
                    "AAMod/CrossMod/BossChecklist/Monarch",
                    "AAMod/NPCs/Bosses/MushroomMonarch/MushroomMonarch_Head_Boss");
                #endregion

                #region Feudal Fungus
                bossChecklist.Call("AddBoss", 0.1f, mod.NPCType("FeudalFungus"), mod,
                    Lang.BossCheck("FeudalFungus"),
                    (Func<bool>)(() => AAWorld.downedFungus),
                    ModContent.ItemType<ConfusingMushroom>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.FungusTrophy>(),
                        ModContent.ItemType<FungusMask>(),
                        ModContent.ItemType<FungusBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.FungusBag>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.MagicTruffle>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>(),
                        ModContent.ItemType<GlowingSporeSac>()
                    },
                    Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<ConfusingMushroom>() + "]" + Lang.BossCheck("FeudalFungusInfo"),
                    Lang.BossCheck("FeudalFungusInfo2"),
                    "AAMod/CrossMod/BossChecklist/Fungus",
                    "AAMod/NPCs/Bosses/FeudalFungus/FeudalFungus_Head_Boss");
                #endregion

                #region Grips
                bossChecklist.Call("AddBoss", 2f, mod.NPCType("GripOfChaosRed"), mod,
                    Lang.BossCheck("GripsofChaos"),
                    (Func<bool>)(() => AAWorld.downedGrips),
                    ModContent.ItemType<CuriousClaw>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyBlue>(),
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyRed>(),
                        ModContent.ItemType<GripMaskBlue>(),
                        ModContent.ItemType<GripMaskRed>(),
                        ModContent.ItemType<GripsBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Grips.GripBag>(),
                        ModContent.ItemType<Items.Boss.Grips.ClawOfChaos>(),
                        ModContent.ItemType<Items.Boss.Grips.ClawBaton>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("CuriousClaw") + "]" + Lang.BossCheck("or") + "[i:" + AAMod.instance.ItemType("InterestingClaw") + "]" + Lang.BossCheck("atnight"),
                    Lang.BossCheck("GripsofChaosInfo"),
                    "AAMod/CrossMod/BossChecklist/Grips",
                    "AAMod/CrossMod/BossChecklist/GripsHead");
                #endregion

                #region Truffle Toad
                bossChecklist.Call("AddBoss", 2.5f, mod.NPCType("TruffleToad"), mod,
                    Lang.BossCheck("TruffleToad"),
                    (Func<bool>)(() => AAWorld.downedToad),
                    ModContent.ItemType<Toadstool>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Toad.ToadTrophy>(),
                        ModContent.ItemType<ToadMask>(),
                        ModContent.ItemType<ToadBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Toad.ToadBag>(),
                        ModContent.ItemType<Items.Boss.Toad.ToadLeg>(),
                        ModContent.ItemType<Items.Boss.Toad.ToadTongue>(),
                        ModContent.ItemType<Items.Boss.Toad.Todegun>(),
                        ModContent.ItemType<Items.Boss.Toad.MushrockStaff>(),
                        ModContent.ItemType<GlowingSporeSac>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Toadstool") + "]" + Lang.BossCheck("TruffleToadInfo"),
                    Lang.BossCheck("TruffleToadInfo2"),
                    "AAMod/CrossMod/BossChecklist/Toad",
                    "AAMod/NPCs/Bosses/Toad/TruffleToad_Head_Boss");
                #endregion

                #region Broodmother
                bossChecklist.Call("AddBoss", 3.5f, mod.NPCType("Broodmother"), mod,
                    Lang.BossCheck("Broodmother"),
                    (Func<bool>)(() => AAWorld.downedBrood),
                    ModContent.ItemType<DragonBell>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Broodmother.BroodmotherTrophy>(),
                        ModContent.ItemType<BroodmotherMask>(),
                        ModContent.ItemType<BroodBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Broodmother.BroodBag>(),
                        ModContent.ItemType<Items.Boss.Broodmother.DragonCape>(),
                        ModContent.ItemType<Items.Boss.Broodmother.BroodScale>(),
                        ModContent.ItemType<Incinerite>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DragonBell") + "]" + Lang.BossCheck("BroodmotherInfo"),
                    Lang.BossCheck("BroodmotherInfo2"),
                    "AAMod/CrossMod/BossChecklist/Brood",
                    "AAMod/NPCs/Bosses/Broodmother/Broodmother_Head_Boss");
                #endregion

                #region Hydra
                bossChecklist.Call("AddBoss", 3.5f, mod.NPCType("Hydra"), mod,
                    Lang.BossCheck("Hydra"),
                    (Func<bool>)(() => AAWorld.downedHydra),
                    ModContent.ItemType<HydraChow>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Hydra.HydraTrophy>(),
                        ModContent.ItemType<HydraMask1>(),
                        ModContent.ItemType<HydraBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Hydra.HydraBag>(),
                        ModContent.ItemType<Items.Boss.Hydra.HydraPendant>(),
                        ModContent.ItemType<Items.Boss.Hydra.HydraHide>(),
                        ModContent.ItemType<Abyssium>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("HydraChow") + "]" + Lang.BossCheck("HydraInfo"),
                    Lang.BossCheck("HydraInfo2"),
                    "AAMod/CrossMod/BossChecklist/Hydra",
                    "AAMod/NPCs/Bosses/Hydra/HydraHead1_Head_Boss",
                    null);
                #endregion

                #region Serpent
                bossChecklist.Call("AddBoss", 5.5f, mod.NPCType("SerpentHead"), mod,
                    Lang.BossCheck("SubzeroSerpent"),
                    (Func<bool>)(() => AAWorld.downedSerpent),
                    ModContent.ItemType<SubzeroCrystal>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentTrophy>(),
                        ModContent.ItemType<SerpentMask>(),
                        ModContent.ItemType<SerpentBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentBag>(),
                        ModContent.ItemType<Items.Boss.Serpent.ArcticMedallion>(),
                        ModContent.ItemType<Items.Boss.Serpent.BlizzardBuster>(),
                        ModContent.ItemType<Items.Boss.Serpent.Icepick>(),
                        ModContent.ItemType<Items.Boss.Serpent.SerpentSpike>(),
                        ModContent.ItemType<Items.Boss.Serpent.SerpentSting>(),
                        ModContent.ItemType<Items.Boss.Serpent.Sickle>(),
                        ModContent.ItemType<Items.Boss.Serpent.SickleShot>(),
                        ModContent.ItemType<Items.Boss.Serpent.SnakeStaff>(),
                        ModContent.ItemType<Items.Boss.Serpent.SnowflakeShuriken>(),
                        ModContent.ItemType<Items.Boss.Serpent.SubzeroSlasher>(),
                        ModContent.ItemType<SnowMana>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("SubzeroCrystal") + "]" + Lang.BossCheck("SubzeroSerpentInfo"),
                    Lang.BossCheck("SubzeroSerpentInfo2"),
                    "AAMod/CrossMod/BossChecklist/Serpent1",
                    "AAMod/NPCs/Bosses/Serpent/SerpentHead_Head_Boss");
                #endregion

                #region Djinn
                bossChecklist.Call("AddBoss", 5.5f, mod.NPCType("Djinn"), mod,
                    Lang.BossCheck("DesertDjinn"),
                    (Func<bool>)(() => AAWorld.downedDjinn),
                    ModContent.ItemType<DjinnLamp>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Djinn.DjinnTrophy>(),
                        ModContent.ItemType<DjinnMask>(),
                        ModContent.ItemType<DjinnBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Djinn.DjinnBag>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandstormMedallion>(),
                        ModContent.ItemType<Items.Boss.Djinn.DjinnBag>(),
                        ModContent.ItemType<Items.Boss.Djinn.Djinnerang>(),
                        ModContent.ItemType<Items.Boss.Djinn.Sandagger>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandLamp>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandScepter>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandstormCrossbow>(),
                        ModContent.ItemType<Items.Boss.Djinn.SultanScimitar>(),
                        ModContent.ItemType<DesertMana>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DjinnLamp") + "]" + Lang.BossCheck("DesertDjinnInfo"),
                    Lang.BossCheck("DesertDjinnInfo2"),
                    "AAMod/CrossMod/BossChecklist/Djinn",
                    "AAMod/NPCs/Bosses/Djinn/Djinn_Head_Boss");
                #endregion

                #region Sagittarius
                bossChecklist.Call("AddBoss", 5.7f, mod.NPCType("Sag"), mod,
                    Lang.BossCheck("Sagittarius"),
                    (Func<bool>)(() => AAWorld.downedSag),
                    ModContent.ItemType<Lifescanner>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Sagittarius.SagTrophy>(),
                        ModContent.ItemType<SagMask>(),
                        ModContent.ItemType<SagBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Sagittarius.SagBag>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.SagShield>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.Legg>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.NeutronStaff>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.SagCore>(),
                        ModContent.ItemType<Doomite>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Lifescanner") + "]" + Lang.BossCheck("SagittariusInfo"),
                    Lang.BossCheck("SagittariusInfo2"),
                    "AAMod/CrossMod/BossChecklist/Sag",
                    "AAMod/NPCs/Bosses/Sag/Sag_Head_Boss");
                #endregion

                #region Anubis
                bossChecklist.Call("AddBoss", 9.7f, mod.NPCType("Anubis"), mod,
                    Lang.BossCheck("Anubis"),
                    (Func<bool>)(() => AAWorld.downedAnubis),
                    ModContent.ItemType<Scepter>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.AnubisTrophy>(),
                        ModContent.ItemType<AnubisMask>(),
                        ModContent.ItemType<AnubisBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.AnubisBag>(),
                        ModContent.ItemType<Items.Boss.Anubis.ArtifactOfJudgement>(),
                        ModContent.ItemType<Items.Boss.Anubis.Judgment>(),
                        ModContent.ItemType<Items.Boss.Anubis.JackalsWrath>(),
                        ModContent.ItemType<Items.Boss.Anubis.NeithsString>(),
                        ModContent.ItemType<Items.Boss.Anubis.SandstormThrower>(),
                        ModContent.ItemType<Items.Boss.Anubis.DesertStaff>(),
                        ModContent.ItemType<Items.Boss.Anubis.SentryOfTheEye>(),
                        ModContent.ItemType<Items.Boss.Anubis.ForsakenFragment>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Scepter") + "]" + Lang.BossCheck("AnubisInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Anubis",
                    "AAMod/NPCs/Bosses/Anubis/Anubis_Head_Boss");
                #endregion

                #region Athena
                bossChecklist.Call("AddBoss", 11.5f, mod.NPCType("Athena"), mod,
                    Lang.BossCheck("Athena"),
                    (Func<bool>)(() => AAWorld.downedAthena),
                    ModContent.ItemType<Owl>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.AthenaTrophy>(),
                        ModContent.ItemType<AthenaMask>(),
                        ModContent.ItemType<AthenaBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.AthenaBag>(),
                        ModContent.ItemType<Items.Boss.Athena.SeraphHarp>(),
                        ModContent.ItemType<Items.Boss.Athena.SkycutterKopis>(),
                        ModContent.ItemType<Items.Boss.Athena.RazorwindLongbow>(),
                        ModContent.ItemType<Items.Boss.Athena.GaleOfWings>(),
                        ModContent.ItemType<Items.Boss.Athena.DivineWindCharm>(),
                        ModContent.ItemType<Items.Boss.Athena.GoddessFeather>()
                    },
                    Lang.BossCheck("Usean") + "[i:" + AAMod.instance.ItemType("Owl") + "]" + Lang.BossCheck("AthenaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Athena",
                    "AAMod/NPCs/Bosses/Athena/Athena_Head_Boss");
                #endregion

                #region Greed
                bossChecklist.Call("AddBoss", 11.5f, mod.NPCType("Greed"), mod,
                    Lang.BossCheck("Greed"),
                    (Func<bool>)(() => AAWorld.downedGreed),
                    ModContent.ItemType<GoldenGrub>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.GreedTrophy>(),
                        ModContent.ItemType<GreedMask>(),
                        ModContent.ItemType<GreedBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.GreedBag>(),
                        ModContent.ItemType<Items.Boss.Greed.DesireCharm>(),
                        ModContent.ItemType<Items.Boss.Greed.StoneSlammer>(),
                        ModContent.ItemType<Items.Boss.Greed.GildedGlock>(),
                        ModContent.ItemType<Items.Boss.Greed.GoldDigger>(),
                        ModContent.ItemType<Items.Boss.Greed.Miner>(),
                        ModContent.ItemType<Items.Boss.Greed.StoneShell>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("GoldenGrub") + "]" + Lang.BossCheck("GreedInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Greed",
                    "AAMod/NPCs/Bosses/Greed/Greed_Head_Boss");
                #endregion

                #region Rajah Rabbit
                bossChecklist.Call("AddBoss", 11.5f, mod.NPCType("Rajah"), mod,
                    Lang.BossCheck("RajahRabbit"),
                    (Func<bool>)(() => AAWorld.downedRajah),
                    ModContent.ItemType<GoldenCarrot>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.RajahTrophy>(),
                        ModContent.ItemType<RajahMask>(),
                        ModContent.ItemType<RajahBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.RajahBag>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahSash>(),
                        ModContent.ItemType<Items.Boss.Rajah.BaneOfTheBunny>(),
                        ModContent.ItemType<Items.Boss.Rajah.Punisher>(),
                        ModContent.ItemType<Items.Boss.Rajah.Bunzooka>(),
                        ModContent.ItemType<Items.Boss.Rajah.RoyalScepter>(),
                        ModContent.ItemType<Items.Boss.Rajah.CottonCane>(),
                        ModContent.ItemType<Items.Boss.Rajah.RabbitcopterEars>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahPelt>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("GoldenCarrot") + "]" + Lang.BossCheck("RajahRabbitInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Rajah",
                    "AAMod/NPCs/Bosses/Rajah/Rajah_Head_Boss");
                #endregion

                #region Forsaken Anubis
                bossChecklist.Call("AddBoss", 15f, mod.NPCType("ForsakenAnubis"), mod,
                    Lang.BossCheck("AnubisA"),
                    (Func<bool>)(() => AAWorld.downedAnubisA),
                    ModContent.ItemType<Scepter>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.FAnubisTrophy>(),
                        ModContent.ItemType<FAnubisMask>(),
                        ModContent.ItemType<AnubisFBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.FAnubisBag>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.ArtifactOfGuilt>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.Verdict>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.Soulsplitter>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.Lifeline>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.CursedFury>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.ForsakenStaff>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.HorusCane>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.SoulFragment>()
                    },
                    Lang.BossCheck("AnubisAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/FAnubis",
                    "AAMod/NPCs/Bosses/Anubis/Forsaken/ForsakenAnubis_Head_Boss");
                #endregion

                #region Olympian Athena
                bossChecklist.Call("AddBoss", 15.1f, mod.NPCType("AthenaA"), mod,
                    Lang.BossCheck("AthenaA"),
                    (Func<bool>)(() => AAWorld.downedAthenaA),
                    ModContent.ItemType<Owl>(),
                    new List<int>
                    {
                        ModContent.ItemType<AthenaABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.Olympian.AthenaABag>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.GoddessHarp>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.Olympia>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.Windfury>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.GaleForce>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.HurricaneStone>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.StarChart>()
                    },
                    Lang.BossCheck("AthenaAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/AthenaA",
                    "AAMod/NPCs/Bosses/Athena/Olympian/AthenaA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAnubisA));
                #endregion

                #region Worm King Greed
                bossChecklist.Call("AddBoss", 15.2f, mod.NPCType("GreedA"), mod,
                    Lang.BossCheck("GreedA"),
                    (Func<bool>)(() => AAWorld.downedGreedA),
                    ModContent.ItemType<GoldenGrub>(),
                    new List<int>
                    {
                        ModContent.ItemType<GreedABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.WKG.GreedABag>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.DesireTalisman>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.Earthbreaker>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.OreCannon>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.OreStaff>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.Unearther>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.GravitySphere>()
                    },
                    Lang.BossCheck("GreedAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/GreedA",
                    "AAMod/NPCs/Bosses/Greed/GreedA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAnubisA));
                #endregion

                #region Equinox Worms
                bossChecklist.Call("AddBoss", 16f, mod.NPCType("DaybringerHead"), mod,
                    Lang.BossCheck("NightcrawlerDaybringer"),
                    (Func<bool>)(() => AAWorld.downedEquinox),
                    ModContent.ItemType<EquinoxWorm>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Equinox.DBTrophy>(),
                        ModContent.ItemType<Items.Boss.Equinox.NCTrophy>(),
                        ModContent.ItemType<DaybringerMask>(),
                        ModContent.ItemType<NightcrawlerMask>(),
                        ModContent.ItemType<Equibox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Equinox.EquinoxBag>(),
                        ModContent.ItemType<Items.Boss.Equinox.RadiantStar>(),
                        ModContent.ItemType<Items.Boss.Equinox.DarkVoid>(),
                        ModContent.ItemType<Stardust>(),
                        ModContent.ItemType<DarkEnergy>(),
                        ModContent.ItemType<DarkmatterOre>(),
                        ModContent.ItemType<RadiumOre>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("EquinoxWorm") + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/Equinox",
                    "AAMod/CrossMod/BossChecklist/EquinoxHead");
                #endregion

                #region Ashe & Haruka
                bossChecklist.Call("AddBoss", 17f, mod.NPCType("Ashe"), mod,
                    Lang.BossCheck("SistersofDiscord"),
                    (Func<bool>)(() => AAWorld.downedSisters),
                    ModContent.ItemType<FlamesOfAnarchy>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.AH.AsheTrophy>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaTrophy>(),
                        ModContent.ItemType<SistersBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.AH.AHBag>(),
                        ModContent.ItemType<Items.Boss.AH.HeartOfPassion>(),
                        ModContent.ItemType<Items.Boss.AH.HeartOfSorrow>(),
                        ModContent.ItemType<Items.Boss.AH.AshRain>(),
                        ModContent.ItemType<Items.Boss.AH.FuryFlame>(),
                        ModContent.ItemType<Items.Boss.AH.FireSpiritStaff>(),
                        ModContent.ItemType<Items.Boss.AH.AsheSatchel>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaKunai>(),
                        ModContent.ItemType<Items.Boss.AH.Masamune>(),
                        ModContent.ItemType<Items.Boss.AH.MizuArashi>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaBox>()
                    },
                    Lang.BossCheck("Usethe") + "[i:" + AAMod.instance.ItemType("FlamesOfAnarchy") + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/AH",
                    "AAMod/CrossMod/BossChecklist/AHHead");
                #endregion

                #region Akuma
                bossChecklist.Call("AddBoss", 18f, mod.NPCType("Akuma"), mod,
                    Lang.BossCheck("Akuma"),
                    (Func<bool>)(() => AAWorld.downedAkuma),
                    ModContent.ItemType<DraconianSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Akuma.AkumaTrophy>(),
                        ModContent.ItemType<AkumaMask>(),
                        ModContent.ItemType<AkumaBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Akuma.ReignOfFire>(),
                        ModContent.ItemType<Items.Boss.Akuma.DragonSlasher>(),
                        ModContent.ItemType<Items.Boss.Akuma.Daycrusher>(),
                        ModContent.ItemType<Items.Boss.Akuma.SunSpear>(),
                        ModContent.ItemType<Items.Boss.Akuma.Solar>(),
                        ModContent.ItemType<Items.Boss.Akuma.MorningGlory>(),
                        ModContent.ItemType<Items.Boss.Akuma.RadiantDawn>(),
                        ModContent.ItemType<Items.Boss.Akuma.YOTD>(),
                        ModContent.ItemType<Items.Boss.Akuma.DaybreakArrow>(),
                        ModContent.ItemType<Items.Boss.Akuma.Dawnstrike>(),
                        ModContent.ItemType<Items.Boss.Akuma.SunStorm>(),
                        ModContent.ItemType<Items.Boss.Akuma.Daystorm>(),
                        ModContent.ItemType<Items.Boss.Akuma.LungStaff>(),
                        ModContent.ItemType<Items.Boss.Akuma.AkumaTerratool>(),
                        ModContent.ItemType<Items.Boss.Akuma.CrucibleScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DraconianSigil") + "]" + Lang.BossCheck("AkumaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Akuma",
                    "AAMod/NPCs/Bosses/Akuma/Akuma_Head_Boss");

                bossChecklist.Call("AddBoss", 18.05f, mod.NPCType("AkumaA"), mod,
                    Lang.BossCheck("AkumaA"),
                    (Func<bool>)(() => AAWorld.downedAkuma),
                    ModContent.ItemType<DraconianRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Akuma.AkumaATrophy>(),
                        ModContent.ItemType<AkumaAMask>(),
                        ModContent.ItemType<AkumaABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Akuma.AkumaBag>(),
                        ModContent.ItemType<Items.Boss.Akuma.TaiyangBaolei>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DraconianRune") + "]" + Lang.BossCheck("AkumaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/AkumaA",
                    "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode));
                #endregion

                #region Yamata
                bossChecklist.Call("AddBoss", 18.1f, mod.NPCType("Yamata"), mod,
                    Lang.BossCheck("Yamata"),
                    (Func<bool>)(() => AAWorld.downedYamata),
                    ModContent.ItemType<DreadSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.YamataTrophy>(),
                        ModContent.ItemType<YamataMask>(),
                        ModContent.ItemType<YamataBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.AbyssalYari>(),
                        ModContent.ItemType<Items.Boss.Yamata.Hydraslayer>(),
                        ModContent.ItemType<Items.Boss.Yamata.Flairdra>(),
                        ModContent.ItemType<Items.Boss.Yamata.HydraStabber>(),
                        ModContent.ItemType<Items.Boss.Yamata.Crescent>(),
                        ModContent.ItemType<Items.Boss.Yamata.AE>(),
                        ModContent.ItemType<Items.Boss.Yamata.Darksprayer>(),
                        ModContent.ItemType<Items.Boss.Yamata.FallingTwilight>(),
                        ModContent.ItemType<Items.Boss.Yamata.MidnightWrath>(),
                        ModContent.ItemType<Items.Boss.Yamata.Sevenshot>(),
                        ModContent.ItemType<Items.Boss.Yamata.ThrowingCrescent>(),
                        ModContent.ItemType<Items.Boss.Yamata.Toxibomb>(),
                        ModContent.ItemType<Items.Boss.Yamata.YamataTerratool>(),
                        ModContent.ItemType<Items.Boss.Yamata.DreadScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DreadSigil") + "]" + Lang.BossCheck("YamataInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Yamata",
                    "AAMod/NPCs/Bosses/Yamata/YamataHead_Head_Boss");

                bossChecklist.Call("AddBoss", 18.15f, mod.NPCType("YamataA"), mod,
                    Lang.BossCheck("YamataA"),
                    (Func<bool>)(() => AAWorld.downedYamata),
                    ModContent.ItemType<DreadRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.YamataATrophy>(),
                        ModContent.ItemType<YamataAMask>(),
                        ModContent.ItemType<YamataABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.YamataBag>(),
                        ModContent.ItemType<Items.Boss.Yamata.Naitokurosu>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DreadRune") + "]" + Lang.BossCheck("YamataInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/YamataA",
                    "AAMod/NPCs/Bosses/Yamata/Awakened/YamataAHead_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode));
                #endregion
                
                #region Zero
                bossChecklist.Call("AddBoss", 18.2f, mod.NPCType("Zero"), mod,
                    Lang.BossCheck("Zero"),
                    (Func<bool>)(() => AAWorld.downedZero),
                    ModContent.ItemType<ZeroTesseract>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroTrophy>(),
                        ModContent.ItemType<ZeroMask>(),
                        ModContent.ItemType<ZeroBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.RiftShredder>(),
                        ModContent.ItemType<Items.Boss.Zero.EventHorizon>(),
                        ModContent.ItemType<Items.Boss.Zero.Vortex>(),
                        ModContent.ItemType<Items.Boss.Zero.BHB>(),
                        ModContent.ItemType<Items.Boss.Zero.GenocideCannon>(),
                        ModContent.ItemType<Items.Boss.Zero.Gigataser>(),
                        ModContent.ItemType<Items.Boss.Zero.Neutralizer>(),
                        ModContent.ItemType<Items.Boss.Zero.OmegaVolley>(),
                        ModContent.ItemType<Items.Boss.Zero.RealityCannon>(),
                        ModContent.ItemType<Items.Boss.Zero.TeslaHand>(),
                        ModContent.ItemType<Items.Boss.Zero.ZeroArrow>(),
                        ModContent.ItemType<Items.Boss.Zero.Battery>(),
                        ModContent.ItemType<Items.Boss.Zero.VoidStar>(),
                        ModContent.ItemType<Items.Boss.Zero.DoomRay>(),
                        ModContent.ItemType<Items.Boss.Zero.DoomPortal>(),
                        ModContent.ItemType<Items.Boss.Zero.ZeroTerratool>(),
                        ModContent.ItemType<Items.Boss.Zero.UnstableSingularity>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ZeroTesseract") + "]" + Lang.BossCheck("ZeroInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Zero",
                    "AAMod/NPCs/Bosses/Zero/Zero_Head_Boss");

                bossChecklist.Call("AddBoss", 18.25f, mod.NPCType("ZeroProtocol"), mod,
                    Lang.BossCheck("ZeroP"),
                    (Func<bool>)(() => AAWorld.downedZero),
                    ModContent.ItemType<ZeroRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroATrophy>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroBag>(),
                        ModContent.ItemType<Items.Boss.Zero.BrokenCode>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ZeroRune") + "]" + Lang.BossCheck("ZeroInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/ZeroProtocol",
                    "AAMod/NPCs/Bosses/Zero/Protocol/ZeroProtocol_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedZero && Main.expertMode));
                #endregion

                #region Champion Rajah Rabbit
                bossChecklist.Call("AddBoss", 19f, mod.NPCType("SupremeRajah"), mod,
                    Lang.BossCheck("RajahRabbitRevenge"),
                    (Func<bool>)(() => AAWorld.downedRajahsRevenge),
                    ModContent.ItemType<GoldenCarrot>(),
                    new List<int>
                    {
                            ///ModContent.ItemType<SRajahBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.RajahCache>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahCape>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.Excalihare>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.BaneOfTheBunnyEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.PunisherEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.FluffyFury>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.BunzookaEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.RabbitsWrath>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.RoyalScepterEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.CottonCaneEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.ChampionPlate>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DiamondCarrot") + "]" + Lang.BossCheck("RajahRabbitRevengeInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/CRajah",
                    "AAMod/NPCs/Bosses/Rajah/SupremeRajah_Head_Boss");
                #endregion

                #region Shen
                bossChecklist.Call("AddBoss", 20f, mod.NPCType("Shen"), mod,
                    Lang.BossCheck("ShenDoragon"),
                    (Func<bool>)(() => AAWorld.downedShen),
                    ModContent.ItemType<ChaosSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Shen.ShenTrophy>(),
                        ModContent.ItemType<ShenMask>(),
                        ModContent.ItemType<ShenBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Shen.ChaosSlayer>(),
                        ModContent.ItemType<Items.Boss.Shen.Astroid>(),
                        ModContent.ItemType<Items.Boss.Shen.Timesplitter>(),
                        ModContent.ItemType<Items.Boss.Shen.DraconicRipper>(),
                        ModContent.ItemType<Items.Boss.Shen.FlamingTwilight>(),
                        ModContent.ItemType<Items.Boss.Shen.Skyfall>(),
                        ModContent.ItemType<Items.Boss.Shen.MeteorStrike>(),
                        ModContent.ItemType<Items.Boss.Shen.ShenTerratool>(),
                        ModContent.ItemType<Items.Boss.Shen.ChaosScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ChaosSigil") + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/Shen",
                    "AAMod/NPCs/Bosses/Shen/Shen_Head_Boss");

                bossChecklist.Call("AddBoss", 20.1f, mod.NPCType("ShenA"), mod,
                    Lang.BossCheck("ShenDoragonA"),
                    (Func<bool>)(() => AAWorld.downedShen),
                    ModContent.ItemType<ChaosRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Shen.ShenATrophy>(),
                        ModContent.ItemType<ShenAMask>(),
                        ModContent.ItemType<ShenABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Shen.ShenCache>(),
                        ModContent.ItemType<Items.Boss.Shen.ChaosSoul>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ChaosRune") + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/ShenA",
                    "AAMod/NPCs/Bosses/Shen/ShenA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedShen && Main.expertMode));
                #endregion

                // SlimeKing = 1f;
                // EyeOfCthulhu = 2f;
                // EaterOfWorlds = 3f;
                // QueenBee = 4f;
                // Skeletron = 5f;
                // WallOfFlesh = 6f;
                // TheTwins = 7f;
                // TheDestroyer = 8f;
                // SkeletronPrime = 9f;
                // Plantera = 10f;
                // Golem = 11f;
                // DukeFishron = 12f;
                // LunaticCultist = 13f;
                // Moonlord = 14f;
            }
        }

        private static void PerformCencusSupport()
        {
            Mod censusMod = ModLoader.GetMod("Census");
            if (censusMod != null)
            {
                Mod mod = AAMod.instance;
                // Here I am using Chat Tags to make my condition even more interesting.
                // If you localize your mod, pass in a localized string instead of just English.
                //censusMod.Call("TownNPCCondition", mod.NPCType("Anubis"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");

                censusMod.Call("TownNPCCondition", mod.NPCType("Anubis"), Lang.CensusMod("Anubis"));
                if (!AAConfigClient.Instance.NoAATownNPC)
                {
                    censusMod.Call("TownNPCCondition", mod.NPCType("Mushman"), Lang.CensusMod("Mushman"));
                    censusMod.Call("TownNPCCondition", mod.NPCType("Lovecraftian"), Lang.CensusMod("Lovecraftian"));
                    censusMod.Call("TownNPCCondition", mod.NPCType("Samurai"), Lang.CensusMod("Samurai"));
                    censusMod.Call("TownNPCCondition", mod.NPCType("Goblin Slayer"), Lang.CensusMod("GoblinSlayer"));
                }
            }
        }

        private static void PerformFargosSetup()
        {
            Mod fargos = ModLoader.GetMod("Fargowiltas");
            if (fargos != null)
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper

                fargos.Call("AddSummon", 0f, "AAMod", "IntimidatingMushroom", (Func<bool>)(() => AAWorld.downedMonarch), 20000);
                fargos.Call("AddSummon", 0.1f, "AAMod", "ConfusingMushroom",(Func<bool>)(() => AAWorld.downedFungus), 20000);
                fargos.Call("AddSummon", 2f, "AAMod", "InterestingClaw", (Func<bool>)(() => AAWorld.downedGrips), 80000);
                fargos.Call("AddSummon", 2.5f, "AAMod", "Toadstool", (Func<bool>)(() => AAWorld.downedToad), 80000);
                fargos.Call("AddSummon", 3.5f, "AAMod", "DragonBell", (Func<bool>)(() => AAWorld.downedBrood), 100000);
                fargos.Call("AddSummon", 3.5f, "AAMod", "HydraChow", (Func<bool>)(() => AAWorld.downedHydra), 100000);
                fargos.Call("AddSummon", 5.5f, "AAMod", "SubzeroCrystal", (Func<bool>)(() => AAWorld.downedSerpent), 100000);
                fargos.Call("AddSummon", 5.5f, "AAMod", "DjinnLamp", (Func<bool>)(() => AAWorld.downedDjinn), 100000);
                fargos.Call("AddSummon", 5.7f, "AAMod", "Lifescanner", (Func<bool>)(() => AAWorld.downedSag), 200000);
                fargos.Call("AddSummon", 9.7f, "AAMod", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 9.7f, "AAMod", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "Owl", (Func<bool>)(() => AAWorld.downedAthena), 500000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "GoldenGrub", (Func<bool>)(() => AAWorld.downedGreed), 500000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "GoldenCarrot", (Func<bool>)(() => AAWorld.downedRajah), 600000);
                fargos.Call("AddSummon", 16f, "AAMod", "EquinoxWorm", (Func<bool>)(() => AAWorld.downedEquinox), 1000000);
                fargos.Call("AddSummon", 17f, "AAMod", "FlamesOfAnarchy", (Func<bool>)(() => AAWorld.downedSisters), 1000000);
                fargos.Call("AddSummon", 18f, "AAMod", "DraconianSigil", (Func<bool>)(() => AAWorld.downedAkuma), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "DraconianRune", (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.1f, "AAMod", "DreadSigil", (Func<bool>)(() => AAWorld.downedYamata), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "DreadRune", (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.2f, "AAMod", "ZeroTesseract", (Func<bool>)(() => AAWorld.downedZero), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "ZeroRune", (Func<bool>)(() => AAWorld.downedZero && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 19f, "AAMod", "DiamondCarrot", (Func<bool>)(() => AAWorld.downedRajahsRevenge), 2500000);
                fargos.Call("AddSummon", 20f, "AAMod", "ChaosSigil", (Func<bool>)(() => AAWorld.downedShen), 2500000);
                fargos.Call("AddSummon", 20.5f, "AAMod", "ChaosRune", (Func<bool>)(() => AAWorld.downedShen && Main.expertMode), 4000000);
            }
        }
    }
}
