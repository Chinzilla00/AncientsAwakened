using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Mask;
using AAMod.Items.BossSummons;
using AAMod.Items.Blocks;
using AAMod.Items.Flasks;
using AAMod.Items.Usable;

namespace AAMod.Globals
{
    internal class WeakReferences
    {
        public static void PerformModSupport()
        {
            PerformHealthBarSupport();
            PerformBossChecklistSupport();
            PerformCencusSupport();
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

                // Rajah
                string[] rajahTypes = new string[] { "Rajah", "Rajah2", "Rajah3", "Rajah4", "Rajah5", "Rajah6", "Rajah7", "Rajah8", "Rajah9", "SupremeRajah" };
                foreach (string rajahType in rajahTypes)
                {
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
                    yabhb.Call("hbFinishSingle", AAMod.instance.NPCType(rajahType));
                }             

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
            }
        }

        private static void PerformBossChecklistSupport()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");

            AAMod mod = AAMod.instance;

            if (bossChecklist != null)
            {
                #region Still working this out. Boss Checklist is fucked
                /*#region Mushroom Monarch
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
                    Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<IntimidatingMushroom>() + "]" + Lang.BossCheck("MushroomMonarchInfo"),
                    "The Mushroom Monarch escapes into the sky",
                    "AAMod/CrossMod/BossChecklist/Monarch",
                    "AAMod/NPCs/Bosses/MushroomMonarch/MushroomMonarch_Head_Boss",
                    null);
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
                    "The Feudal Fungus vanishes",
                    "AAMod/CrossMod/BossChecklist/Fungus",
                    "AAMod/NPCs/Bosses/MushroomMonarch/FeudalFungus_Head_Boss",
                    null);
                #endregion

                #region Grips
                bossChecklist.Call("AddBoss", 2f, mod.NPCType("FeudalFungus"), mod,
                    Lang.BossCheck("FeudalFungus"),
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
                    "The Grips fly away, having dealt with you",
                    "AAMod/CrossMod/BossChecklist/Grips",
                    "AAMod/CrossMod/BossChecklist/GripsHead",
                    null);
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
                    "The Truffle Toad croaks, then vanishes",
                    "AAMod/CrossMod/BossChecklist/Toad",
                    "AAMod/NPCs/Bosses/Toad/TruffleToad_Head_Boss",
                    null);
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
                    "The Broodmother returns to the fiery depths",
                    "AAMod/CrossMod/BossChecklist/Brood",
                    "AAMod/NPCs/Bosses/Broodmother/Broodmother_Head_Boss",
                    null);
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
                    "The Hydra slinks back into her dark lair",
                    "AAMod/CrossMod/BossChecklist/Hydra",
                    "AAMod/NPCs/Bosses/Hydra/HydraHead1_Head_Boss",
                    null);
                #endregion*/

                #endregion

                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("MushroomMonarch"), 0f, (Func<bool>)(() => AAWorld.downedMonarch), Lang.BossCheck("Usean") + "[i:" + AAMod.instance.ItemType("IntimidatingMushroom") + "]" + Lang.BossCheck("MushroomMonarchInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("FeudalFungus"), 0.1f, (Func<bool>)(() => AAWorld.downedFungus), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ConfusingMushroom") + "]" + Lang.BossCheck("FeudalFungusInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("GripsofChaos"), 2f, (Func<bool>)(() => AAWorld.downedGrips), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("CuriousClaw") + "]" + Lang.BossCheck("or") + "[i:" + AAMod.instance.ItemType("InterestingClaw") + "]" + Lang.BossCheck("atnight"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("TruffleToad"), 3f, (Func<bool>)(() => AAWorld.downedToad), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Toadstool") + "]" + Lang.BossCheck("TruffleToadInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Broodmother"), 3.5f, (Func<bool>)(() => AAWorld.downedBrood), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DragonBell") + "]" + Lang.BossCheck("BroodmotherInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Hydra"), 3.5f, (Func<bool>)(() => AAWorld.downedHydra), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("HydraChow") + "]" + Lang.BossCheck("HydraInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("SubzeroSerpent"), 5.5f, (Func<bool>)(() => AAWorld.downedSerpent), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("SubzeroCrystal") + "]" + Lang.BossCheck("SubzeroSerpentInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("DesertDjinn"), 5.5f, (Func<bool>)(() => AAWorld.downedDjinn), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DjinnLamp") + "]" + Lang.BossCheck("DesertDjinnInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Sagittarius"), 5.7f, (Func<bool>)(() => AAWorld.downedSag), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Lifescanner") + "]" + Lang.BossCheck("SagittariusInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Anubis"), 9.5f, (Func<bool>)(() => AAWorld.downedAnubis), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("Scepter") + "]" + Lang.BossCheck("AnubisInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Athena"), 11.5f, (Func<bool>)(() => AAWorld.downedAthena), Lang.BossCheck("Usean") + "[i:" + AAMod.instance.ItemType("Owl") + "]" + Lang.BossCheck("AthenaInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Greed"), 11.5f, (Func<bool>)(() => AAWorld.downedGreed), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("GoldenGrub") + "]" + Lang.BossCheck("GreedInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("RajahRabbit"), 12.1f, (Func<bool>)(() => AAWorld.downedRajah), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("GoldenCarrot") + "]" + Lang.BossCheck("RajahRabbitInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("AthenaA"), 14.2f, (Func<bool>)(() => AAWorld.downedAthenaA), Lang.BossCheck("AthenaAInfo"), (Func<bool>)(() => AAWorld.AthenaHerald));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("GreedA"), 14.4f, (Func<bool>)(() => AAWorld.downedGreedA), Lang.BossCheck("GreedAInfo"), (Func<bool>)(() => AAWorld.AthenaHerald));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("AnubisA"), 15f, (Func<bool>)(() => AAWorld.downedAnubisA), Lang.BossCheck("AnubisAInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("NightcrawlerDaybringer"), 15f, (Func<bool>)(() => AAWorld.downedEquinox), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("EquinoxWorm") + "]");
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("SistersofDiscord"), 16.1f, (Func<bool>)(() => AAWorld.downedSisters), Lang.BossCheck("Usethe") + "[i:" + AAMod.instance.ItemType("FlamesOfAnarchy") + "]");
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Yamata"), 16.2f, (Func<bool>)(() => AAWorld.downedYamata), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DreadSigil") + "]" + Lang.BossCheck("YamataInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Akuma"), 16.3f, (Func<bool>)(() => AAWorld.downedAkuma), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DraconianSigil") + "]" + Lang.BossCheck("AkumaInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("Zero"), 16.4f, (Func<bool>)(() => AAWorld.downedZero), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ZeroTesseract") + "]" + Lang.BossCheck("ZeroInfo"));
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("ShenDoragon"), 20f, (Func<bool>)(() => AAWorld.downedShen), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("ChaosSigil") + "]");
                bossChecklist.Call("AddBossWithInfo", Lang.BossCheck("RajahRabbitRevenge"), 40f, (Func<bool>)(() => AAWorld.downedRajahsRevenge), Lang.BossCheck("Usea") + "[i:" + AAMod.instance.ItemType("DiamondCarrot") + "]" + Lang.BossCheck("RajahRabbitRevengeInfo"));

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

                censusMod.Call("TownNPCCondition", mod.NPCType("Anubis"), "Always available");
                if (!AAConfigClient.Instance.NoAATownNPC)
                {
                    censusMod.Call("TownNPCCondition", mod.NPCType("Mushman"), "After defeating Mushroom Monarch or Feudal Fungus, build a house in a red mushroom biome");
                    censusMod.Call("TownNPCCondition", mod.NPCType("Lovecraftian"), "Eye of Cthulhu defeated");
                    censusMod.Call("TownNPCCondition", mod.NPCType("Samurai"), "Grips of Chaos defeated");
                    censusMod.Call("TownNPCCondition", mod.NPCType("Goblin Slayer"), "Goblin Army is defeated");
                }
            }
        }
    }
}
