﻿using AAMod.Buffs;
using AAMod.Items;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using AAMod.NPCs.Bosses.Athena;
using AAMod.NPCs.Bosses.Athena.Olympian;
using AAMod.NPCs.Bosses.Shen;
using AAMod.NPCs.Bosses.Yamata;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.Zero.Protocol;
using AAMod.NPCs.Bosses.Anubis.Forsaken;
using AAMod.Items.FishingItem;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.Audio;

namespace AAMod
{
    public partial class AAPlayer : ModPlayer
    {
        #region Variables

        #region Minions
        public bool FireSpirit = false;
        public bool ImpServant = false;
        public bool ImpSlave = false;
        public bool MoonBee = false;
        public bool Searcher = false;
        public bool enderMinion = false;
        public bool enderMinionEX = false;
        public bool LungMinion = false;
        public bool DragonMinion = false;
        public bool BabyPhoenix = false;
        public bool GripMinion = false;
        public bool ProbeMinion = false;
        public bool SkullMinion = false;
        public bool EaterMinion = false;
        public bool CrimeraMinion = false;
        public bool CrowMinion = false;
        public bool DemonMinion = false;
        public bool DevilMinion = false;
        public bool DoomiteProbe = false;
        public bool DoomiteProbeC = false;
        public bool TerraMinion = false;
        public bool HallowedPrism = false;
        public bool TrueHallowedPrism = false;
        public bool SnakeMinion = false;
        public bool dustDevil = false;
        public bool KrakenMinion = false;
        public bool Fishnado = false;
        public bool MadnessElemental = false;
        public bool FlameSoul = false;
        public bool Orbiters = false;
        public bool Protocol = false;
        public bool ScoutMinion = false;
        public bool SagOrbiter = false;
        public bool Rabbitcopter = false;
        public bool RabbitcopterR = false;
        public bool Sock = false;
        public bool Socc = false;
        public bool Squirrel = false;
        public bool DapperSquirrel = false;
        public bool CyberClaw = false;
        public bool ChaosClaw = false;
        public bool MiniZero = false;
        public bool TerraSummon = false;
        public bool DragonSpirit = false;
        public bool Seraph = false;
        public bool Athena = false;
        public bool Baron = false;
        public bool Xiao = false;
        public bool ChaosConstruct = false;
        public bool CCBook = false;
        public bool CCBookEX = false;
        public bool WeakCCRune = false;
        public bool CCRune = false;
        public bool Cerla = false;
        public bool CerlaEX = false;
        #endregion

        #region Biome bools.
        public bool ZoneMire = false;
        public bool ZoneInferno = false;
        public bool ZoneVoid = false;
        public bool ZoneMush = false;
        public bool ZoneStorm = false;
        public bool ZoneRisingSunPagoda = false;
        public bool ZoneRisingMoonLake = false;
        public bool ZoneShip = false;
        public bool VoidUnit = false;
        public bool SunAltar = false;
        public bool MoonAltar = false;
        public bool AkumaAltar = false;
        public bool YamataAltar = false;
        public bool Terrarium = false;
        public bool ZoneStars = false;
        public bool ZoneHoard = false;
        public bool ZoneAcropolis = false;
        public bool AshCurse;
        public int VoidGrav = 0;
        public static int Ashes = 0;
        public int CthulhuCountdown = 10800;
        public bool Leave = false;

        public bool ZoneTower;

        public bool RadiumStars = false;
        public bool Darkmatter = false;
        #endregion

        #region Armor bools.
        public bool AncientGoldBody = false;
        public bool AncientGoldLeg = false;
        public bool AncientGoldSet = false;
        public bool StripeManFish = false;
        public bool StripeManOre = false;
        public bool StripeManSpawn = false;
        public bool StripeManSet = false;
        public bool MoonSet;
        public bool goblinSlayer;
        public bool IsGoblin;
        public bool leatherSet;
        public bool mushiumSet;
        public bool kindledSet;
        public bool depthSet;
        public bool impSet;
        public bool DynaskullSet;
        public bool fleshrendSet;
        public bool nightsSet;
        public bool deathlySet;
        public bool tribalSet;
        public bool demonSet;
        public bool demonBonus;
        public bool terraSet;
        public bool chaosSet;
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool radiumMe;
        public bool radiumRa;
        public bool radiumMa;
        public bool radiumSu;
        public bool DarkmatterSet;
        public bool dracoSet;
        public bool dreadSet;
        public bool zeroSet1;
        public bool zeroSet;
        public bool valkyrieSet;
        public bool infinitySet;
        public bool Alpha;
        public bool Palladium;
        public bool fulgurite;
        public bool ringActive = false;
        public bool doomite;
        public bool Radium;
        public bool perfectChaos;
        public bool perfectChaosMe;
        public bool perfectChaosRa;
        public bool perfectChaosMa;
        public bool perfectChaosSu;
        public bool Assassin;
        public bool AbyssalStealth;
        public bool Witch;

        public bool ChaosMe = false;
        public bool ChaosRa = false;
        public bool ChaosMe1 = false;
        public bool ChaosRa2 = false;
        public bool ChaosMa = false;
        public bool ChaosSu = false;

        public bool Olympian = false;
        public bool StoneSoldier = false;

        public bool ChampionMe = false;
        public bool ChampionRa = false;
        public bool ChampionMa = false;
        public int CarrotBuff = 0;
        public bool ChampionSu = false;

        public bool TerraMe = false;
        public bool TerraRa = false;
        public bool TerraSu = false;
        public int CrystalMode = 0;
        public bool TerraMa = false;
        public int RoseCooldown = 0;

        public bool onoPrevious;
        public bool ono;
        public bool onoHideVanity;
        public bool onoForceVanity;

        public bool AsheFlame;
        public float AsheFlameScale = 0f;
        public int AsheCooldown = 0;
        #endregion

        #region Accessory bools
        public bool artifactJudgement;
		public int artifactJudgementCharge = 0;
		public bool artifactGuilt;
		public int artifactGuiltCharge = 0;
        public bool clawsOfChaos;
        public bool HydraPendant;
        public bool demonGauntlet;
        public bool BrokenCode;
        public int AbilityCD = 180;
        public bool AshRemover;
        public bool FogRemover;
        public bool Baolei;
        public bool Naitokurosu;
        public bool Duality;
        public bool DragonShell;
        public bool ammo20percentdown = false;
        public int AADash;
        public int AADashTime;
        public int dashDelayAA;
        public bool RStar;
        public bool DVoid;
        public int[] AADoubleTapKeyTimer = new int[4];
        public int[] AAHoldDownKeyTimer = new int[4];
        public bool DiscordShredder;
        public bool lantern = false;
        public bool HeartP = false;
        public bool HeartS = false;
        public bool HeartA = false;
        public bool DragonsGuard = false;
        public bool ShadowBand = false;
        public bool RajahCape = false;
        public bool olympianWings = false;
        public bool BlackLotusEmblem = false;

        public bool SagShield = false;
        public bool ShieldUp = false;
        public int SagCooldown = 0;

        public bool GreedCharm;
        public bool GreedTalisman;
        public bool OldOneCharm = false;
        public bool SpellBookofRagnarok;
        public bool CursedEyeofSoulBinder;
        #endregion

        #region debuffs
        public bool CursedHellfire = false;
        public bool infinityOverload = false;
        public bool discordInferno = false;
        public bool dragonFire = false;
        public bool hydraToxin = false;
        public bool terraBlaze = false;
        public bool Snagged = false;
        public bool Snagged1 = false;
        public bool YamataCount = false;
        public bool YamataACount = false;
        public bool Clueless = false;
        public bool Yanked = false;
        public bool InfinityScorch = false;
        public bool LockedOn = false;
        public bool shroomed = false;
        public bool riftbent = false;
        public bool DestinedToDie = false;
        public int TeleportTimer = 0;
        public bool YamataGravity = false;
        public bool YamataAGravity = false;
        public bool Hunted = false;
        public bool IB = false;
        public bool Spear = false;
        public bool AkumaPain = false;
        public bool FFlames = false;
        #endregion

        #region buffs

        public bool Ronin = false;
        public bool Glitched = false;
        public bool Greed1 = false;
        public bool Greed2 = false;
        public float GreedyDamage = 0;

        public bool luckycalm = false;
        public bool luckythorns = false;
        public bool StripeCrasyLucky = false;
        public bool CrasyLucky = false;
        #endregion

        #region pets
        public bool Broodmini = false;
        public bool Raidmini = false;
        public bool MiniProbe = false;
        public bool Sharkron = false;
        public bool RoyalKitten = false;
        public bool Mudkip = false;
        public bool MudkipS = false;
        public bool BoomBoi = false;
        public bool DragonSoul = false;
        public bool Glowmoss = false;
        public bool Cerberus = false;
        public bool K9 = false;
        public bool Lunamini = false;
        public bool ZeroBab = false;
        #endregion

        //NPCcount
        public static int yamata = -1;

        #region Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);
        public static Color ZeroColor = new Color((int)(233 * 0.7f), (int)(53 * 0.7f), (int)(53 * 0.7f));
        public static Color groviteColor = new Color(138, (int)(39 * 0.7f), (int)(196 * 0.7f));
        public static bool[] groviteGlow = new bool[255];

        public static int ZeroKills = 0;

        public int ManaLantern = 0;
        #endregion

        #region Misc
        public bool Compass = false;
        public Vector2 RiftPos = new Vector2(0, 0);
        public int PrismCooldown = 0;
        public bool WorldgenReminder = false;
        public bool DemonSun = false;
        public bool AnubisBook = false;
        public bool GivenAnuSummon = false;
        public bool GivenWormIdol = false;

        public float spellbookDamage = 1f;
        public float MaxMovespeedboost = 0;
        public bool bossactive = false;
        public bool nohitplayer = true;

        #endregion

        #endregion

        #region Save/Load
        public override TagCompound Save()
        {
            var saved = new List<string>();
            if (AnubisBook) saved.Add("Book");
            if (GivenAnuSummon) saved.Add("Stick");
            if (GivenWormIdol) saved.Add("Idol");
            return new TagCompound
            {
                { "saved", saved }
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("saved");
            AnubisBook = downed.Contains("Book");
            GivenAnuSummon = downed.Contains("Stick");
            GivenWormIdol = downed.Contains("Idol");
        }

        #endregion

        #region Reset Effects

        public override void ResetEffects()
        {
            ResetMinionEffect();
            ResetArmorEffect();
            ResetAccessoryEffect();
            ResetBuffEffect();
            ResetDebuffEffect();
            ResetPetsEffect();

            spellbookDamage = 1f;
            MaxMovespeedboost = 0;
            bossactive = false;

            //EnemyChecks
            IsGoblin = false;
            ResetMiscEffect();
        }

        private void ResetMiscEffect()
        {
            Compass = false;
            DemonSun = false;
            onoPrevious = ono;
            ono = onoHideVanity = onoForceVanity = false;
        }

        private void ResetMinionEffect()
        {
            FireSpirit = false;
            ImpServant = false;
            ImpSlave = false;
            MoonBee = false;
            Searcher = false;
            enderMinion = false;
            enderMinionEX = false;
            BabyPhoenix = false;
            LungMinion = false;
            DragonMinion = false;
            GripMinion = false;
            ProbeMinion = false;
            SkullMinion = false;
            EaterMinion = false;
            CrimeraMinion = false;
            CrowMinion = false;
            DemonMinion = false;
            DevilMinion = false;
            DoomiteProbe = false;
            DoomiteProbeC = false;
            HallowedPrism = false;
            TrueHallowedPrism = false;
            TerraMinion = false;
            SnakeMinion = false;
            dustDevil = false;
            KrakenMinion = false;
            Fishnado = false;
            MadnessElemental = false;
            FlameSoul = false;
            Orbiters = false;
            Protocol = false;
            ScoutMinion = false;
            SagOrbiter = false;
            Rabbitcopter = false;
            RabbitcopterR = false;
            Sock = false;
            Socc = false;
            Squirrel = false;
            DapperSquirrel = false;
            CyberClaw = false;
            ChaosClaw = false;
            MiniZero = false;
            TerraSummon = false;
            DragonSpirit = false;
            Seraph = false;
            Athena = false;
            Baron = false;
            Xiao = false;
            ChaosConstruct = false;
            CCBook = false;
            CCBookEX = false;
            WeakCCRune = false;
            CCRune = false;
            Cerla = false;
            CerlaEX = false;
        }

        private void ResetArmorEffect()
        {
			artifactJudgement = false;
			artifactGuilt = false;
            MoonSet = false;
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            demonSet = false;
            demonBonus = false;
            fleshrendSet = false;
            goblinSlayer = false;
            tribalSet = false;
            impSet = false;
            terraSet = false;
            chaosSet = false;
            DynaskullSet = false;
            zeroSet = false;
            dracoSet = false;
            dreadSet = false;
            darkmatterSetMe = false;
            darkmatterSetRa = false;
            darkmatterSetMa = false;
            darkmatterSetSu = false;
            darkmatterSetTh = false;
            infinitySet = false;
            Alpha = false;
            Palladium = false;
            fulgurite = false;
            doomite = false;
            DarkmatterSet = false;
            perfectChaos = false;
            Assassin = false;
            AbyssalStealth = false;
            AsheFlame = false;
            Witch = false;
            ChaosMe = false;
            ChaosMe1 = false;
            ChaosRa = false;
            ChaosRa2 = false;
            ChaosMa = false;
            ChaosSu = false;
            Olympian = false;
            AncientGoldBody = false;
            AncientGoldLeg = false;
            AncientGoldSet = false;
            StripeManFish = false;
            StripeManOre = false;
            StripeManSpawn = false;
            StripeManSet = false;
            ChampionMe = false;
            ChampionRa = false;
            ChampionMa = false;
            ChampionSu = false;
            StoneSoldier = false;
            TerraMe = false;
            TerraRa = false;
            TerraSu = false;
            TerraMa = false;
        }

        private void ResetAccessoryEffect()
        {
            AshRemover = false;
            FogRemover = false;
            clawsOfChaos = false;
            HydraPendant = false;
            demonGauntlet = false;
            BrokenCode = false;
            Baolei = false;
            Duality = false;
            Naitokurosu = false;
            ammo20percentdown = false;
            AshCurse = !Main.dayTime && !AAWorld.downedAkuma;
            AADash = 0;
            DiscordShredder = false;
            RStar = false;
            DVoid = false;
            lantern = false;
            HeartP = false;
            HeartS = false;
            HeartA = false;
            BlackLotusEmblem = false;
            SagShield = false;
            ShieldUp = false;
            DragonsGuard = false;
            ShadowBand = false;
            RajahCape = false;
            GreedCharm = false;
            GreedTalisman = false;
            Greed1 = false;
            Greed2 = false;
            olympianWings = false;
            OldOneCharm = false;
            SpellBookofRagnarok = false;
            CursedEyeofSoulBinder = false;
        }

        private void ResetBuffEffect()
        {
            Ronin = false;
            luckycalm = false;
            luckythorns = false;
            CrasyLucky = false;
        }

        private void ResetDebuffEffect()
        {
            CursedHellfire = false;
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Clueless = false;
            Yanked = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            IB = false;
            Spear = false;
            AkumaPain = false;
            Greed1 = false;
            Greed2 = false;
            FFlames = false;
        }

        private void ResetPetsEffect()
        {
            Broodmini = false;
            Raidmini = false;
            MiniProbe = false;
            Sharkron = false;
            RoyalKitten = false;
            Mudkip = false;
            MudkipS = false;
            BoomBoi = false;
            DragonSoul = false;
            Glowmoss = false;
            Cerberus = false;
            K9 = false;
            Lunamini = false;
            ZeroBab = false;
        }

        public override void Initialize()
        {
            AbilityCD = 0;
            ManaLantern = 0;
            ZoneInferno = false;
            ZoneMire = false;
            ZoneMush = false;
            ZoneStorm = false;
            ZoneVoid = false;
            ZoneRisingMoonLake = false;
            ZoneRisingSunPagoda = false;
            ZoneShip = false;
            ZoneTower = false;
            ZoneStars = false;
            ZoneHoard = false;
            ZoneAcropolis = false;
            WorldgenReminder = false; 
            GivenAnuSummon = false;
            GivenWormIdol = false;
        }

        #endregion

        #region Biomes

        public override void UpdateBiomes()
        {
            bool Akuma = BaseAI.GetNPC(player.Center, ModContent.NPCType<Akuma>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaA>(), 5000) != -1;
            bool Yamata = BaseAI.GetNPC(player.Center, ModContent.NPCType<Yamata>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<YamataA>(), 5000) != -1;
            bool Zero = BaseAI.GetNPC(player.Center, ModContent.NPCType<Zero>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<ZeroProtocol>(), 5000) != -1;
            if(ModSupport.GetMod("AAModEXAI") != null)
            {
                Akuma = Akuma || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "Akuma").npc.type, 5000) != -1 || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "AkumaA").npc.type, 5000) != -1;
                Yamata = Yamata || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "Yamata").npc.type, 5000) != -1 || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "YamataA").npc.type, 5000) != -1;
                Zero = Zero || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "Zero").npc.type, 5000) != -1 || BaseAI.GetNPC(player.Center, ModSupport.GetModNPC("AAModEXAI", "ZeroProtocol").npc.type, 5000) != -1;
            }
            ZoneTower = player.ZoneTowerSolar || player.ZoneTowerNebula || player.ZoneTowerStardust || player.ZoneTowerVortex;
            ZoneMire = (AAWorld.mireTiles > 100) || Yamata;
            ZoneInferno = AAWorld.infernoTiles > 100 || Akuma;
            ZoneMush = AAWorld.mushTiles > 100;
            Terrarium = AAWorld.terraTiles >= 1;
            ZoneVoid = (AAWorld.voidTiles > 20 && player.ZoneSkyHeight) || (AAWorld.voidTiles > 100 && !player.ZoneSkyHeight || Zero);
            ZoneRisingMoonLake = AAWorld.lakeTiles >= 1;
            ZoneRisingSunPagoda = AAWorld.pagodaTiles >= 1;
            ZoneStars = AAWorld.Radium >= 20;
            ZoneHoard = AAWorld.HoardTiles > 1 && !ZoneStars;
            ZoneAcropolis = AAWorld.CloudTiles > 1;
        }

        public override void UpdateBiomeVisuals()
        {
            bool Underground = player.Center.Y > Main.worldSurface * 16;
            bool useAthena = NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
            bool useShenA = NPC.AnyNPCs(ModContent.NPCType<ShenA>());
            bool useShen = NPC.AnyNPCs(ModContent.NPCType<Shen>()) && !useShenA;
            bool useAkuma = NPC.AnyNPCs(ModContent.NPCType<AkumaA>()) || AkumaAltar;
            bool useYamata = NPC.AnyNPCs(ModContent.NPCType<YamataA>()) || YamataAltar;
            bool useAnu = NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>());

            if(ModSupport.GetMod("AAModEXAI") != null)
            {
                useAthena = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "AthenaA").npc.type);
                useShenA = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "ShenA").npc.type);
                useShen = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "Shen").npc.type) && !useShenA;
                useAkuma = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "AkumaA").npc.type) || AkumaAltar;
                useYamata = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "YamataA").npc.type) || YamataAltar;
                useAnu = NPC.AnyNPCs(ModSupport.GetModNPC("AAModEXAI", "ForsakenAnubis").npc.type);
            }

            bool useMire = (ZoneMire || MoonAltar) && !useYamata && !useShen && !useShenA && !useAnu;
            bool useInferno = (ZoneInferno || SunAltar) && !useAkuma && !useShen && !useShenA && !useAnu;
            bool useVoid = (ZoneVoid || VoidUnit) && !useShen && !useShenA && !useAnu;

            player.ManageSpecialBiomeVisuals("AAMod:AnubisSky", useAnu);
            player.ManageSpecialBiomeVisuals("AAMod:AthenaSky", useAthena);
            player.ManageSpecialBiomeVisuals("AAMod:ShenSky", useShen);
            player.ManageSpecialBiomeVisuals("AAMod:ShenASky", useShenA);
            player.ManageSpecialBiomeVisuals("AAMod:AkumaSky", useAkuma);
            player.ManageSpecialBiomeVisuals("AAMod:YamataSky", useYamata);

            if (!Underground)
            {
                player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", useInferno);
                player.ManageSpecialBiomeVisuals("AAMod:MireSky", useMire);
            }

            if (Main.UseHeatDistortion)
            {
                player.ManageSpecialBiomeVisuals("HeatDistortion", useAkuma || useInferno);
            }

            player.ManageSpecialBiomeVisuals("AAMod:VoidSky", useVoid);
        }

        public override bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            return ZoneMire == modOther.ZoneMire &&
                ZoneInferno == modOther.ZoneInferno &&
                ZoneVoid == modOther.ZoneVoid &&
                ZoneMush == modOther.ZoneMush &&
                Terrarium == modOther.Terrarium &&
                ZoneStorm == modOther.ZoneStorm &&
                ZoneShip == modOther.ZoneShip &&
                ZoneStars == modOther.ZoneStars &&
                ZoneHoard == modOther.ZoneHoard &&
                ZoneAcropolis == modOther.ZoneAcropolis;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            modOther.ZoneInferno = ZoneInferno;
            modOther.ZoneMire = ZoneMire;
            modOther.ZoneVoid = ZoneVoid;
            modOther.ZoneMush = ZoneMush;
            modOther.Terrarium = Terrarium;
            modOther.ZoneStorm = ZoneStorm;
            modOther.ZoneRisingMoonLake = ZoneRisingMoonLake;
            modOther.ZoneRisingSunPagoda = ZoneRisingSunPagoda;
            modOther.ZoneShip = ZoneShip;
            modOther.ZoneStars = ZoneStars;
            modOther.ZoneHoard = ZoneHoard;
            modOther.ZoneAcropolis = ZoneAcropolis;
        }

        public override void SendCustomBiomes(BinaryWriter bb)
        {
            BitsByte zoneByte = 0;
            zoneByte[0] = ZoneInferno;
            zoneByte[1] = ZoneMire;
            zoneByte[2] = ZoneVoid;
            zoneByte[3] = ZoneMush;
            zoneByte[4] = Terrarium;
            zoneByte[5] = ZoneStorm;
            zoneByte[6] = ZoneRisingSunPagoda;
            zoneByte[7] = ZoneRisingMoonLake;
            bb.Write(zoneByte);

            BitsByte zoneByte2 = 0;
            zoneByte2[0] = ZoneShip;
            zoneByte2[1] = ZoneStars;
            zoneByte2[2] = ZoneHoard;
            zoneByte2[3] = ZoneAcropolis;
            bb.Write(zoneByte2);
        }

        public override void ReceiveCustomBiomes(BinaryReader bb)
        {
            BitsByte zoneByte = bb.ReadByte();
            ZoneInferno = zoneByte[0];
            ZoneMire = zoneByte[1];
            ZoneVoid = zoneByte[2];
            ZoneMush = zoneByte[3];
            Terrarium = zoneByte[4];
            ZoneStorm = zoneByte[5];
            ZoneRisingSunPagoda = zoneByte[6];
            ZoneRisingMoonLake = zoneByte[7];

            BitsByte zoneByte2 = bb.ReadByte();
            ZoneShip = zoneByte2[0];
            ZoneStars = zoneByte2[1];
            ZoneHoard = zoneByte2[2];
            ZoneAcropolis = zoneByte2[3];
        }

        #endregion

        #region Hit Effects

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit) 
		{
			if (npc.HasBuff(mod.BuffType("ForsakenWeak")))
			{
				damage -= damage/5;
			}

            if(luckythorns)
            {
                if (player.whoAmI == Main.myPlayer && !player.immune && !npc.dontTakeDamage)
                {
                    int RDamage = (int)(npc.damage * player.allDamage * 0.433f);
                    int direc = -1;
                    if (npc.position.X + npc.width / 2 < player.position.X + player.width / 2)
                    {
                        direc = 1;
                    }
                    player.ApplyDamageToNPC(npc, RDamage, 10f, -direc, false);
                }
            }
		}

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Palladium)
            {
                player.AddBuff(BuffID.RapidHealing, 300);
            }

            if (StoneSoldier)
            {
                if (target.life <= 0 && Main.rand.Next(80) == 0)
                {
                    Projectile.NewProjectile(target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0, Main.myPlayer);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (TerraRa && proj.ranged && Main.rand.Next(3) == 0)
            {
                float screenX;
                float screenY;
                if (Main.rand.Next(2) == 0)
                {
                    screenX = Main.screenPosition.X;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenX += Main.screenWidth;
                    }
                    screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                }
                else
                {
                    screenY = Main.screenPosition.Y;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenY += Main.screenHeight;
                    }
                    screenX = Main.screenPosition.X;
                    screenX += Main.rand.Next(Main.screenWidth);
                }
                Vector2 vector = new Vector2(screenX, screenY);
                float velocityX = target.Center.X - vector.X;
                float velocityY = target.Center.Y - vector.Y;
                velocityX += Main.rand.Next(-50, 51) * 0.1f;
                velocityY += Main.rand.Next(-50, 51) * 0.1f;
                float num6 = 24 / (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                velocityX *= num6;
                velocityY *= num6;
                Projectile p = Projectile.NewProjectileDirect(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<Items.Armor.Terra.Projectiles.TerraBullet>(), damage / 3, 0f, player.whoAmI);
                p.tileCollide = false;
            }

            if (Palladium)
            {
                player.AddBuff(BuffID.RapidHealing, 300);
            }

            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
                if (target.life <= 0 && Main.rand.Next(80) == 0)
                {
                    Projectile.NewProjectile(target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0, Main.myPlayer);
                }
            }

            if (target.HasBuff(mod.BuffType("Forsaken")) && proj.type == mod.ProjectileType("EnchancedMummyArrow"))
            {
				float num1 = 9f;
				Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
				float f1 = target.Center.X - vector2.X;
				float f2 = target.Center.Y - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
				
				float numberProjectiles = 3;
				float rotation = MathHelper.ToRadians(3);
				vector2 += Vector2.Normalize(new Vector2(SpeedX, SpeedY)) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(SpeedX, SpeedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
					Projectile.NewProjectile(vector2.X, vector2.Y, perturbedSpeed.X*2, perturbedSpeed.Y*2, mod.ProjectileType("ForsakenArrow"), damage/2, knockback, player.whoAmI);
				}
				target.buffImmune[mod.BuffType("Forsaken")] = true;
			}
        }

		public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
		{
			if (artifactJudgement)
			{
				artifactJudgementCharge += damage;
			}
			if (artifactGuilt)
			{
				artifactGuiltCharge += damage;
			}
		}

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (TerraMe)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TerraSphere"), 30, 4, Main.myPlayer, 0, npc.whoAmI);
            }

            if (DragonsGuard || ChaosMe)
            {
                npc.AddBuff(BuffID.OnFire, 120);
            }

            if (artifactJudgement)
            {
                artifactJudgementCharge += damage;
            }
            if (artifactGuilt)
            {
                artifactGuiltCharge += damage;
            }

            if (fleshrendSet && Main.rand.Next(2) == 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 position = new Vector2(player.Center.X - 40, player.Center.Y - 40);
                        Dust.NewDust(position, 80, 80, 108, 0f, 0f, 124, new Color(255, 50, 0), 1f);
                    }

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        float dist = npc.Distance(player.Center);

                        if (target.active && !target.dontTakeDamage && !target.friendly && target.immune[player.whoAmI] == 0 && dist < 100f)
                        {
                            player.ApplyDamageToNPC(target, 30, 0, 0, false); // target , damage, knockback, direction, crit
                        }
                    }
                }
            }

            if (ChaosMe)
            {
                npc.AddBuff(ModContent.BuffType<DragonFire>(), 180);
                npc.AddBuff(ModContent.BuffType<HydraToxin>(), 180);
            }

            if (BrokenCode)
            {
                player.AddBuff(BuffID.Panic, 180);
                player.immuneTime = player.longInvince ? 180 : 120;
            }

            if (npc.type == NPCID.GoblinArcher
                || npc.type == NPCID.GoblinPeon
                || npc.type == NPCID.GoblinScout
                || npc.type == NPCID.GoblinSorcerer
                || npc.type == NPCID.GoblinSummoner
                || npc.type == NPCID.GoblinThief
                || npc.type == NPCID.GoblinWarrior
                || npc.type == NPCID.DD2GoblinBomberT1
                || npc.type == NPCID.DD2GoblinBomberT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.DD2GoblinT1
                || npc.type == NPCID.DD2GoblinT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.BoundGoblin
                || npc.type == NPCID.GoblinTinkerer)
            {
                player.endurance += .8f;
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (goblinSlayer)
            {
                if (target.type == NPCID.GoblinArcher
                    || target.type == NPCID.GoblinPeon
                    || target.type == NPCID.GoblinScout
                    || target.type == NPCID.GoblinSorcerer
                    || target.type == NPCID.GoblinSummoner
                    || target.type == NPCID.GoblinThief
                    || target.type == NPCID.GoblinWarrior
                    || target.type == NPCID.DD2GoblinBomberT1
                    || target.type == NPCID.DD2GoblinBomberT2
                    || target.type == NPCID.DD2GoblinBomberT3
                    || target.type == NPCID.DD2GoblinT1
                    || target.type == NPCID.DD2GoblinT2
                    || target.type == NPCID.DD2GoblinBomberT3
                    || target.type == NPCID.BoundGoblin
                    || target.type == NPCID.GoblinTinkerer)
                {
                    damage *= 5;
                    IsGoblin = true;
                }
            }

            if (perfectChaosMe)
            {
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (valkyrieSet)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (Baolei)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }

            if (Naitokurosu)
            {
                int buff = Main.dayTime ? BuffID.Venom : ModContent.BuffType<Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (Duality)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : ModContent.BuffType<Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (darkmatterSetMe)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (kindledSet)
            {
                player.magmaStone = true;
            }

            if (clawsOfChaos)
            {
                player.ApplyDamageToNPC(target, 5, 0, 0, false);
            }

            if (DiscordShredder)
            {
                player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (demonGauntlet)
            {
                int buff = WorldGen.crimson ? BuffID.Ichor : BuffID.CursedInferno;
                target.AddBuff(buff, 180);
            }

            if (HeartP && player.statLife > (player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<DragonFire>(), 600);
            }
            else if (HeartP && player.statLife < (player.statLifeMax / 3))
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (HeartS && player.statLife > (player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<HydraToxin>(), 600);
            }
            else if (HeartS && player.statLife < (player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<Moonraze>(), 600);
            }

            if (dracoSet)
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (Alpha && !target.boss)
            {
                target.AddBuff(BuffID.Wet, 600);
            }

            if (player.HasBuff(mod.BuffType("DragonfireFlaskBuff")))
            {
                target.AddBuff(mod.BuffType("DragonFire"), 900);
            }

            if (player.HasBuff(mod.BuffType("HydratoxinFlaskBuff")))
            {
                target.AddBuff(mod.BuffType("Hydratoxin"), 900);
            }
            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
            }

            if (ChampionMa)
            {
                if (Main.rand.Next(30) == 0)
                {
                    int i = Item.NewItem(target.Hitbox, mod.ItemType("CarrotBooster"), 1, false, 0, true);
                    Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                }
            }
        }


        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (proj.melee)
            {
                if (perfectChaosMe)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (dracoSet)
                {
                    target.AddBuff(BuffID.Daybreak, 600);
                }

                if (valkyrieSet)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                    target.AddBuff(BuffID.Chilled, 180);
                }

                if (darkmatterSetMe)
                {
                    target.AddBuff(mod.BuffType("Electrified"), 500);
                }

                if (ChaosMe || ChaosMe1)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(mod.BuffType(buffName), 180);
                }

                if (demonGauntlet)
                {
                    int buff = WorldGen.crimson ? BuffID.Ichor : BuffID.CursedInferno;
                    target.AddBuff(buff, 180);
                }

                if (player.HasBuff(mod.BuffType("DragonfireFlaskBuff")))
                {
                    target.AddBuff(mod.BuffType("DragonFire"), 900);
                }

                if (player.HasBuff(mod.BuffType("HydratoxinFlaskBuff")))
                {
                    target.AddBuff(mod.BuffType("Hydratoxin"), 900);
                }
            }

            if (proj.ranged)
            {
                if (perfectChaosRa)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (dreadSet)
                {
                    target.AddBuff(ModContent.BuffType<Moonraze>(), 600);
                }

                if (DynaskullSet && Main.rand.Next(4) == 0)
                {
                    target.AddBuff(BuffID.Confused, 180);
                }

                if (depthSet)
                {
                    target.AddBuff(BuffID.Poisoned, 180);
                }

                if (darkmatterSetRa)
                {
                    target.AddBuff(mod.BuffType("Electrified"), 500);
                }

                if (ChaosRa || ChaosRa2)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(mod.BuffType(buffName), 180);
                }
            }

            if (proj.magic)
            {
                if (MoonSet)
                {
                    target.AddBuff(ModContent.BuffType<Moonraze>(), 300);
                }

                if (zeroSet)
                {
                    target.AddBuff(ModContent.BuffType<BrokenArmor>(), 1000);
                }

                if (perfectChaosMa)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (darkmatterSetMa)
                {
                    target.AddBuff(mod.BuffType("Electrified"), 500);
                }

                if (ChaosMa)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(mod.BuffType(buffName), 180);
                }

                if (BlackLotusEmblem)
                {
                    target.AddBuff(mod.BuffType("Moonraze"), 180);
                }
            }

            if (proj.minion)
            {
                if (zeroSet1)
                {
                    target.AddBuff(ModContent.BuffType<BrokenArmor>(), 1000);
                }

                if (perfectChaosSu)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (impSet)
                {
                    target.AddBuff(BuffID.OnFire, 180);
                }

                if (darkmatterSetSu)
                {
                    target.AddBuff(mod.BuffType("Electrified"), 500);
                }
            }

            if (proj.thrown)
            {
                if (darkmatterSetTh)
                {
                    target.AddBuff(mod.BuffType("Electrified"), 500);
                }

                if (Alpha && Main.rand.Next(2) == 0 && !target.boss)
                {
                    target.AddBuff(BuffID.Wet, 500);
                }
            }

            if (ChampionMa)
            {
                if (Main.rand.Next(30) == 0)
                {
                    int i = Item.NewItem(target.Hitbox, mod.ItemType("CarrotBooster"), 1, false, 0, true);
                    Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                }
            }

            if (Baolei && (proj.melee || proj.magic))
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }

            if (Naitokurosu && (proj.ranged || proj.minion))
            {
                int buff = Main.dayTime ? BuffID.Venom : ModContent.BuffType<Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (Duality)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : ModContent.BuffType<Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (clawsOfChaos)
            {
                player.ApplyDamageToNPC(target, 5, 0, 0, false);
            }

            if (DiscordShredder)
            {
                player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
            }
        }

        #endregion

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (Main.rand.Next(100) < (10 + (player.cratePotion ? 10 : 0)))
            {
                if (liquidType == 0 && player.ZoneSnow)
                {
                    caughtType = mod.ItemType("IceCrate");
                }

                if (liquidType == 0 && player.ZoneDesert)
                {
                    caughtType = mod.ItemType("DesertCrate");
                }

                if ((liquidType == 0 || liquidType == 1) && player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    caughtType = mod.ItemType("InfernoCrate");
                }

                if (liquidType == 0 && player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    caughtType = mod.ItemType("MireCrate");
                }

                if (liquidType == 0 && player.GetModPlayer<AAPlayer>().ZoneVoid)
                {
                    caughtType = mod.ItemType("VoidCrate");
                }

                if (liquidType == 0 && player.GetModPlayer<AAPlayer>().ZoneHoard)
                {
                    caughtType = ItemID.GoldenCrate;
                }

                if (liquidType == 1 && ItemID.Sets.CanFishInLava[fishingRod.type] && player.ZoneUnderworldHeight)
                {
                    caughtType = mod.ItemType("HellCrate");
                }
            }

            if (questFish == mod.ItemType("TriHeadedKoi") && player.GetModPlayer<AAPlayer>().ZoneMire && Main.rand.NextBool())
            {
                caughtType = mod.ItemType("TriHeadedKoi");
            }

            if (questFish == mod.ItemType("Fishmother") && player.GetModPlayer<AAPlayer>().ZoneInferno && Main.rand.NextBool())
            {
                caughtType = mod.ItemType("Fishmother");
            }

            if (questFish == mod.ItemType("GlitchFish") && player.GetModPlayer<AAPlayer>().ZoneVoid && Main.rand.NextBool())
            {
                caughtType = mod.ItemType("GlitchFish");
            }

            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if(Main.rand.Next(50) == 0 && Main.hardMode)
                {
                    caughtType = mod.ItemType("ScorchShark");
                }
                else if(Main.rand.Next(49) == 0)
                {
                    caughtType = mod.ItemType("SharpeningLavaFish");
                }
            }

            if (player.GetModPlayer<AAPlayer>().ZoneMire && Main.hardMode)
            {
                if(Main.rand.Next(50) == 0 && Main.hardMode)
                {
                    caughtType = mod.ItemType("SwimmingHydra");
                }
                else if(Main.rand.Next(49) == 0)
                {
                    caughtType = mod.ItemType("ToxinMonkfish");
                }
            }

            if ((Main.rand.Next(4096) == 0 && liquidType == 0 && player.fishingSkill >= 100)|| (Main.rand.Next(2048) == 0 && player.accFishingLine && player.accTackleBox))
            {
                caughtType = ModContent.ItemType<ShinyCharmFish>();
            }
        }

        public int[] Charges = null;
        public int[] Spheres = null;

        public float ShieldScale = 0;
        public float RingRotation = 0;

        public float TimeScale = 0;

        public override void PostUpdate()
        {
            if (!bossactive)
            {
                nohitplayer = true;
            }
            if (Ronin)
            {
                player.immune = true;
                player.immuneTime = 60;
            }
            if (olympianWings && player.dash < 1)
            {
                if (player.velocity.Y != 0)
                {
                    player.dash = 2;
                }
                else
                {
                    player.dash = 0;
                }
            }
			if (artifactJudgementCharge >= 250)
			{
				player.AddBuff(mod.BuffType("EyeOfJudgement"), 900);
				artifactJudgementCharge = 0;
			}
			if (artifactGuiltCharge >= 250)
			{
				player.AddBuff(mod.BuffType("EyeOfForsaken"), 900);
				artifactGuiltCharge = 0;
			}
            if (!Greed1 && !Greed2)
            {
                GreedyDamage = 0;
            }
            DarkmatterSet = darkmatterSetMe || darkmatterSetRa || darkmatterSetMa || darkmatterSetSu || darkmatterSetTh;

            if (NPC.AnyNPCs(ModContent.NPCType<AkumaTransition>()))
            {
                int n = BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaTransition>(), -1);
                NPC akuma = Main.npc[n];

                if (akuma.ai[0] >= 660)
                {
                    player.AddBuff(ModContent.BuffType<BlazingPain>(), 2);
                }
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                player.AddBuff(ModContent.BuffType<BlazingPain>(), 2);
            }

            if (BasePlayer.HasAccessory(player, ModContent.ItemType<Items.Vanity.HappySunSticker>(), true, true))
            {
                Main.sunTexture = mod.GetTexture("Backgrounds/DemonSun");
                Main.sun3Texture = mod.GetTexture("Backgrounds/DemonSunEclipse");
            }
            else
            {
                Main.sunTexture = ModContent.GetTexture("Terraria/Sun");;
                Main.sun3Texture = ModContent.GetTexture("Terraria/Sun3");
            }

            if (player.ZoneSandstorm && (ZoneInferno || ZoneMire))
            {
                EmitDust();
            }

            #region SagShieldDrawMethod

            if (SagCooldown > 0)
            {
                SagCooldown--;
            }
            else
            {
                SagCooldown = 0;
            }

            if (ShieldUp)
            {
                RingRotation += .05f;
                ShieldScale += .02f;
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
            }
            else
            {
                ShieldScale -= .02f;
                if (ShieldScale <= 0f)
                {
                    ShieldScale = 0f;
                }
            }

            if (ShieldScale > 0f || TimeScale > 0f)
            {
                RingRotation += .05f;
            }

            if (ShieldScale > 0)
            {
                RingRotation += .05f;
            }

            #endregion

            #region AsheFlameDrawMethod

            if (AsheCooldown > 0)
            {
                AsheCooldown--;
            }
            else
            {
                AsheCooldown = 0;
            }

            if (AsheFlame)
            {
                RingRotation += .05f;
                AsheFlameScale += .02f;
                if (AsheFlameScale >= 1f)
                {
                    AsheFlameScale = 1f;
                }
            }
            else
            {
                AsheFlameScale -= .02f;
                if (AsheFlameScale <= 0f)
                {
                    AsheFlameScale = 0f;
                }
            }

            if (AsheFlameScale > 0f)
            {
                RingRotation += .05f;
            }

            #endregion

            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Equinox.DaybringerHead>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Equinox.NightcrawlerHead>()))
            {
                TimeScale = 0;
            }

            if (Orbiters)
            {
                Spheres = BaseAI.GetProjectiles(player.Center, mod.ProjectileType("FireOrbiter"), Main.myPlayer, 48);

                if (player.ownedProjectileCounts[mod.ProjectileType("FireOrbiter")] > 0)
                {
                    player.minionDamage += AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<Projectiles.AH.FireOrbiter>()) * .1f;

                    if (Main.netMode != 2 && Main.LocalPlayer.miscCounter % 3 == 0)
                    {
                        for (int m = 0; m < Spheres.Length; m++)
                        {
                            Projectile projectile = Main.projectile[Spheres[m]];

                            if (projectile != null && projectile.active)
                            {
                                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDustLight>());

                                Main.dust[dustID].position += player.position - player.oldPosition;
                                Main.dust[dustID].velocity = (player.Center - projectile.Center) * 0.05f;
                                Main.dust[dustID].alpha = 100;
                                Main.dust[dustID].noGravity = true;
                            }
                        }
                    }
                }
            }

            if (AAWorld.ModContentGenerated || ZoneInferno || ZoneMire || ZoneVoid || Terrarium || ZoneMush)
            {
                AAWorld.ModContentGenerated = true;
                WorldgenReminder = true;
            }

            if (!WorldgenReminder)
            {
                int ReminderChoice = Main.rand.Next(11);


                if (Main.netMode != 1)
                {
                    switch (ReminderChoice)
                    {
                        case 0:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo1"), new Color(45, 46, 70), false);
                            break;
                        case 1:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo2"), new Color(45, 46, 70), false);
                            break;
                        case 2:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo3"), new Color(255, 0, 0), false);
                            break;
                        case 3:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo4"), new Color(102, 20, 48), false);
                            break;
                        case 4:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo5"), new Color(72, 78, 117), false);
                            break;
                        case 5:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo6"), new Color(128, 0, 0), false);
                            break;
                        case 6:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo7"), new Color(216, 110, 40), false);
                            break;
                        case 7:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo8"), new Color(43, 46, 61), false);
                            break;
                        case 8:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo9"), Color.CornflowerBlue, false);
                            break;
                        case 9:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo10"), Color.Goldenrod, false);
                            break;
                        case 10:
                            BaseUtility.Chat(Lang.AAPlayerChat("WorldgenReminderInfo11"), new Color(107, 137, 179), false);
                            break;

                    }
                }

                WorldgenReminder = true;
            }

            if (RStar)
            {
                Lighting.AddLight((int)(player.position.X + player.width / 2) / 16, (int)(player.position.Y + player.height / 2) / 16, 1f, 0.95f, 0.8f);
            }

            if (kindledSet || lantern)
            {
                Lighting.AddLight((int)(player.position.X + player.width / 2) / 16, (int)(player.position.Y + player.height / 2) / 16, AAColor.Lantern.R / 255, AAColor.Lantern.G / 255 * 0.95f, AAColor.Lantern.B / 255 * 0.8f);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<Yamata>()))
            {
                player.AddBuff(ModContent.BuffType<YamataGravity>(), 10, true);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                player.AddBuff(ModContent.BuffType<YamataAGravity>(), 10, true);
            }

            if (player.GetModPlayer<AAPlayer>().ZoneMire || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.dayTime && !AAWorld.downedYamata)
                {
                    if (!player.GetModPlayer<AAPlayer>().FogRemover)
                    {
                        player.AddBuff(ModContent.BuffType<Clueless>(), 5);
                    }
                }
            }

            if (player.GetModPlayer<AAPlayer>().Terrarium)
            {
                player.AddBuff(ModContent.BuffType<Terrarium>(), 2);
                player.AddBuff(BuffID.DryadsWard, 2);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()))
            {
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
                }

                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
            }

            if (player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AshCurse)
                {
                    AshRain(player);
                }
            }

            if (player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AAWorld.downedAllAncients && !AAWorld.downedShen)
                {
                    EmberRain(player);
                }
            }

            if(player.GetModPlayer<AAPlayer>().Assassin)
            {
                float RandomX = 50f;
                float RandomY = 25f;
                bool flag = player.itemAnimation > 0;
                if (flag && player.inventory[player.selectedItem].melee && Main.rand.Next(200) == 0 && player.whoAmI == Main.myPlayer)
                {
                    Vector2 SpeedVector = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
                    SpeedVector.Normalize();
                    if (SpeedVector.HasNaNs())
                    {
                        SpeedVector = Vector2.UnitX * player.direction;
                    }
                    SpeedVector *= 15f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(player.Center.X + player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[1] = new Vector2(player.Center.X - player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[2] = new Vector2(player.Center.X - player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    int i = 0;
                    while (i < 3)
                    {
                        if(Main.netMode != 1) Projectile.NewProjectile(Spwanposition[i].X, Spwanposition[i].Y, SpeedVector.X, SpeedVector.Y, mod.ProjectileType("AssassinDagger"), (int)(player.inventory[player.selectedItem].damage * 1.3), 2f, player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(SpeedVector.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, mod.DustType("AcidDust"), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                        i++;
                    }
                }
            }

            if (BlackLotusEmblem && player.inventory[player.selectedItem].mana > 0 && player.statMana < (int)(player.inventory[player.selectedItem].mana * player.manaCost))
            {
                BlackLotusQuickMana();
            }

            if (player.controlQuickHeal)
            {
                SpecialQuickHeal();
            }

            if (StripeManSet)
            {
                if(AAMod.ArmorAbilityKey.JustPressed)
                {
                    StripeCrasyLucky = !StripeCrasyLucky;
                }
            }

            if (StripeCrasyLucky || CrasyLucky)
            {
                if(StripeCrasyLucky) StripeCrasyLucky = true;
                Main.rand = new AAFakeRand();
                if(Main.raining)
                {
                    Main.rainTime = 300;
                    Main.maxRaining = .7f;
                }
            }
            else
            {
                StripeCrasyLucky = false;
                Main.rand = new UnifiedRandom();
            }

            if (ZoneVoid)
            {
                player.gravity = Player.defaultGravity + .1f;
            }

            if (CCBook || CCBookEX)
            {
                float slotscanuse = player.maxMinions - player.slotsMinions;
                if (slotscanuse > 1)
                {
                    bool RuneControl = player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>()] > 1 || player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>()] > 1 || player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>()] > 1;
                    bool RuneControlEX = player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.TerraRune>()] > 1 || player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.ChaosRune>()] > 1 || player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.VoidRune>()] > 1;
                    if (RuneControl || RuneControlEX)
                    {
                        player.ClearBuff(ModContent.BuffType<CCRune>());
                    }
                    if (player.FindBuffIndex(ModContent.BuffType<CCRune>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<CCRune>(), 3600, true);
                    }
                    if (CCBook)
                    {
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>()] < 1 && slotscanuse > 1f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>(), (int)(1 * player.minionDamage), 0, player.whoAmI, 0f, 0f);
                        }
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>()] < 1 && slotscanuse > 2f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>(), (int)(50 * player.minionDamage), 4f, player.whoAmI, 0f, 0f);
                        }
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>()] < 1 && slotscanuse > 3f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>(), (int)(100 * player.minionDamage), 2f, player.whoAmI, 0f, 0f);
                        }
                    }
                    if (CCBookEX)
                    {
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.TerraRune>()] < 1 && slotscanuse > 1f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.TerraRune>(), (int)(1 * player.minionDamage), 0, player.whoAmI, 0f, 0f);
                        }
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.ChaosRune>()] < 1 && slotscanuse > 2f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.ChaosRune>(), (int)(400 * player.minionDamage), 4f, player.whoAmI, 0f, 0f);
                        }
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.VoidRune>()] < 1 && slotscanuse > 3f)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.VoidRune>(), (int)(800 * player.minionDamage), 2f, player.whoAmI, 0f, 0f);
                        }
                    }
                }
            }

            if (ChampionMe && AAMod.ArmorAbilityKey.JustPressed && !player.HasBuff(ModContent.BuffType<Items.Armor.Champion.RageCool>()))
            {
                int BuffLength = 240;
                if (player.statLife < (int)(player.statLifeMax2 * .75f))
                {
                    BuffLength = 360;
                }
                if (player.statLife < (int)(player.statLifeMax2 * .5f))
                {
                    BuffLength = 480;
                }
                if (player.statLife < (int)(player.statLifeMax2 * .25f))
                {
                    BuffLength = 600;
                }
                player.AddBuff(ModContent.BuffType<Items.Armor.Champion.RageBuff>(), BuffLength);
                int RageCooldown = BuffLength * 4;
                player.AddBuff(ModContent.BuffType<Items.Armor.Champion.RageCool>(), RageCooldown);
            }

            if (player.HasBuff(ModContent.BuffType<Items.Armor.Champion.RageBuff>()))
            {
                player.armorEffectDrawShadowLokis = true;
            }

            if (ChampionRa && AAMod.ArmorAbilityKey.JustPressed && !player.HasBuff(mod.BuffType("DroneCool")) && 
                !AAGlobalProjectile.AnyProjectiles(mod.ProjectileType("RajahDrone")))
            {
                Vector2 vector2;
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, mod.ProjectileType("RajahDrone"), (int)(100 * player.rangedDamage), 2, Main.myPlayer, 0f, 0f);
            }

            if (TerraSu)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    CrystalMode++;
                    if (CrystalMode > 2)
                    {
                        CrystalMode = 0;
                    }
                }
                if (CrystalMode == 2)
                {
                    player.lifeRegen += 12;
                    player.statDefense = (int)(player.statDefense * 1.2f);
                    player.allDamage /= 2;
                }
            }

            if (RoseCooldown > 0)
            {
                RoseCooldown--;
            }

            if (TerraMa && RoseCooldown <= 0)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    RoseCooldown = 600;
                    float playerY = player.position.Y + player.height;

                    Projectile.NewProjectile(new Vector2(player.Center.X - 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<Items.Armor.Terra.Projectiles.TerraRoseA>(), (int)(50 * player.magicDamage), 4, Main.myPlayer);
                    Projectile.NewProjectile(new Vector2(player.Center.X + 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<Items.Armor.Terra.Projectiles.TerraRoseA>(), (int)(50 * player.magicDamage), 4, Main.myPlayer);
                }
            }
        }

        public void CarrotLevelup()
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 22; i++)
                {
                    if (player.buffType[i] == mod.BuffType("CBoost1") || 
                        player.buffType[i] == mod.BuffType("CBoost2") ||
                        player.buffType[i] == mod.BuffType("CBoost3"))
                    {
                        player.DelBuff(i);
                    }
                }
                CarrotBuff = (int)MathHelper.Clamp(CarrotBuff + 1, 0f, 3f);
                player.AddBuff(mod.BuffType("CBoost" + CarrotBuff), 480, true);
                return;
            }
        }

        public void SpecialQuickHeal()
        {
            if (player.noItems)
			{
				return;
			}
            Item item = new Item();
            for (int i = 0; i < 58; i++)
			{
                item = player.inventory[i];
				if (item.type == mod.ItemType("RoninPotion") && ItemLoader.CanUseItem(item, player))
				{
                    break;
                }
            }
			if (item == null)
			{
				return;
			}
			if (player.potionDelay > 0 || (player.statLife == player.statLifeMax2 && item.type != mod.ItemType("RoninPotion")))
			{
				return;
			}
			Main.PlaySound(item.UseSound, player.position);
			if (item.potion)
			{
				if (item.type == 227)
				{
					player.potionDelay = player.restorationDelayTime;
					player.AddBuff(21, player.potionDelay, true);
				}
				else
				{
					player.potionDelay = player.potionDelayTime;
					player.AddBuff(21, player.potionDelay, true);
				}
			}
			ItemLoader.UseItem(item, player);
			player.statLife += item.healLife;
			player.statMana += item.healMana;
			if (player.statLife > player.statLifeMax2)
			{
				player.statLife = player.statLifeMax2;
			}
			if (player.statMana > player.statManaMax2)
			{
				player.statMana = player.statManaMax2;
			}
			if (item.healLife > 0 && Main.myPlayer == player.whoAmI)
			{
				player.HealEffect(item.healLife, true);
			}
			if (item.healMana > 0)
			{
				player.AddBuff(94, Player.manaSickTime, true);
				if (Main.myPlayer == player.whoAmI)
				{
					player.ManaEffect(item.healMana);
				}
			}
			if (ItemLoader.ConsumeItem(item, player))
			{
				item.stack--;
			}
			if (item.stack <= 0)
			{
				item.TurnToAir();
			}
			Recipe.FindRecipes();
        }

        public void BlackLotusQuickMana()
		{
			if (player.noItems)
			{
				return;
			}
			if (player.statMana == player.statManaMax2)
			{
				return;
			}
			for (int i = 0; i < 58; i++)
			{
				if (player.inventory[i].stack > 0 && player.inventory[i].type > 0 && player.inventory[i].healMana > 0 && (player.potionDelay == 0 || !player.inventory[i].potion) && ItemLoader.CanUseItem(player.inventory[i], player))
				{
					Main.PlaySound(player.inventory[i].UseSound, player.position);
					if (player.inventory[i].potion)
					{
						if (player.inventory[i].type == 227)
						{
							player.potionDelay = player.restorationDelayTime;
							player.AddBuff(21, player.potionDelay, true);
						}
						else
						{
							player.potionDelay = player.potionDelayTime;
							player.AddBuff(21, player.potionDelay, true);
						}
					}
					ItemLoader.UseItem(player.inventory[i], player);
					player.statLife += player.inventory[i].healLife;
					player.statMana += player.inventory[i].healMana;
					if (player.statLife > player.statLifeMax2)
					{
						player.statLife = player.statLifeMax2;
					}
					if (player.statMana > player.statManaMax2)
					{
						player.statMana = player.statManaMax2;
					}
					if (player.inventory[i].healLife > 0 && Main.myPlayer == player.whoAmI)
					{
						player.HealEffect(player.inventory[i].healLife, true);
					}
					if (player.inventory[i].healMana > 0)
					{
						player.AddBuff(94, 60, true);
						if (Main.myPlayer == player.whoAmI)
						{
							player.ManaEffect(player.inventory[i].healMana);
						}
					}
					if (ItemLoader.ConsumeItem(player.inventory[i], player))
					{
						player.inventory[i].stack--;
					}
					if (player.inventory[i].stack <= 0)
					{
						player.inventory[i].TurnToAir();
					}
					Recipe.FindRecipes();
					return;
				}
			}
		}

        public override void PostUpdateBuffs()
        {
            if (player.mount.Active || player.mount.Cart)
            {
                player.dashDelay = 60;
                AADash = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            if (player.mount.Active || player.mount.Cart)
            {
                player.dashDelay = 60;
                AADash = 0;
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            float movespeedmax = 1f + MaxMovespeedboost;

            player.maxRunSpeed *= movespeedmax;
            
            if (player.pulley && AADash > 0)
            {
                AADashMovement();
            }
            else if (player.grappling[0] == -1 && !player.tongued)
            {
                AAHorizontalMovement();
                if (AADash > 0)
                {
                    AADashMovement();
                }
            }
        }
        
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if(item.ranged && Assassin)
            {
                speedX *= 1.3f;
                speedY *= 1.3f;
                Vector2 SpeedVector = new Vector2(speedX, speedY);
                if(Main.rand.Next(10) == 0 && player.whoAmI == Main.myPlayer)
                {
                    float RandomX = 50f;
                    float RandomY = 25f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(player.Center.X + player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    Spwanposition[1] = new Vector2(player.Center.X - player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    Spwanposition[2] = new Vector2(player.Center.X - player.direction * Main.rand.NextFloat(25f, RandomX), player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(Spwanposition[i].X, Spwanposition[i].Y, speedX, speedY, mod.ProjectileType("AssassinArrow"), (int)(item.damage * 1.3), 2f, player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(SpeedVector.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, mod.DustType("AcidDust"), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                    }
                }
            }
			return true;
		}

        public void AAHorizontalMovement()
        {
            float runSpeed = (player.accRunSpeed + player.maxRunSpeed) / 2f;
            if (player.controlLeft && player.velocity.X > -player.accRunSpeed && player.dashDelay >= 0)
            {
                if (player.velocity.X < -runSpeed && player.velocity.Y == 0f && !player.mount.Active)
                {
                    if (AADash == 1 && Main.rand.Next(50) == 0)
                    {
                        int dust = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y), player.width + 8, 4, ModContent.DustType<Feather>(), -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
            }
            else if (player.controlRight && player.velocity.X < player.accRunSpeed && player.dashDelay >= 0)
            {
                if (player.velocity.X > runSpeed && player.velocity.Y == 0f && !player.mount.Active)
                {
                    if (AADash == 1 && Main.rand.Next(50) == 0)
                    {
                        int dust = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y), player.width + 8, 4, ModContent.DustType<Feather>(), -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
            }
        }

        public void AADashMovement()
        {
            if (player.dashDelay > 0)
            {
                return;
            }
            if (player.dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.985f;
                float num9 = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float num10 = 0.94f;
                int num11 = 20;
                if (AADash == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int num12;
                        if (player.velocity.Y == 0f)
                        {
                            num12 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height - 4f), player.width, 8, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                        }
                        else
                        {
                            num12 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2 - 8f), player.width, 16, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                        }
                        Main.dust[num12].velocity *= 0.1f;
                        Main.dust[num12].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
                if (AADash > 0)
                {
                    player.vortexStealthActive = false;
                    if (player.velocity.X > num7 || player.velocity.X < -num7)
                    {
                        player.velocity.X = player.velocity.X * num8;
                        return;
                    }
                    if (player.velocity.X > num9 || player.velocity.X < -num9)
                    {
                        player.velocity.X = player.velocity.X * num10;
                        return;
                    }
                    player.dashDelay = num11;
                    if (player.velocity.X < 0f)
                    {
                        player.velocity.X = -num9;
                        return;
                    }
                    if (player.velocity.X > 0f)
                    {
                        player.velocity.X = num9;
                        return;
                    }
                }
            }
            else if (AADash > 0 && !player.mount.Active)
            {
                if (AADash == 1)
                {
                    int direction = 0;
                    bool DashAttempt = false;
                    if (AADashTime > 0)
                    {
                        AADashTime--;
                    }
                    if (AADashTime < 0)
                    {
                        AADashTime++;
                    }
                    if (player.controlRight && player.releaseRight && player.velocity.Y != 0)
                    {
                        if (AADashTime > 0)
                        {
                            direction = 1;
                            DashAttempt = true;
                            AADashTime = 0;
                        }
                        else
                        {
                            AADashTime = 15;
                        }
                    }
                    else if (player.controlLeft && player.releaseLeft && player.velocity.Y != 0)
                    {
                        if (AADashTime < 0)
                        {
                            direction = -1;
                            DashAttempt = true;
                            AADashTime = 0;
                        }
                        else
                        {
                            AADashTime = -15;
                        }
                    }
                    if (DashAttempt)
                    {
                        player.velocity.X = 14.5f * direction;
                        Point point = (player.Center + new Vector2(direction * player.width / 2 + 2, player.gravDir * -player.height / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (player.Center + new Vector2(direction * player.width / 2 + 2, 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        player.dashDelay = -1;
                        for (int num17 = 0; num17 < 2; num17++)
                        {
                            int num18 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                            Dust expr_CDB_cp_0 = Main.dust[num18];
                            expr_CDB_cp_0.position.X += Main.rand.Next(-5, 6);
                            Dust expr_D02_cp_0 = Main.dust[num18];
                            expr_D02_cp_0.position.Y += Main.rand.Next(-5, 6);
                            Main.dust[num18].velocity *= 0.2f;
                            Main.dust[num18].scale *= .1f + Main.rand.Next(20) * 0.01f;
                            Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                        }
                        return;
                    }
                }
            }
        }

        #region Dust Effects

        public static void EmitDust()
        {
            if (Main.gamePaused)
            {
                return;
            }

            int sandTiles = Main.sandTiles;
            Player player = Main.LocalPlayer;
            bool flag = Sandstorm.Happening && player.ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
            Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);

            if (sandTiles < 100 || player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
            {
                return;
            }

            if (!flag)
            {
                return;
            }

            int maxValue = 1;
            if (Main.rand.Next(maxValue) != 0)
            {
                return;
            }

            int num = Math.Sign(Main.windSpeed);
            float num2 = Math.Abs(Main.windSpeed);
            if (num2 < 0.01f)
            {
                return;
            }

            float num3 = num * MathHelper.Lerp(0.9f, 1f, num2);
            float num4 = 2000f / sandTiles;
            float num5 = 3f / num4;
            num5 = MathHelper.Clamp(num5, 0.77f, 1f);
            int num6 = (int)num4;
            float num7 = Main.screenWidth / (float)Main.maxScreenW;
            int num8 = (int)(1000f * num7);
            float num9 = 20f * Sandstorm.Severity;
            float num10 = num8 * (Main.gfxQuality * 0.5f + 0.5f) + num8 * 0.1f - Dust.SandStormCount;
            if (num10 <= 0f)
            {
                return;
            }

            float num11 = Main.screenWidth + 1000f;
            float num12 = Main.screenHeight;
            Vector2 value = Main.screenPosition + player.velocity;

            WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
            weightedRandom.Add(new Color(200, 160, 20, 180), Main.screenTileCounts[53] + Main.screenTileCounts[396] + Main.screenTileCounts[397]);
            weightedRandom.Add(new Color(103, 98, 122, 180), Main.screenTileCounts[112] + Main.screenTileCounts[400] + Main.screenTileCounts[398]);
            weightedRandom.Add(new Color(135, 43, 34, 180), Main.screenTileCounts[234] + Main.screenTileCounts[401] + Main.screenTileCounts[399]);
            weightedRandom.Add(new Color(213, 196, 197, 180), Main.screenTileCounts[116] + Main.screenTileCounts[403] + Main.screenTileCounts[402]);

            float num13 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
            float num14 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
            int num15 = 0;

            while (num15 < num9)
            {
                if (Main.rand.Next(num6 / 4) == 0)
                {
                    Vector2 vector = new Vector2(Main.rand.NextFloat() * num11 - 500f, Main.rand.NextFloat() * -50f);

                    if (Main.rand.Next(3) == 0 && num == 1)
                    {
                        vector.X = Main.rand.Next(500) - 500;
                    }
                    else if (Main.rand.Next(3) == 0 && num == -1)
                    {
                        vector.X = Main.rand.Next(500) + Main.screenWidth;
                    }

                    if (vector.X < 0f || vector.X > Main.screenWidth)
                    {
                        vector.Y += Main.rand.NextFloat() * num12 * 0.9f;
                    }

                    vector += value;

                    int num16 = (int)vector.X / 16;
                    int num17 = (int)vector.Y / 16;

                    if (Main.tile[num16, num17] != null && Main.tile[num16, num17].wall == 0)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, 268, 0f, 0f, 0)];
                            dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;

                            Dust expr_460_cp_0 = dust;
                            expr_460_cp_0.velocity.Y *= dust.scale;

                            Dust expr_47A_cp_0 = dust;
                            expr_47A_cp_0.velocity.Y *= 0.35f;

                            dust.velocity.X = num3 * 5f + Main.rand.NextFloat() * 1f;

                            Dust expr_4B7_cp_0 = dust;
                            expr_4B7_cp_0.velocity.X += num3 * num14 * 20f;

                            dust.fadeIn += num14 * 0.2f;
                            dust.velocity *= 1f + num13 * 0.5f;
                            dust.color = weightedRandom;
                            dust.velocity *= 1f + num13;
                            dust.velocity *= num5;
                            dust.scale = 0.9f;

                            num10 -= 1f;
                            if (num10 <= 0f)
                            {
                                break;
                            }
                        }

                        if (num10 <= 0f)
                        {
                            return;
                        }
                    }
                }

                num15++;
            }
        }

        public static void AshRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }

            if ((player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda) && player.GetModPlayer<AAPlayer>().AshCurse)
            {
                if (!player.GetModPlayer<AAPlayer>().AshRemover || !(player.ZoneSkyHeight || player.ZoneOverworldHeight))
                {
                    player.AddBuff(ModContent.BuffType<BurningAsh>(), 5);
                }

                if (AAWorld.infernoTiles > 0 && Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
                {
                    int maxValue = 800 / AAWorld.infernoTiles;
                    float num = Main.screenWidth / (float)Main.maxScreenW;
                    int num2 = (int)(500f * num);
                    num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                    float num3 = 1f + 50f * Main.cloudAlpha;
                    int num4 = 0;

                    while (num4 < num3)
                    {
                        try
                        {
                            if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                            {
                                break;
                            }

                            if (Main.rand.Next(maxValue) == 0)
                            {
                                int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                                int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                                if (Main.LocalPlayer.velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.LocalPlayer.velocity.Y;
                                }

                                if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) - 500;
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) + Main.screenWidth;
                                }

                                if (num5 < 0 || num5 > Main.screenWidth)
                                {
                                    num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                                }

                                num5 += (int)Main.screenPosition.X;

                                int num7 = num5 / 16;
                                int num8 = num6 / 16;

                                if (Main.tile[num7, num8] != null && Main.tile[num7, num8].wall == 0)
                                {
                                    int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Dusts.AshRain>(), 0f, 0f, 0);
                                    Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                                    Dust expr_292_cp_0 = Main.dust[dust];
                                    expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                                    if (!player.GetModPlayer<AAPlayer>().AshCurse)
                                    {
                                        Main.dust[dust].velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                        Dust expr_2EC_cp_0 = Main.dust[dust];
                                        expr_2EC_cp_0.velocity.X += Main.windSpeed * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                                        Dust expr_370_cp_0 = Main.dust[dust];
                                        expr_370_cp_0.velocity.Y *= 0.5f;
                                    }

                                    Dust expr_38E_cp_0 = Main.dust[dust];
                                    expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                                    Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                        catch
                        {
                        }

                        num4++;
                    }
                }
            }
        }

        public static void EmberRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }

            if ((player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake) && AAWorld.downedAllAncients && !AAWorld.downedShen)
            {
                if (Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
                {
                    int maxValue = 8;
                    float num = Main.screenWidth / (float)Main.maxScreenW;
                    int num2 = (int)(500f * num);
                    num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                    float num3 = 1f + 50f * Main.cloudAlpha;
                    int num4 = 0;

                    while (num4 < num3)
                    {
                        try
                        {
                            if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                            {
                                break;
                            }

                            if (Main.rand.Next(maxValue) == 0)
                            {
                                int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                                int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                                if (Main.LocalPlayer.velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.LocalPlayer.velocity.Y;
                                }

                                if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) - 500;
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) + Main.screenWidth;
                                }

                                if (num5 < 0 || num5 > Main.screenWidth)
                                {
                                    num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                                }

                                num5 += (int)Main.screenPosition.X;

                                int num7 = num5 / 16;
                                int num8 = num6 / 16;

                                if (Main.tile[num7, num8] != null && Main.tile[num7, num8].wall == 0)
                                {
                                    int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Dusts.Discord>(), 0f, 0f, 0);
                                    Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                                    Dust expr_292_cp_0 = Main.dust[dust];
                                    expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                                    if (!player.GetModPlayer<AAPlayer>().AshCurse)
                                    {
                                        Main.dust[dust].velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                        Dust expr_2EC_cp_0 = Main.dust[dust];
                                        expr_2EC_cp_0.velocity.X += Main.windSpeed * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                                        Dust expr_370_cp_0 = Main.dust[dust];
                                        expr_370_cp_0.velocity.Y *= 0.5f;
                                    }

                                    Dust expr_38E_cp_0 = Main.dust[dust];
                                    expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                                    Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                        catch
                        {
                        }

                        num4++;
                    }
                }
            }
        }

        #endregion

        #region Dev Armor

        public void DropDevArmor(int dropType)
        {
            //0 = Pre-HM
            //1 = HM
            //2 = Post-Plant
            //3 = PML
            //4 = PA
            string addonEX = dropType == 4 ? "EX" : ""; //only include EX if it's a dropType 3 (ie from ancients)

            bool spawnedDevItems = false; //this prevents it from not dropping anything if the chance lands on something it cannot drop yet (for prehm/hm) as by this point it's past the 10% chance and thus should drop.
            while (!spawnedDevItems)
            {
                int choice = Main.rand.Next(40);

                switch (choice)
                {
                    case 0:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Hallam.MagiciansHat>());

                        if (dropType >= 4)
                        {
                            player.QuickSpawnItem(mod.ItemType("Prismeow" + addonEX));
                        }

                        spawnedDevItems = true;
                        break;

                    case 1:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Alphakip.AlphaBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("AmphibianLongsword" + addonEX));
                        }

                        if (dropType >= 4)
                        {
                            player.QuickSpawnItem(mod.ItemType("AlphakipTerratool"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 2:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Beg.BegBag>());

                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("MonochromeApple"));
                        }

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("PoniumStaff" + addonEX));
                        }

                        spawnedDevItems = true;
                        break;

                    case 3:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Maskano.MaskBag>());

                        spawnedDevItems = true;
                        break;

                    case 4:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Charlie.CharlieBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("SoulSiphon"));
                        }
                        break;

                    case 5:
                        player.QuickSpawnItem(mod.ItemType("TailsHead"));
                        player.QuickSpawnItem(mod.ItemType("TailsBody"));
                        player.QuickSpawnItem(mod.ItemType("TailsLegs"));

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 4 ? "FreedomStar" : "MobianBuster"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 6:
                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("SkrallStaff"));
                            spawnedDevItems = true;
                        }

                        break;

                    case 7:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Delly.DellyBag>());

                        break;

                    case 8:
                        player.QuickSpawnItem(mod.ItemType("FezLordsBag"));

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 4 ? "Chronos" : "TimeTeller"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 9:
                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("TitanAxe" + addonEX));
                            spawnedDevItems = true;
                        }

                        break;

                    case 10:
                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("EnderStaff" + addonEX));
                            spawnedDevItems = true;
                        }

                        break;

                    case 11:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Eliza.LizBag>());

                        if (dropType >= 3)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                player.QuickSpawnItem(mod.ItemType("CatsEyeRifle" + addonEX));
                            }
                            else
                            {
                                player.QuickSpawnItem(mod.ItemType(dropType == 4 ? "ArchwitchStaff" : "ArchwitchWand"));
                            }

                            if (dropType >= 4)
                            {
                                player.QuickSpawnItem(mod.ItemType("LizTerratool"));
                            }
                        }

                        spawnedDevItems = true;
                        break;

                    case 12:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Aves.AvesBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("DuckstepGun" + addonEX));
                        }

                        spawnedDevItems = true;
                        break;

                    case 13:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Tied.OldMagiciansHat>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 4 ? "GentlemansLongblade" : "GentlemansRapier"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 14:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Moon.MoonBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("Etheral" + addonEX));
                        }

                        spawnedDevItems = true;
                        break;

                    case 15:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Grox.GroviteSeaChest>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 4 ? "SoccStaff" : "SockStaff"));
                        }

                        if (dropType >= 4)
                        {
                            player.QuickSpawnItem(mod.ItemType("GroviteTerratool"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 16:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.CC.CCBox>());

                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("CCRuneBookPage"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 17:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Gibs.GibsBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(Main.rand.Next(2) == 0 ? mod.ItemType("Skullshot") : mod.ItemType("GibsFemur"));
                        }

                        spawnedDevItems = true;
                        break;

                    case 18:
                        player.QuickSpawnItem(mod.ItemType("ApawnEgg"));
                        spawnedDevItems = true;
                        break;

                    case 19:
                        player.QuickSpawnItem(mod.ItemType("UniBag"));

                        if (dropType >= 3)
                        {
                            //player.QuickSpawnItem(mod.ItemType("CursedSickle" + addonEX));
                        }

                        spawnedDevItems = true;
                        break;

                    case 20:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Mikpin.MikBag>());

                        spawnedDevItems = true;
                        break;

                    case 21:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Fargo.TopHat>());

                        if (dropType >= 3)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                player.QuickSpawnItem(mod.ItemType("MagicAcorn" + addonEX));
                            }
                            else
                            {
                                player.QuickSpawnItem(mod.ItemType("Placeholder"));
                            }
                        }

                        spawnedDevItems = true;
                        break;

                    case 22:

                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Blazen.BlazenBag>());

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("ThunderLord" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;

                    case 23:
                        player.QuickSpawnItem(ItemID.ReaperHood);
                        player.QuickSpawnItem(ItemID.ReaperRobe);

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("GrimReaperScythe" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;

                    case 24:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("UmbralReaper"));
                        }
                        spawnedDevItems = true;
                        break;

                    case 25:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("FuryForger" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;

                    case 26:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Cerberus.InvokerBag>());
                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("InvokerStaff"));
                        }
                        spawnedDevItems = true;
                        break;

                    case 27:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("GameRaider"));
                        }
                        spawnedDevItems = true;
                        break;
                    case 28:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Pluto.PlutoBag>());
                        break;
                    case 29:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.VoidEye.VoidBag>());
                        break;
                    case 30:
                        player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Anarchy.AnarchyBag>());
                        break;
                    case 31:
                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("UmbreonSP" + addonEX));
                        }
                        break;
                    case 32:
                            player.QuickSpawnItem(ModContent.ItemType<Items.Vanity.Shox.ShoxBag>());
                        break;
                    case 33:

                        if (dropType >= 3)
                        {
                            player.QuickSpawnItem(mod.ItemType("CerlaStaff" + addonEX));
                        }

                        break;

                    default:
                        spawnedDevItems = false;
                        break;
                }
            }
        }

        public void PHMDevArmor()
        {
            DropDevArmor(0);
        }

        public void HMDevArmor()
        {
            DropDevArmor(1);
        }

        public void PPDevArmor()
        {
            DropDevArmor(2);
        }

        public void PMLDevArmor()
        {
            DropDevArmor(3);
        }

        public void SADevArmor()
        {
            DropDevArmor(4);
        }

        #endregion

        public override void PreUpdate()
        {
            groviteGlow[player.whoAmI] = false;

            if (player.GetModPlayer<AAPlayer>().ZoneVoid || player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
            }

            if (player.GetModPlayer<AAPlayer>().ZoneMire || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.raining)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        Main.rainTime++;
                    }
                }
            }
        }

        public override void GetWeaponKnockback(Item item, ref float knockback)
        {
            if (demonGauntlet)
            {
                if (item.melee)
                {
                    knockback += 2f;
                }
            }

            if (IsGoblin)
            {
                knockback += 5f;
            }
        }
 
        public override float UseTimeMultiplier(Item item)
        {
            float multiplier = 1f;

            if (item.damage > 0)
            {
                if (HydraPendant)
                {
                    multiplier *= 1.15f;
                }

                while (item.useTime / multiplier < 1)
                {
                    multiplier -= .1f;
                }

                while (item.useAnimation / multiplier < 2)
                {
                    multiplier -= .1f;
                }
            }

            return multiplier;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AAMod.Rift.JustPressed)
            {
                RiftPos = player.position;
                for (int m = 0; m < 58; m++)
                {
                    if (player.inventory[m].type == ModContent.ItemType<Items.Usable.RiftMirror>())
                    {
                        Main.PlaySound(new LegacySoundStyle(2, 6), player.position);
                        player.Spawn();
                    }
                }
            }

            if (AAMod.RiftReturn.JustPressed && RiftPos != new Vector2(0, 0))
            {
                for (int m = 0; m < 58; m++)
                {
                    if (player.inventory[m].type == ModContent.ItemType<Items.Usable.RiftMirror>())
                    {
                        Main.PlaySound(new LegacySoundStyle(2, 6), player.position);
                        LeaveDust(player);
                        player.velocity = Vector2.Zero;
                        player.position = RiftPos;
                    }
                }
            }

            if (SagShield)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed && SagCooldown == 0)
                {
                    player.AddBuff(ModContent.BuffType<SagShield>(), 300);
                    SagCooldown = 5400;
                }
            }

            if (Witch)
            {
                if (AAMod.ArmorAbilityKey.JustPressed && AsheCooldown == 0)
                {
                    Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 104, 1f, 0f);
                    if(player.inventory[player.selectedItem].magic || player.inventory[player.selectedItem].summon)
                    {
                        for(int i = 0; i < 8; i++)
                        {
                            Vector2 shoot = new Vector2((float)Math.Sin(i * 0.25f * 3.1415926f), (float)Math.Cos(i * 0.25f * 3.1415926f));
                            shoot *= 8f;
                            int id = Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X, shoot.Y, mod.ProjectileType("AsheFire"), player.inventory[player.selectedItem].damage, 5, Main.myPlayer, 0f, 1f);
                            Main.projectile[id].magic = true;
                            Main.projectile[id].hostile = false;
                            Main.projectile[id].friendly = true;
                        }
                    }
                    player.AddBuff(ModContent.BuffType<AsheFlame>(), 900);
                    AsheCooldown = 5400;
                }
            }

            if (OldOneCharm)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed && DD2Event.Ongoing && DD2Event.TimeLeftBetweenWaves > 0)
                {
                    DD2Event.TimeLeftBetweenWaves = 60;
                    if (Main.netMode != 0)
                    {
                        AANet.SendNetMessage(AANet.DD2EventTime, (byte)DD2Event.TimeLeftBetweenWaves);
                    }
                }
            }

            if (ChaosRa2)
            {
                if (AAMod.ArmorAbilityKey.JustPressed && AbilityCD == 0)
                {
                    AbilityCD = 180;

                    int damage = 70;
                    float knockback = 1;

                    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                    float speedX = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float speedY = Main.mouseY + Main.screenPosition.Y - vector2.Y;

                    if (player.gravDir == -1f)
                    {
                        speedY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                    }

                    if ((float.IsNaN(speedX) && float.IsNaN(speedY)) || (speedX == 0f && speedY == 0f))
                    {
                        speedX = player.direction;
                        speedY = 0f;
                    }

                    vector2.X = Main.mouseX + Main.screenPosition.X;
                    vector2.Y = Main.mouseY + Main.screenPosition.Y;

                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("DragonShot"), damage, knockback, Main.myPlayer, 0f, 0f);
                }
            }

            if (AbilityCD != 0)
            {
                AbilityCD--;
            }
        }

        private static void LeaveDust(Player player)
        {
            for (int index = 0; index < 70; ++index)
                Main.dust[Dust.NewDust(player.position, player.width, player.height, 15, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
            Main.TeleportEffect(player.getRect(), 1);
            Main.TeleportEffect(player.getRect(), 3);
        }

        public bool ShinyCheck()
        {
            for (int i = 0; i < 58; i++)
            {
                Item item = player.inventory[i];
                if (item.type == mod.ItemType("ShinyCharm"))
                {
                    if (Main.rand.Next(2048) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            if (Main.rand.Next(4096) == 0)
                return true;

            return false;
        }
        public int IZHoldTimer = 180;
        public bool InfZ = false;
        public int GetIZHealth = 2500000;
        public int EscapeLine = 180;
        public int RiftTimer;
        public int RiftDamage = 10;

        public int AARegenCount = 0;

        public override void UpdateLifeRegen()
        {
            if (SagShield)
            {
                if (player.lifeRegen < 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen += 2;
            }

            if (TerraMe)
            {
                AARegenCount++;
                while (AARegenCount >= 100)
                {
                    AARegenCount -= 100;
                    if (player.statLife < player.statLifeMax2)
                    {
                        player.statLife += 2;
                        for (int i = 0; i < 10; i++)
                        {
                            int num6 = Dust.NewDust(player.position, player.width, player.height, 107, 0f, 0f, 175, default, 1.75f);
                            Main.dust[num6].noGravity = true;
                            Main.dust[num6].velocity *= 0.75f;
                            int num7 = Main.rand.Next(-40, 41);
                            int num8 = Main.rand.Next(-40, 41);
                            Dust expr_7EE_cp_0 = Main.dust[num6];
                            expr_7EE_cp_0.position.X = expr_7EE_cp_0.position.X + num7;
                            Dust expr_80A_cp_0 = Main.dust[num6];
                            expr_80A_cp_0.position.Y = expr_80A_cp_0.position.Y + num8;
                            Main.dust[num6].velocity.X = -num7 * 0.075f;
                            Main.dust[num6].velocity.Y = -num8 * 0.075f;
                        }
                    }
                }
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Spear)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 2;
            }

            if (infinityOverload)
            {
                player.lifeRegen -= 60;
            }

            if (YamataGravity || YamataAGravity)
            {
                if (player.mount.CanFly)
                {
                    player.mount.Dismount(player);
                }

                player.wingTimeMax /= 2;
                if (player.wingTime > player.wingTimeMax)
                    player.wingTimeMax = player.wingTimeMax;

                if (YamataAGravity)
                {
                    player.moveSpeed *= .58f;
                }
            }

            if (FFlames)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 40 * (player.statLife / player.statLifeMax2);
            }


            if (CursedHellfire)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 30;
            }

            if (shroomed && player.velocity.Y == 0)
            {
                player.velocity.X *= .8f;
            }

            if (Hunted)
            {
                if (player.rocketTimeMax > 30)
                {
                    player.wingTimeMax = 30;
                }

                if (player.accRunSpeed > 3f)
                {
                    player.accRunSpeed = 3f;
                }

                player.wingTimeMax /= 2;
                if (player.wingTimeMax <= 0)
                {
                    player.wingTimeMax = 0;
                }
            }

            if (terraBlaze)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 16;
            }

            if (dragonFire)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= 8;
                
            }

            if (riftbent)
            {
                RiftTimer++;
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;

                if (RiftTimer >= 120)
                {
                    RiftDamage += 10;
                    RiftTimer = 0;
                }

                if (RiftDamage >= 80)
                {
                    RiftDamage = 80;
                }

                player.lifeRegen -= RiftDamage;
            }
            else
            {
                RiftDamage = 10;
                RiftTimer = 0;
            }

            if (hydraToxin)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegen -= Math.Abs((int)player.velocity.X);
            }


            if (discordInferno)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                player.lifeRegen -= Math.Abs((int)player.velocity.X) + 4;
                player.allDamage *= 0.8f;
            }

            if (AkumaPain)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;

                if ((player.onFire || player.frostBurn || player.onFire2 || dragonFire || discordInferno) && player.lifeRegen < 0)
                {
                    player.lifeRegen *= 2;
                }
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
            if (bossactive)
            {
                nohitplayer = false;
            }
            if (ShieldUp)
			{
				return false;
			}
            if (Ronin)
            {
                return false;
            }
            if (AncientGoldSet)
            {
                long num = 0;
                for (int i = 0; i < 54; i++)
                {
                    if (player.inventory[i].type == 71)
                    {
                        num += player.inventory[i].stack;
                    }
                    if (player.inventory[i].type == 72)
                    {
                        num += player.inventory[i].stack * 100;
                    }
                    if (player.inventory[i].type == 73)
                    {
                        num += player.inventory[i].stack * 10000;
                    }
                    if (player.inventory[i].type == 74)
                    {
                        num += player.inventory[i].stack * 1000000;
                    }
                }
                if(num >= damage * 10000)
                {
                    for (int i = 0; i < 54; i++)
                    {
                        if (player.inventory[i].type == 71)
                        {
                            player.inventory[i].stack = 0;
                            player.inventory[i].TurnToAir();
                        }
                        if (player.inventory[i].type == 72)
                        {
                            player.inventory[i].stack = 0;
                            player.inventory[i].TurnToAir();
                        }
                        if (player.inventory[i].type == 73)
                        {
                            player.inventory[i].stack = 0;
                            player.inventory[i].TurnToAir();
                        }
                        if (player.inventory[i].type == 74)
                        {
                            player.inventory[i].stack = 0;
                            player.inventory[i].TurnToAir();
                        }
                    }
                    damage = 0;
                    return false;
                }
            }
            return true;
        }

        public override void UpdateDead()
        {
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Yanked = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            Spear = false;
            MaxMovespeedboost = 0;
            spellbookDamage = 1f;
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (demonGauntlet)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    int ThisDust = 170;
                    if (!WorldGen.crimson)
                    {
                        ThisDust = 75;
                    }

                    Dust dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ThisDust, 0f, 0f, 46)];
                    dust.noGravity = true;
                }
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (FFlames)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("ForsakenDust"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 1.5f);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                    r *= 0.1f;
                    g *= 0.7f;
                    b *= 0.1f;
                }
            }

            if (infinityOverload)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadB"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.3f;
                b *= 0.7f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadR"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.7f;
                g *= 0.2f;
                b *= 0.2f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadG"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.1f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadY"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.5f;
                g *= 0.5f;
                b *= 0.1f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadP"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.6f;
                g *= 0.1f;
                b *= 0.6f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadO"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.8f;
                g *= 0.5f;
                b *= 0.1f;

                fullBright = true;
            }

            if (terraBlaze)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 107, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.2f;

                fullBright = true;
            }

            if (CursedHellfire)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 75, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].scale = 3f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    Main.playerDrawDust.Add(dust);
                }

                fullBright = true;
            }

            if (discordInferno)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, ModContent.DustType<Dusts.Discord>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }
            }

            if (shroomed)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, ModContent.DustType<Dusts.ShroomDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0f, 0f, 0.45f);
            }

            if (riftbent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0f, 0f, 0.45f);
            }
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (ammo20percentdown && Main.rand.Next(5) == 0)
            {
                return false;
            }

            return base.ConsumeAmmo(weapon, ammo);
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneMire || ZoneRisingMoonLake)
            {
                return mod.GetTexture("Map/MireMap");
            }
            else if (ZoneInferno || ZoneRisingSunPagoda)
            {
                return mod.GetTexture("Map/InfernoMap");
            }
            else if (ZoneVoid)
            {
                return mod.GetTexture("Map/VoidMap");
            }

            return null;
        }

        #region Highest Damage check

        public bool MeleeHighest(Player player)
        {
            return player.meleeDamage > player.rangedDamage &&
                player.meleeDamage > player.magicDamage &&
                player.meleeDamage > player.minionDamage &&
                player.meleeDamage > player.thrownDamage;
        }

        public bool RangedHighest(Player player)
        {
            return player.rangedDamage > player.meleeDamage &&
                player.rangedDamage > player.magicDamage &&
                player.rangedDamage > player.minionDamage &&
                player.rangedDamage > player.thrownDamage;
        }

        public bool MagicHighest(Player player)
        {
            return player.magicDamage > player.rangedDamage &&
                player.magicDamage > player.meleeDamage &&
                player.magicDamage > player.minionDamage &&
                player.magicDamage > player.thrownDamage;
        }

        public bool SummonHighest(Player player)
        {
            return player.minionDamage > player.rangedDamage &&
                player.minionDamage > player.magicDamage &&
                player.minionDamage > player.meleeDamage &&
                player.minionDamage > player.thrownDamage;
        }

        public bool ThrownHighest(Player player)
        {
            return player.thrownDamage > player.rangedDamage &&
                player.thrownDamage > player.magicDamage &&
                player.thrownDamage > player.minionDamage &&
                player.thrownDamage > player.meleeDamage;
        }

        #endregion


        public override void FrameEffects()
        {
            if (onoForceVanity && !onoHideVanity)
            {
                player.legs = mod.GetEquipSlot("onoLeg", EquipType.Legs);
                player.body = mod.GetEquipSlot("onoBody", EquipType.Body);
                player.head = mod.GetEquipSlot("onoHead", EquipType.Head);
            }
        }

        public override void UpdateVanityAccessories()
        {
            for (int n = 10; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == mod.ItemType("StripeManShirt"))
                {
                    player.accWatch = 3;
                    player.accDepthMeter = 1;
                    player.accCompass = 1;
                    player.accFishFinder = true;
                    player.accWeatherRadio = true;
                    player.accCalendar = true;
                    player.accThirdEye = true;
                    player.accJarOfSouls = true;
                    player.accCritterGuide = true;
                    player.accStopwatch = true;
                    player.accOreFinder = true;
                    player.accDreamCatcher = true;
                }
                if (item.type == ModContent.ItemType<Items.Vanity.Ohno.ono>())
                {
                    onoHideVanity = false;
                    onoForceVanity = true;
                }
                if (item.type == mod.ItemType("Equinox"))
                {
                    player.hideWolf = false;
                    player.forceWerewolf = true;
                    if ((player.wet && !player.lavaWet && (!player.mount.Active || player.mount.Type != 3)) || !player.forceWerewolf)
                    {
                        player.hideMerman = false;
                        player.forceMerman = true;
                    }
                }
            }
        }
    }

    public class MimicSummon : ModPlayer
    {
        int LastChest = 0;

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
                {
                    int x2 = Main.chest[LastChest].x;
                    int y2 = Main.chest[LastChest].y;
                    ChestItemSummonCheck(x2, y2, mod);
                }
                LastChest = player.chest;
            }
        }

        public override void UpdateAutopause()
        {
            LastChest = player.chest;
        }

        public static void ChestItemSummonCheck(int x, int y, Mod mod)
        {
            if (!Main.hardMode || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
            {
                return;
            }

            ushort tileType = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].type;
            int tileStyle = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].frameX / 36;

            if (!TileID.Sets.BasicChest[tileType] || tileStyle == 5 || tileStyle == 6)
            {
                return;
            }

            bool hasInfernoKey = false;
            bool hasItems = false;

            for (int i = 0; i < 40; i++)
            {
                if (Main.chest[chestIndex].item[i] == null || Main.chest[chestIndex].item[i].type <= 0)
                {
                    continue;
                }

                if (hasItems || Main.chest[chestIndex].item[i].stack != 1)
                {
                    return;
                }

                hasItems = true;

                if (Main.chest[chestIndex].item[i].type == mod.ItemType("KeyOfSmite"))
                {
                    hasInfernoKey = true;
                }
                else if (Main.chest[chestIndex].item[i].type != mod.ItemType("KeyOfSpite"))
                {
                    return;
                }
            }

            if (!hasItems)
            {
                return;
            }

            for (int j = x; j <= x + 1; j++)
            {
                for (int k = y; k <= y + 1; k++)
                {
                    if (TileID.Sets.BasicChest[Main.tile[j, k].type])
                    {
                        Main.tile[j, k].active(false);
                    }
                }
            }

            for (int l = 0; l < 40; l++)
            {
                Main.chest[chestIndex].item[l] = new Item();
            }

            Chest.DestroyChest(x, y);
            NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y, 0f, chestIndex);
            NetMessage.SendTileSquare(-1, x, y, 3);

            int npcToSpawn = mod.NPCType("MireMimic");
            if (hasInfernoKey)
            {
                npcToSpawn = mod.NPCType("InfernoMimic");
            }

            int npcIndex = NPC.NewNPC(x * 16 + 16, y * 16 + 32, npcToSpawn);
            Main.npc[npcIndex].whoAmI = npcIndex;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
            Main.npc[npcIndex].BigMimicSpawnSmoke();
        }
    }

    public partial class AAPlayer : ModPlayer
    {
        public override void ModifyDrawLayers(List<PlayerLayer> list)
        {
            AddPlayerLayer(list, glAfterHead, PlayerLayer.Head);
            AddPlayerLayer(list, glAfterBody, PlayerLayer.Body);
            AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms);
            AddPlayerLayer(list, glAfterHandOn, PlayerLayer.HandOnAcc);
            AddPlayerLayer(list, glAfterHandOff, PlayerLayer.HandOffAcc);
            AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms);
            AddPlayerLayer(list, glAfterWep, PlayerLayer.HeldItem);
            AddPlayerLayer(list, glAfterLegs, PlayerLayer.Legs);
            AddPlayerLayer(list, glAfterShield, PlayerLayer.ShieldAcc);
            AddPlayerLayer(list, glAfterNeck, PlayerLayer.NeckAcc);
            AddPlayerLayer(list, glAfterFace, PlayerLayer.FaceAcc);

            if (!player.merman && !player.wereWolf && groviteGlow[player.whoAmI])
            {
                BaseDrawing.AddPlayerLayer(list, glGroviteHead, PlayerLayer.Head, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteBody, PlayerLayer.Body, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteLegs, PlayerLayer.Legs, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteArm, PlayerLayer.Arms, false);
                BaseDrawing.AddPlayerLayer(list, glGroviteWings, PlayerLayer.Wings, false);
            }

            AddPlayerLayer(list, glAfterAll, list[list.Count - 1]);
        }

        public static void AddPlayerLayer(List<PlayerLayer> layers, PlayerLayer layer, PlayerLayer parent)
        {
            int index = layers.IndexOf(parent);
            if (index != -1)
            {
                layers.Insert(index + 1, layer);
            }
        }

        public PlayerLayer glAfterWep = new PlayerLayer("AAMod", "glAfterWep", PlayerLayer.HeldItem, delegate (PlayerDrawInfo edi)
        {
            if (edi.shadow != 0)
            {
                return;
            }

            Player drawPlayer = edi.drawPlayer;
            Item heldItem = drawPlayer.inventory[drawPlayer.selectedItem];
            BaseAAItem baseAAItem = null;

            if (heldItem.modItem != null && heldItem.modItem is BaseAAItem)
            {
                baseAAItem = (BaseAAItem)heldItem.modItem;
            }

            if (baseAAItem != null && baseAAItem.glowmaskTexture != null && baseAAItem.glowmaskDrawType != BaseAAItem.GLOWMASKTYPE_NONE)
            {
                Vector2? offsetNull = baseAAItem.HoldoutOffset();
                Vector2 offset = (offsetNull != null) ? (Vector2)offsetNull : Vector2.Zero;

                if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_SWORD)
                {
                    BaseDrawing.DrawHeldSword(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, null, 1, AAMod.instance.GetTexture(baseAAItem.glowmaskTexture));
                }
                else if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_GUN)
                {
                    BaseDrawing.DrawHeldGun(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, false, false, 0f, 0f, null, 1, AAMod.instance.GetTexture(baseAAItem.glowmaskTexture));
                }
            }
        });

        public PlayerLayer glAfterHead = new PlayerLayer("AAMod", "glAfterHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

            Vector2 position = edi.position;
            int dyeHead = edi.headArmorShader;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayHelmet_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterVisor")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHelm_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHelmet_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHeaddress")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterHeaddress_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHat")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHat_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelm")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHelm_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHelmet_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumHeadgear_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumMask_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("GripMaskRed")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/GripMaskRed_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DaybringerMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DaybringerMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("NightcrawlerMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/NightcrawlerMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("RetrieverMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/RetrieverMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ZeroMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ZeroMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TiedMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TiedMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.FlashGlow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("LizEars")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/LizEars_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomHat")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomHat_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DJDuckHead")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DJDuckHead_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteVisor")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosKabuto")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosKabutoBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosKabuto_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosMask")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosMaskBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosHood")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosHoodBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosVisor")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosVisorBlue_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosVisor_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenHelmet_Head"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("GibsSkull")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/GibsSkull_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedHood")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HoodlumHood")) && drawPlayer.statLife < (drawPlayer.statLifeMax2 / 2))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HoodlumHood_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("AthenaAMask")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/AthenaAMask_Head_Glow"), dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Flash, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterShield = new PlayerLayer("AAMod", "glAfterShield", PlayerLayer.ShieldAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("TaiyangBaolei")))
            {
                string texturePath = Main.dayTime ? "Glowmasks/TaiyangBaoleiA_Shield_Glow" : "Glowmasks/TaiyangBaolei_Shield_Glow";
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture(texturePath), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterFace = new PlayerLayer("AAMod", "glAfterFace", PlayerLayer.FaceAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("SoulStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SoulStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("SpaceStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SpaceStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("RealityStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/RealityStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("TimeStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/TimeStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("PowerStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/PowerStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("MindStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/MindStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame);
            }
        });

        public PlayerLayer glAfterNeck = new PlayerLayer("AAMod", "glAfterNeck", PlayerLayer.NeckAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("Naitokurosu")))
            {
                string texturePath = Main.dayTime ? "Glowmasks/Naitokurosu_Neck_Glow" : "Glowmasks/NaitokurosuA_Neck_Glow";
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture(texturePath), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterHandOn = new PlayerLayer("AAMod", "glAfterHandOn", PlayerLayer.HandOnAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DemonGauntlet")))
            {
                Texture2D Glow = mod.GetTexture("Glowmasks/DemonGauntlet_HandsOn_Glow");
                Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, Glow, edi.handOnShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterHandOff = new PlayerLayer("AAMod", "glAfterHandOff", PlayerLayer.HandOffAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DemonGauntlet")))
            {
                Texture2D Glow = mod.GetTexture("Glowmasks/DemonGauntlet_HandsOff_Glow");
                Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, Glow, edi.handOffShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterBody = new PlayerLayer("AAMod", "glAfterBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumPlatemail")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DJDuckShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DJDuckShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteBreastplate")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteBreastplate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_" + (drawPlayer.Male ? "Body" : "FemaleBody")), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenPlate_" + (drawPlayer.Male ? "Body" : "Female")), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedRobe")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedRobe_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterArm = new PlayerLayer("AAMod", "glAfterArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumPlatemail")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomShirt_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteBreastplate")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.direction == 1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenPlate_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedRobe")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedRobe_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public PlayerLayer glAfterLegs = new PlayerLayer("AAMod", "glAfterLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterGreaves")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumCuisses")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumCuisses_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomPants")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomPants_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteGreaves")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("BlazenBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/BlazenBoots_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("CursedPants")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/CursedPants_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, edi.shadow), drawPlayer.legFrame);
            }
            /*else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShoxCurse")))
            {
                if (!drawPlayer.invis && !drawPlayer.mount.Active)
                {
                    Color color14 = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)(edi.position.X + drawPlayer.width * 0.5) / 16, (int)(edi.position.Y + drawPlayer.height * 0.75) / 16, Color.White), 0f);
                    Texture2D texture2 = mod.GetTexture("Items/Vanity/Shox/ShoxCurse_Pants");
                    bool flag10 = drawPlayer.legFrame.Y == 0;
                    int num65 = drawPlayer.miscCounter / 3 % 8;
                    if (flag10)
                    {
                        num65 = drawPlayer.miscCounter / 4 % 8;
                    }
                    Rectangle rectangle3 = new Rectangle(18 * flag10.ToInt(), num65 * 26, 16, 24);
                    float num66 = 12f - Main.OffsetsPlayerHeadgear[drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height].Y;
                    Vector2 arg_6147_0 = edi.position + drawPlayer.Size * new Vector2(0.5f, 0.5f + 0.5f * drawPlayer.gravDir);
                    int arg_6135_0 = drawPlayer.direction;
                    Vector2 vector7 = arg_6147_0 + new Vector2(0, -num66 * drawPlayer.gravDir) - Main.screenPosition + drawPlayer.legPosition;
                    vector7 = vector7.Floor();
                    DrawData value = new DrawData(texture2, vector7, new Rectangle?(rectangle3), color14, drawPlayer.legRotation, rectangle3.Size() * new Vector2(0.5f, 0.5f - drawPlayer.gravDir / 2), 1f, SpriteEffects.None, 0);
                    value.shader = edi.legArmorShader;
                    Main.playerDrawData.Add(value);
                }
            }*/
        });

        #region Grovite Layers
        public PlayerLayer glGroviteHead = new PlayerLayer("AAMod", "glGroviteHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateHood")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateHood_Head_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.headArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.headRotation, drawPlayer.headPosition, edi.headOrigin);
            }
        });

        public PlayerLayer glGroviteBody = new PlayerLayer("AAMod", "glGroviteBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateCofferplate")))
            {
                Texture2D tex = mod.GetTexture("GroviteCofferplateBodyGlow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.bodyArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, edi.bodyOrigin);
            }
        });

        public PlayerLayer glGroviteLegs = new PlayerLayer("AAMod", "glGroviteLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && (!drawPlayer.mount.Active || drawPlayer.mount.Type != 6) && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateBoots")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateBoots_Legs_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.legArmorShader, drawPlayer, drawPlayer.legFrame, drawPlayer.legRotation, drawPlayer.legPosition, edi.legOrigin);
            }
        });

        public PlayerLayer glGroviteArm = new PlayerLayer("AAMod", "glGroviteArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (edi.shadow == 0 && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateCofferplate")))
            {
                Texture2D tex = mod.GetTexture("Glowmasks/AngryPirateCofferplate_Arms_Glow");
                DrawFlickerTexture(0, Main.playerDrawData, edi, tex, edi.bodyArmorShader, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, edi.bodyOrigin);
            }
        });

        public PlayerLayer glGroviteWings = new PlayerLayer("AAMod", "glGroviteWings", PlayerLayer.Wings, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            int accSlot = 0;
            bool social = false;

            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("AngryPirateSails"), ref social, ref accSlot))
            {
                int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1)
                {
                    dye = 0;
                }

                DrawFlickerTexture(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/AngryPirateSails_Wings/Glow"), dye, drawPlayer);
            }
        });
        #endregion

        public PlayerLayer glAfterAll = new PlayerLayer("AAMod", "glAfterAll", delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;

            if (drawPlayer.mount.Active)
            {
                return;
            }

            if (drawPlayer.GetModPlayer<AAPlayer>().ShieldScale > 0)
            {
                Texture2D Shield = mod.GetTexture("Textures/SagittariusShield");
                BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);

                Texture2D Ring = mod.GetTexture("Textures/SagittariusRing");
                BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);

                Texture2D RingGlow = mod.GetTexture("Glowmasks/SagittariusFreeRing_Glow");
                BaseDrawing.DrawTexture(Main.spriteBatch, RingGlow, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), ColorUtils.COLOR_GLOWPULSE, true);
            }

            if (drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale > 0)
            {
                Texture2D Shield = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");
                int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                BaseDrawing.DrawTexture(Main.spriteBatch, Shield, red, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);
            }

            int cbuff = drawPlayer.GetModPlayer<AAPlayer>().CarrotBuff;

            if (cbuff > 0)
            {
                Texture2D Shield = mod.GetTexture("Textures/CBoost1");
                if (drawPlayer.HasBuff(mod.BuffType("CBoost2")))
                {
                    Shield = mod.GetTexture("Textures/CBoost2");
                }
                if (drawPlayer.HasBuff(mod.BuffType("CBoost3")))
                {
                    Shield = mod.GetTexture("Textures/CBoost3");
                }
                BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), Main.DiscoColor, true);
            }
        });

        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0;
            bool dummy = false;

            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman)
            {
                return false;
            }

            ModItem mitem = ItemLoader.GetItem(type);
            if (mitem != null)
            {
                Item item = mitem.item;
                if (item.headSlot > 0)
                {
                    return BasePlayer.HasHelmet(player, type) && BaseDrawing.ShouldDrawHelmet(player, type);
                }
                else if (item.bodySlot > 0)
                {
                    return BasePlayer.HasChestplate(player, type) && BaseDrawing.ShouldDrawChestplate(player, type);
                }
                else if (item.legSlot > 0)
                {
                    return BasePlayer.HasLeggings(player, type) && BaseDrawing.ShouldDrawLeggings(player, type);
                }
                else if (item.accessory)
                {
                    return BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseDrawing.ShouldDrawAccessory(player, type);
                }
            }

            return false;
        }

        public static void DrawFlickerTexture(int drawType, object sb, PlayerDrawInfo edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default, float rotation = 0, Vector2 drawPos = default, Vector2 framePos = default)
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead)
            {
                return;
            }

            for (int j = 0; j < 7; j++)
            {
                Color color = new Color(110 - j * 10, 110 - j * 10, 110 - j * 10, 110 - j * 10);
                Vector2 vector = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)) * 0.4f;

                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings)
                    {
                        rotation = drawPlayer.bodyRotation;
                        frame = new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4);
                        framePos = new Vector2(Main.wingsTexture[drawPlayer.wings].Width / 2, Main.wingsTexture[drawPlayer.wings].Height / 8);
                    }

                    Vector2 pos;
                    int x;
                    int y;

                    if (wings)
                    {
                        x = (int)(edi.position.X - Main.screenPosition.X + drawPlayer.width / 2 - 9 * drawPlayer.direction);
                        y = (int)(edi.position.Y - Main.screenPosition.Y + drawPlayer.height / 2 + 2f * drawPlayer.gravDir);
                        pos = new Vector2(x, y);
                    }
                    else
                    {
                        x = (int)(edi.position.X - Main.screenPosition.X - frame.Width / 2 + drawPlayer.width / 2);
                        y = (int)(edi.position.Y - Main.screenPosition.Y + drawPlayer.height - frame.Height + 4f);
                        pos = new Vector2(x, y);
                    }

                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0)
                        {
                            shader = shader
                        };
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else if (sb is SpriteBatch)
                    {
                        ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0);
                    }
                }
            }
        }
    }
}