using System.IO;
using System.Linq;
using AAMod.Buffs;
using AAMod.Items.Dev;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Shen;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using AAMod.NPCs.Bosses.Zero.Protocol;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using AAMod.NPCs.Bosses.Yamata;
using System.Collections.Generic;
using BaseMod;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using AAMod.Items;
using AAMod.Items.DevTools;
using AAMod.Items.BossSummons;
namespace AAMod
{
    public class AAPlayer : ModPlayer
    {
        //Minions
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
        public bool DemonMinion = false;
        public bool DevilMinion = false;
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
        // Biome bools.
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
        public bool AshCurse;
        public int VoidGrav = 0;
        public static int Ashes = 0;
        public int CthulhuCountdown = 10800;
        public bool Leave = false;

        public bool ZoneTower;

        public bool RadiumStars = false;
        public bool Darkmatter = true;
        // Armor bools.
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
        public bool uraniumSet;
        public bool techneciumSet;
        public bool trueCopper;
        public bool trueHallow;
        public bool trueNights;
        public bool trueFlesh;
        public bool trueTribal;
        public bool trueDeathly;
        public bool trueAbyssal;
        public bool trueDemon;
        public bool trueDemonBonus;
        public bool trueBlazing;
        public bool trueRaider;
        public bool trueAtlantean;
        public bool trueDynaskull;
        public int DynaskullCooldown = 0;
        public bool terraSet;
        public bool chaosSet;
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool DarkmatterSet;
        public bool dracoSet;
        public bool dreadSet;
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
        public bool Assassin;
        public bool AbyssalStealth;
        public bool AssassinStealth;
        public bool Witch;
        // Accessory bools.
        public bool clawsOfChaos;
        public bool HydraPendant;
        public bool demonGauntlet;
        public bool StormClaw;
        public bool dwarvenGauntlet;
        public bool BrokenCode;
        public bool InfinityGauntlet;
        public bool TrueInfinityGauntlet;
        public bool Power;
        public bool Reality;
        public bool Mind;
        public bool Time;
        public bool Soul;
        public bool Space;
        public int SnapCD = 0;
        public int AbilityCD = 180;
        public bool AshRemover;
        public bool FogRemover;
        public bool Baolei;
        public bool Naitokurosu;
        public bool Duality;
        public bool DragonShell;
        public bool ammo20percentdown = false;
        public int AADash;
        public bool RStar;
        public bool DVoid;
        public int dashTimeAA;
        public int dashDelayAA;
        public int[] AADoubleTapKeyTimer = new int[4];
        public int[] AAHoldDownKeyTimer = new int[4];
        public bool DiscordShredder;
        public bool lantern = false;
        public bool HeartP = false;
        public bool HeartS = false;
        public bool HeartA = false;
        public bool DragonsGuard = false;
        public bool ShadowBand = false;

        public bool SagShield = false;
        public bool ShieldUp = false;
        public int SagCooldown = 0;

        public bool BegAccessoryPrevious;
        public bool BegAccessory;
        public bool BegHideVanity;
        public bool BegForceVanity;
        public bool HorseBuff;
        //debuffs
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
        public bool Unstable = false;
        public bool Abducted = false;
        public Vector2 RingLocation;
        //buffs

        //pets
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

        //NPCcount

        public static int yamata = -1;

        //Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);

        public static Color ZeroColor = new Color((int)(233 * 0.7f), (int)(53 * 0.7f), (int)(53 * 0.7f));

        public static Color groviteColor = new Color(138, (int)(39 * 0.7f), (int)(196 * 0.7f));
        public static bool[] groviteGlow = new bool[255];

        //IZ Death count
        public static int ZeroKills = 0;

        //Stat Boosts
        public int ManaLantern = 0;

        //Misc
        public bool Compass = false;

        public Vector2 RiftPos = new Vector2(0, 0);
        
        public int PrismCooldown = 0;

        public bool WorldgenReminder = false;

        public override void ResetEffects()
        {
            //Minions
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
            DemonMinion = false;
            DevilMinion = false;
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
            //Armor
            MoonSet = false;
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            demonSet = false;
            demonBonus = false;
            fleshrendSet = false;
            goblinSlayer = false;
            tribalSet = false;
            techneciumSet = false;
            trueTribal = false;
            trueAbyssal = false;
            impSet = false;
            trueDemon = false;
            trueDemonBonus = false;
            trueDeathly = false;
            trueDynaskull = false;
            trueCopper = false;
            trueBlazing = false;
            trueRaider = false;
            trueAtlantean = false;
            terraSet = false;
            chaosSet = false;
            DynaskullSet = false;
            zeroSet = false;
            dracoSet = false;
            dreadSet = false;
            uraniumSet = false;
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
            AssassinStealth = false;
            AbyssalStealth = false;
            Witch = false;
            //Accessory
            AshRemover = false;
            FogRemover = false;
            clawsOfChaos = false;
            HydraPendant = false;
            demonGauntlet = false;
            StormClaw = false;
            BrokenCode = false;
            dwarvenGauntlet = false;
            InfinityGauntlet = false;
            Power = false;
            Reality = false;
            Mind = false;
            Time = false;
            Soul = false;
            Space = false;
            TrueInfinityGauntlet = false;
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
            SagShield = false;
            ShieldUp = false;
            DragonsGuard = false;
            ShadowBand = false;
            //Debuffs
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
            Unstable = false;
            //Buffs
            //Weapons
            //Pets
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
            //EnemyChecks
            IsGoblin = false;

            //Misc
            Compass = false;

            //Biomes

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
            WorldgenReminder = false;
        }

        /*public override TagCompound Save()
        {
            var PlayerBool = new List<string>();


            if (Compass) PlayerBool.Add("Compass");

            return new TagCompound {
                {"PlayerBool", PlayerBool},
                {
                    "ManaLantern", ManaLantern
                }
            };
        }


        public override void Load(TagCompound tag)
        {
            int ManaLantern = tag.GetInt("ManaLantern");
            var PlayerBool = tag.GetList<string>("PlayerBool");

            Compass = PlayerBool.Contains("Compass");

            if (tag.ContainsKey("ManaLantern"))
            {
                ManaLantern = tag.GetInt("ManaLantern");
            }
            else
            {
                ManaLantern = 0;
            }
        }*/

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            // Make sure this condition is the same as the condition in the Buff to remove itself. We do this here instead of in ModItem.UpdateAccessory in case we want future upgraded items to set blockyAccessory
            if (BegAccessory)
            {
                player.AddBuff(mod.BuffType<Buffs.Horse>(), 60, true);
            }
        }

        public override void PreUpdateBuffs()
        {
            
        }

        public override void UpdateBiomes()
        {
            ZoneTower = player.ZoneTowerSolar || player.ZoneTowerNebula || player.ZoneTowerStardust || player.ZoneTowerVortex;
            ZoneMire = (AAWorld.mireTiles > 100) || NPC.AnyNPCs(mod.NPCType<Yamata>()) || NPC.AnyNPCs(mod.NPCType<YamataA>());
            ZoneInferno = (AAWorld.infernoTiles > 100) || (NPC.AnyNPCs(mod.NPCType<Akuma>()) || NPC.AnyNPCs(mod.NPCType<AkumaA>()));
            ZoneMush = (AAWorld.mushTiles > 100);
            Terrarium = (AAWorld.terraTiles >= 1);
            ZoneVoid = (AAWorld.voidTiles > 20) || (NPC.AnyNPCs(mod.NPCType<Zero>()) || NPC.AnyNPCs(mod.NPCType<ZeroAwakened>()));
            //ZoneStorm = (AAWorld.stormTiles >= 1);
            //ZoneShip = (AAWorld.shipTiles >= 1);
            ZoneRisingMoonLake = AAWorld.lakeTiles >= 1;
            ZoneRisingSunPagoda = AAWorld.pagodaTiles >= 1;
            ZoneStars = AAWorld.Radium >= 20;
        }

        public static Player PlayerPos = Main.player[Main.myPlayer];

        public static Color Uranium
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), Color.Green, Color.Green, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)));
            }
        }

        public static Color FlashGlow
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Transparent, Color.White, Color.White, Color.Transparent);
            }
        }

        public float Intensity;

        public override void UpdateBiomeVisuals()
        {
            bool useShen = (NPC.AnyNPCs(mod.NPCType<ShenDoragon>()));
            bool useShenA = (NPC.AnyNPCs(mod.NPCType<ShenA>()));
            bool useAkuma = (NPC.AnyNPCs(mod.NPCType<AkumaA>()) || AkumaAltar);
            bool useYamata = (NPC.AnyNPCs(mod.NPCType<YamataA>()) || YamataAltar);
            bool useMire = (ZoneMire || MoonAltar) && !useYamata && !useShen;
            bool useInferno = (ZoneInferno || SunAltar) && !useAkuma && !useShen;
            bool useVoid = (ZoneVoid || VoidUnit) && !useShen;
            bool useStars = ZoneStars && !useShen;

            //player.ManageSpecialBiomeVisuals("AAMod:StarSky", useStars);

            player.ManageSpecialBiomeVisuals("AAMod:ShenSky", useShen);

            player.ManageSpecialBiomeVisuals("AAMod:ShenASky", useShenA);

            player.ManageSpecialBiomeVisuals("AAMod:AkumaSky", useAkuma);

            player.ManageSpecialBiomeVisuals("AAMod:YamataSky", useYamata);

            player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", useInferno);

            if (Main.UseHeatDistortion)
            {
                player.ManageSpecialBiomeVisuals("HeatDistortion", useAkuma || useInferno);
            }

            player.ManageSpecialBiomeVisuals("AAMod:MireSky", useMire);

            player.ManageSpecialBiomeVisuals("AAMod:VoidSky", useVoid);
        }

        public override bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>(mod);
            return (ZoneMire == modOther.ZoneMire &&
                ZoneInferno == modOther.ZoneInferno &&
                ZoneVoid == modOther.ZoneVoid &&
                ZoneMush == modOther.ZoneMush &&
                Terrarium == modOther.Terrarium &&
                ZoneStorm == modOther.ZoneStorm &&
                ZoneShip == modOther.ZoneShip &&
                ZoneStars == modOther.ZoneStars);
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>(mod);
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
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            byte flags = 0;
            if (ZoneInferno)
                flags |= 1;
            if (ZoneMire)
                flags |= 2;
            if (ZoneVoid)
                flags |= 3;
            if (ZoneMush)
                flags |= 4;
            if (Terrarium)
                flags |= 5;
            if (ZoneStorm)
                flags |= 6;
            if (ZoneRisingSunPagoda)
                flags |= 7;
            if (ZoneRisingMoonLake)
                flags |= 8;
            if (ZoneShip)
                flags |= 9;
            if (ZoneStars)
                flags |= 10;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            byte flags = reader.ReadByte();
            ZoneInferno = ((flags & 1) == 1);
            ZoneMire = ((flags & 2) == 2);
            ZoneVoid = ((flags & 3) == 3);
            ZoneMush = ((flags & 4) == 4);
            Terrarium = ((flags & 5) == 5);
            ZoneStorm = ((flags & 6) == 6);
            ZoneRisingSunPagoda = ((flags & 7) == 7);
            ZoneRisingMoonLake = ((flags & 8) == 8);
            ZoneShip = ((flags & 9) == 9);
            ZoneStars = ((flags & 10) == 10);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Palladium)
            {
                player.AddBuff(BuffID.RapidHealing, 300);
            }
	    if (trueNights && item.melee && Main.rand.Next(4) == 0)
            {
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType<Items.Armor.TrueNights.CursedFireball>(), damage, 0, Main.myPlayer);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Palladium)
            {
                player.AddBuff(BuffID.RapidHealing, 300);
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (DragonsGuard)
            {
                npc.AddBuff(BuffID.OnFire, 120);
            }
            if (fleshrendSet && Main.rand.Next(2) == 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 position;
                        position.X = player.Center.X - 40;
                        position.Y = player.Center.Y - 40;
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

            if (trueCopper)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType<Items.Armor.TrueCopper.CopperSoul>(), 40, 10, Main.myPlayer, 0, 0);
            }

            if (fulgurite)
            {
                if (!ringActive)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType<Items.Armor.Fulgurite.FulguriteRing>(), 40, 6, Main.myPlayer, 0, 0);
                }
            }

            if (trueBlazing)
            {
                npc.AddBuff(mod.BuffType<Electrified>(), 180);
            }


            if (techneciumSet)
            {
                npc.AddBuff(mod.BuffType<Electrified>(), 180);
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
                player.endurance *= 1.8f;
            }
            else
            {
                player.endurance *= 1f;
            }
        }

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
                if ((liquidType == 0 || liquidType == 1) && player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
                {
                    caughtType = mod.ItemType("InfernoCrate");
                }
                if (liquidType == 0 && player.GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    caughtType = mod.ItemType("MireCrate");
                }
                else if (liquidType == 1 && ItemID.Sets.CanFishInLava[fishingRod.type] && player.ZoneUnderworldHeight)
                {
                    caughtType = mod.ItemType("HellCrate");
                }
            }
        }

        public int[] Spheres = null;
        public float ShieldScale = 0;
        public float RingRoatation = 0;

        public override void PostUpdate()
        {
            if (SagCooldown > 0)
            {
                player.noItems = true;
                SagCooldown--;
            }
            else
            {
                player.noItems = false;
                SagCooldown = 0;
            }
            if (ShieldUp)
            {
                RingRoatation += .05f;
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

            if (ShieldScale > 0)
            {
                RingRoatation += .05f;
            }
            if (trueAbyssal)
            {
                Color light = BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y));
                if (light.R < 40 && light.G < 40 && light.B < 40)
                {
                    AbyssalStealth = true;
                }
                else
                {
                    AbyssalStealth = false;
                }
            }
            if (uraniumSet)
            {
                Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), Color.Green, Color.Green, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)));
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), (color * .01f).R, (color * .01f).G, (color * .01f).B);
                float RadiationDistance = 64f;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.boss && Vector2.Distance(player.Center, nPC.Center) <= RadiationDistance && Collision.CanHit(nPC.position, nPC.width, nPC.height, Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height))
                        {
                            player.ApplyDamageToNPC(nPC, 1, 0f, 0, false);
                        }
                    }
                }
            }

            if (Orbiters)
            {
                Spheres = BaseAI.GetProjectiles(player.Center, mod.NPCType("FireOrbiter"), Main.myPlayer, 48);
                if (player.ownedProjectileCounts[mod.ProjectileType("FireOrbiter")] > 0)
				{
                    player.minionDamage += AAGlobalProjectile.CountProjectiles(mod.ProjectileType<Projectiles.AH.FireOrbiter>()) * .1f;
                    if (Main.netMode != 2 && Main.player[Main.myPlayer].miscCounter % 3 == 0)
                    {
                        for (int m = 0; m < Spheres.Length; m++)
                        {
                            Projectile projectile = Main.projectile[Spheres[m]];
                            if (projectile != null && projectile.active)
                            {
                                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaDustLight>());
                                Main.dust[dustID].position += (player.position - player.oldPosition);
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
                if (Main.rand.Next(8) == 0)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey uh...kid? Correct me if I'm wrong, but I think your world didn't generate with Ancients Awakened stuff in it. I'd make a new one if I were you.", new Color(180, 41, 32), false);
                }
                else if (Main.rand.Next(8) == 1)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("YOU IMBECILE! YOU DIDN'T GENERATE ANCIENTS AWAKENED CONTENT! MAKE A NEW WORLD NOW! REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE", new Color(45, 46, 70), false);
                }
                else if (Main.rand.Next(8) == 2)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("ERR0R. W0RLD D0ES N0T APPEAR T0 C0NTAIN AAM0D.TM0D C0NTENT. PLEASE GENERATE A NEW W0RLD.", new Color(255, 0, 0), false);
                }
                else if (Main.rand.Next(8) == 3)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("HEY! You didn't generate Ancients Awakened stuff in this world! Generate a new world before I blast you to mars!", new Color(102, 20, 48), false);
                }
                else if (Main.rand.Next(8) == 4)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey, uh...I don't see any Ancients Awakened content in this world. Might be smart to make a new world or whatever...", new Color(72, 78, 117), false);
                }
                else if (Main.rand.Next(8) == 5)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey. You. Interdimensional being. You might have forgotten to make a new world after downloading the mod. Make a new world if you want all the mod's content.", new Color(128, 0, 0), false);
                }
                else if (Main.rand.Next(8) == 6)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Make...new world....or mushmad...will squish...little terrarian...", new Color(216, 110, 40), false);
                }
                else if (Main.rand.Next(8) == 7)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("...Mortal. Your world doesn't have Ancients Awakened content if my old eyes are not lying to me. Generating a new world would be optimal.", new Color(43, 46, 61), false);
                }
                WorldgenReminder = true;
            }
            DarkmatterSet = darkmatterSetMe || darkmatterSetRa || darkmatterSetMa || darkmatterSetSu || darkmatterSetTh;
            if (RStar)
            {
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1f, 0.95f, 0.8f);
            }
            if (kindledSet || lantern)
            {
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, AAColor.Lantern.R / 255, (AAColor.Lantern.G / 255) * 0.95f, (AAColor.Lantern.B / 255) * 0.8f);
            }
            if (NPC.AnyNPCs(mod.NPCType<Yamata>()))
            {
                player.AddBuff(mod.BuffType<Buffs.YamataGravity>(), 10, true);
            }
            if (NPC.AnyNPCs(mod.NPCType<YamataA>()))
            {
                player.AddBuff(mod.BuffType<Buffs.YamataAGravity>(), 10, true);
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.dayTime && !AAWorld.downedYamata)
                {
                    if (!player.GetModPlayer<AAPlayer>(mod).FogRemover)
                    {
                        player.AddBuff(mod.BuffType<Clueless>(), 5);
                    }
                }
            }
            if (player.GetModPlayer<AAPlayer>().Terrarium)
            {
                player.AddBuff(mod.BuffType<Terrarium>(), 2);
                player.AddBuff(BuffID.DryadsWard, 2);
            }
            if (Main.rand.Next(3600) == 0)
            {
                VoidGrav = (Main.rand.Next(0, 5) + 1);
            }
            if (NPC.AnyNPCs(mod.NPCType<ZeroAwakened>()))
            {
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
                }
                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
            }
            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (!BrokenCode)
                {
                    if (VoidGrav == 0)
                    {
                        VoidGrav = (Main.rand.Next(0, 5) + 1);
                    }
                    if (VoidGrav == 1)
                    {
                        player.gravity = 0.1f;
                    }
                    if (VoidGrav == 2)
                    {
                        player.gravity = 0.5f;
                    }
                    if (VoidGrav == 3)
                    {
                        player.gravity = 1f;
                    }
                    if (VoidGrav == 4)
                    {
                        player.gravity = 5f;
                    }
                    if (VoidGrav == 5)
                    {
                        player.gravity = 10f;
                    }
                }
                else
                {
                    player.gravity = 1f;
                }
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AshCurse)
                {
                    AshRain(player, mod);
                }
            }
            if (player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AAWorld.downedAllAncients && !AAWorld.downedShen)
                {
                    EmberRain(player, mod);
                }
            }

            if (Assassin)
            {
                bool flag14 = false;
                if (AssassinStealth)
                {
                    float num29 = player.stealth;
                    player.stealth -= 0.04f;
                    if (player.stealth < 0f)
                    {
                        player.stealth = 0f;
                    }
                    else
                    {
                        flag14 = true;
                    }
                    if (player.stealth == 0f && num29 != player.stealth && Main.netMode == 1)
                    {
                        NetMessage.SendData(84, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                    player.rangedDamage += (1f - player.stealth) * 0.8f;
                    player.meleeDamage += (1f - player.stealth) * 0.8f;
                    player.rangedCrit += (int)((1f - player.stealth) * 20f);
                    player.meleeCrit += (int)((1f - player.stealth) * 20f);
                    player.aggro -= (int)((1f - player.stealth) * 1200f);
                    if (player.mount.Active)
                    {
                        AssassinStealth = false;
                    }
                }
                else
                {
                    float num30 = player.stealth;
                    player.stealth += 0.04f;
                    if (player.stealth > 1f)
                    {
                        player.stealth = 1f;
                    }
                    else
                    {
                        flag14 = true;
                    }
                    if (player.stealth == 1f && num30 != player.stealth && Main.netMode == 1)
                    {
                        NetMessage.SendData(84, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                if (flag14)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                        Dust dust = Main.dust[Dust.NewDust(player.Center - vector * 30f, 0, 0, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust.noGravity = true;
                        dust.position = player.Center - vector * (float)Main.rand.Next(5, 11);
                        dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 4f;
                        dust.scale = 0.5f + Main.rand.NextFloat();
                        dust.fadeIn = 0.5f;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                        Dust dust2 = Main.dust[Dust.NewDust(player.Center - vector2 * 30f, 0, 0, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust2.noGravity = true;
                        dust2.position = player.Center - vector2 * 12f;
                        dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 2f;
                        dust2.scale = 0.5f + Main.rand.NextFloat();
                        dust2.fadeIn = 0.5f;
                    }
                }
            }


            if (trueAbyssal)
            {
                if (AbyssalStealth)
                {
                    float num29 = player.stealth;
                    player.stealth -= 0.04f;
                    if (player.stealth < 0f)
                    {
                        player.stealth = 0f;
                    }
                    if (player.stealth == 0f && num29 != player.stealth && Main.netMode == 1)
                    {
                        NetMessage.SendData(84, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                    player.rangedDamage += (1f - player.stealth) * 0.6f;
                    player.rangedCrit += (int)((1f - player.stealth) * 16f);
                    player.aggro -= 50;
                    if (player.mount.Active)
                    {
                        AbyssalStealth = false;
                    }
                }
                else
                {
                    float num30 = player.stealth;
                    player.stealth += 0.04f;
                    if (player.stealth > 1f)
                    {
                        player.stealth = 1f;
                    }
                    if (player.stealth == 1f && num30 != player.stealth && Main.netMode == 1)
                    {
                        NetMessage.SendData(84, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }

        public void DropDevArmor(int dropType)
        {
            //0 = Pre-HM
            //1 = HM
            //2 = PML
            //3 = PA
            bool spawnedDevItems = false; //this prevents it from not dropping anything if the chance lands on something it cannot drop yet (for prehm/hm) as by this point it's past the 10% chance and thus should drop.
            string addonEX = (dropType == 3 ? "EX" : ""); //only include EX if it's a dropType 3 (ie from ancients)
            while (!spawnedDevItems)
            {
                int choice = Main.rand.Next(18);
                switch (choice)
                {
                    case 0:
                        player.QuickSpawnItem(mod.ItemType("HalHat"));
                        player.QuickSpawnItem(mod.ItemType("HalTux"));
                        player.QuickSpawnItem(mod.ItemType("HalTrousers"));
                        if (dropType >= 2) player.QuickSpawnItem(mod.ItemType("Prismeow" + addonEX));
                        spawnedDevItems = true;
                        break;
                    case 1:
                        string addonA = (dropType == 3 ? "A" : "");
                        if (Main.rand.Next(4000) == 0)
                        {
                            if (dropType < 2)
                            {
                                player.QuickSpawnItem(mod.ItemType("ShinyFishDiverMask" + addonA));
                                player.QuickSpawnItem(mod.ItemType("ShinyFishDiverJacket" + addonA));
                                player.QuickSpawnItem(mod.ItemType("ShinyFishDiverBoots" + addonA));
                            }
                            if (dropType >= 1)
                            {
                                player.QuickSpawnItem(mod.ItemType("MudkipBall"));
                                player.QuickSpawnItem(mod.ItemType("ShinyKipronWings"));
                            }
                            if (dropType >= 2)
                            {
                                player.QuickSpawnItem(mod.ItemType("SFishDiverMaskA"));
                                player.QuickSpawnItem(mod.ItemType("SFishDiverJacketA"));
                                player.QuickSpawnItem(mod.ItemType("SFishDiverBootsA"));
                                player.QuickSpawnItem(mod.ItemType("AmphibianLongsword" + addonEX + "S"));
                            }

                            spawnedDevItems = true;
                            break;
                        }
                        player.QuickSpawnItem(mod.ItemType("FishDiverMask" + addonA));
                        player.QuickSpawnItem(mod.ItemType("FishDiverJacket" + addonA));
                        player.QuickSpawnItem(mod.ItemType("FishDiverBoots" + addonA));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("MudkipBall"));
                            player.QuickSpawnItem(mod.ItemType("KipronWings"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("AmphibianLongsword" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;
                    case 2:
                        player.QuickSpawnItem(mod.ItemType("PonyHead"));
                        player.QuickSpawnItem(mod.ItemType("PonyBody"));
                        player.QuickSpawnItem(mod.ItemType("PonyHoofs"));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("MonochromeApple"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("PoniumStaff" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;
                    case 3:
                        player.QuickSpawnItem(mod.ItemType("GlitchesHat"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesBreastplate"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesGreaves"));
                        if (dropType >= 2) player.QuickSpawnItem(mod.ItemType("UmbreonSP" + addonEX));
                        spawnedDevItems = true;
                        break;
                    case 4:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("CordesDuFuret_Notes"));
                        }
                        spawnedDevItems = true;
                        break;
                    case 5:

                        player.QuickSpawnItem(mod.ItemType("FazerHood"));
                        player.QuickSpawnItem(mod.ItemType("FazerShirt"));
                        player.QuickSpawnItem(mod.ItemType("FazerPants"));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("FazerPaws"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("Fluff" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;
                    case 6:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("SkrallStaff"));
                            spawnedDevItems = true;
                        }
                        break;
                    case 7:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 3 ? "DragonShell" : "CharlieShell"));
                            spawnedDevItems = true;
                        }
                        break;
                    case 8:
                        player.QuickSpawnItem(mod.ItemType("FezLordsBag"));
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 3 ? "Chronos" : "TimeTeller"));
                        }
                        spawnedDevItems = true;
                        break;
                    case 9:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("TitanAxe" + addonEX));
                            spawnedDevItems = true;
                        }
                        break;
                    case 10:
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("EnderStaff" + addonEX));
                            spawnedDevItems = true;
                        }
                        break;
                    case 11:
                        player.QuickSpawnItem(mod.ItemType("LizEars"));
                        player.QuickSpawnItem(mod.ItemType("LizShirt"));
                        player.QuickSpawnItem(mod.ItemType("LizBoots"));
                        player.QuickSpawnItem(mod.ItemType("LizScarf"));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("RoyalStar"));
                            player.QuickSpawnItem(mod.ItemType("NightingaleWings"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("CatsEyeRifle" + addonEX));
                            spawnedDevItems = true;
                        }
                        break;
                    case 12:
                        player.QuickSpawnItem(mod.ItemType("DJDuckHead"));
                        player.QuickSpawnItem(mod.ItemType("DJDuckShirt"));
                        player.QuickSpawnItem(mod.ItemType("DJDuckPants"));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("DuckstepWings"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("DuckstepGun" + addonEX));
                            spawnedDevItems = true;
                        }
                        break;
                    case 13:
                        player.QuickSpawnItem(mod.ItemType("TiedHat"));
                        player.QuickSpawnItem(mod.ItemType("TiedHalTux"));
                        player.QuickSpawnItem(mod.ItemType("TiedTrousers"));
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 3 ? "GentlemansLongblade" : "GentlemansRapier"));
                        }
                        spawnedDevItems = true;
                        break;
                    case 14:
                        player.QuickSpawnItem(mod.ItemType("MoonHood"));
                        player.QuickSpawnItem(mod.ItemType("MoonRobe"));
                        player.QuickSpawnItem(mod.ItemType("MoonBoots"));
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("Etheral" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;
                    case 15:
                        player.QuickSpawnItem(mod.ItemType("TailsHead"));
                        player.QuickSpawnItem(mod.ItemType("TailsBody"));
                        player.QuickSpawnItem(mod.ItemType("TailsLegs"));
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType(dropType == 3 ? "FreedomStar" : "MobianBuster"));
                        }
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
        
        public void PMLDevArmor()
        {
            DropDevArmor(2);
        }

        public void SADevArmor()
        {
            DropDevArmor(3);
        }

        public override void PreUpdate()
        {
            if (SnapCD != 0)
            {
                SnapCD--;
            }
            if ((Mind || Power || Reality || Soul || Space || Time) && !(dwarvenGauntlet || InfinityGauntlet || TrueInfinityGauntlet))
            {
                player.AddBuff(mod.BuffType<InfinityOverload>(), 180);
            }
            if (player.GetModPlayer<AAPlayer>().ZoneVoid || player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake /*|| NPC.AnyNPCs(mod.NPCType<SoC>())*/)
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

        public static void AshRain(Player player, Mod mod)
        {
            if (Main.gamePaused)
            {
                return;
            }
            if ((player.GetModPlayer<AAPlayer>(mod).ZoneInferno || player.GetModPlayer<AAPlayer>(mod).ZoneRisingSunPagoda) && player.GetModPlayer<AAPlayer>(mod).AshCurse)
            {
                if (!player.GetModPlayer<AAPlayer>(mod).AshRemover || !(player.ZoneSkyHeight || player.ZoneOverworldHeight))
                {
                    player.AddBuff(mod.BuffType<BurningAsh>(), 5);
                }
                if (AAWorld.infernoTiles > 0 && Main.player[Main.myPlayer].position.Y < Main.worldSurface * 16.0)
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
                                if (Main.player[Main.myPlayer].velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.player[Main.myPlayer].velocity.Y;
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
                                    int num9 = Dust.NewDust(new Vector2(num5, num6), 10, 10, mod.DustType<Dusts.AshRain>(), 0f, 0f, 0, default(Color), 1f);
                                    Main.dust[num9].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;
                                    Dust expr_292_cp_0 = Main.dust[num9];
                                    expr_292_cp_0.velocity.Y = expr_292_cp_0.velocity.Y * Main.dust[num9].scale;
                                    if (!player.GetModPlayer<AAPlayer>(mod).AshCurse)
                                    {
                                        Main.dust[num9].velocity.X = Main.rand.Next(-10, 10) * 0.1f;
                                        Dust expr_2EC_cp_0 = Main.dust[num9];
                                        expr_2EC_cp_0.velocity.X = expr_2EC_cp_0.velocity.X + Main.windSpeed * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[num9].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;
                                        Dust expr_370_cp_0 = Main.dust[num9];
                                        expr_370_cp_0.velocity.Y = expr_370_cp_0.velocity.Y * 0.5f;
                                    }
                                    Dust expr_38E_cp_0 = Main.dust[num9];
                                    expr_38E_cp_0.velocity.Y = expr_38E_cp_0.velocity.Y * (1f + 0.3f * Main.cloudAlpha);
                                    Main.dust[num9].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[num9].velocity *= 1f + Main.cloudAlpha * 0.5f;
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

        public static void EmberRain(Player player, Mod mod)
        {
            if (Main.gamePaused)
            {
                return;
            }
            if ((player.GetModPlayer<AAPlayer>(mod).ZoneRisingSunPagoda || player.GetModPlayer<AAPlayer>(mod).ZoneRisingMoonLake) && AAWorld.downedAllAncients && !AAWorld.downedShen)
            {
                if (Main.player[Main.myPlayer].position.Y < Main.worldSurface * 16.0)
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
                                if (Main.player[Main.myPlayer].velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.player[Main.myPlayer].velocity.Y;
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
                                    int num9 = Dust.NewDust(new Vector2(num5, num6), 10, 10, mod.DustType<Dusts.Discord>(), 0f, 0f, 0, default(Color), 1f);
                                    Main.dust[num9].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;
                                    Dust expr_292_cp_0 = Main.dust[num9];
                                    expr_292_cp_0.velocity.Y = expr_292_cp_0.velocity.Y * Main.dust[num9].scale;
                                    if (!player.GetModPlayer<AAPlayer>(mod).AshCurse)
                                    {
                                        Main.dust[num9].velocity.X = Main.rand.Next(-10, 10) * 0.1f;
                                        Dust expr_2EC_cp_0 = Main.dust[num9];
                                        expr_2EC_cp_0.velocity.X = expr_2EC_cp_0.velocity.X + Main.windSpeed * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[num9].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;
                                        Dust expr_370_cp_0 = Main.dust[num9];
                                        expr_370_cp_0.velocity.Y = expr_370_cp_0.velocity.Y * 0.5f;
                                    }
                                    Dust expr_38E_cp_0 = Main.dust[num9];
                                    expr_38E_cp_0.velocity.Y = expr_38E_cp_0.velocity.Y * (1f + 0.3f * Main.cloudAlpha);
                                    Main.dust[num9].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[num9].velocity *= 1f + Main.cloudAlpha * 0.5f;
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

        public override void GetWeaponKnockback(Item item, ref float knockback)
        {
            if (demonGauntlet == true)
            {
                if (item.melee)
                {
                    knockback += 2f;
                }
            }
            if (dwarvenGauntlet == true)
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

        public void DrawItem(int i)
        {

            if (player.HeldItem.type == mod.ItemType("VoidStar"))
            {
                Vector2 vector25 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
                if (player.direction != 1)
                {
                    vector25.X = player.bodyFrame.Width - vector25.X;
                }
                if (player.gravDir != 1f)
                {
                    vector25.Y = player.bodyFrame.Height - vector25.Y;
                }
                vector25 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
                Vector2 position17 = player.RotatedRelativePoint(player.position + vector25, true) - player.velocity;
                for (int num277 = 0; num277 < 4; num277++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, 242, player.direction * 2, 0f, 150, new Color(110, 20, 0), 1.3f)];
                    dust.position = position17;
                    dust.velocity *= 0f;
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                    dust.velocity += player.velocity;
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.position += Utils.RandomVector2(Main.rand, -4f, 4f);
                        dust.scale += Main.rand.NextFloat();
                        if (Main.rand.Next(2) == 0)
                        {
                            dust.customData = this;
                        }
                    }
                }
            }
        }

        public virtual float UseTimeMultiplier(Item item, Player player)
        {
            float multiplier = 1f;

            int useTime = item.useTime;

            int useAnimate = item.useAnimation;
            if (StormClaw == true)
            {
                if (item.autoReuse == false)
                {
                    multiplier *= 2f;
                }
            }

            if (HydraPendant == true)
            {
                multiplier *= 1.15f;
            }

            while (useTime / multiplier < 1)
            {
                multiplier -= .1f;
            }

            while (useAnimate / multiplier < 2)
            {
                multiplier -= .1f;
            }

            return multiplier;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (trueDeathly && player.FindBuffIndex(mod.BuffType("UnstableSoul")) == -1)
            {
                player.statLife = 100;
                player.HealEffect(80);
                player.immune = true;
                player.immuneTime = player.longInvince ? 180 : 120;
                Main.NewText("Your soul ripples...", 51, 255, 255);
                player.AddBuff(mod.BuffType("UnstableSoul"), 18000);
                return false;
            }
            return true;
        }
        public override void clientClone(ModPlayer clientClone)
        {
            AAPlayer clone = clientClone as AAPlayer;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Time)
            {
                player.respawnTimer = (int)(player.respawnTimer * .2);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AAMod.Rift.JustPressed)
            {
                RiftPos = player.position;
                for (int m = 0; m < 58; m++)
                {
                    if (player.inventory[m].type == mod.ItemType<Items.Usable.RiftMirror>())
                    {
                        player.Spawn();
                    }
                }
            }
            if (AAMod.RiftReturn.JustPressed && RiftPos != new Vector2(0, 0))
            {
                for (int m = 0; m < 58; m++)
                {
                    if (player.inventory[m].type == mod.ItemType<Items.Usable.RiftMirror>())
                    {
                        player.position = RiftPos;
                    }
                }
            }
            if (InfinityGauntlet || TrueInfinityGauntlet || Alpha)
            {
                if (AAMod.InfinityHotKey.JustPressed && SnapCD <= 0)
                {
                    SnapCD = 18000;
                    Main.NewText("Perfectly Balanced, as all things should be...", Color.Purple);
                    Main.npc.Where(x => x.active && !x.townNPC && x.type != NPCID.TargetDummy && x.type != mod.NPCType<RiftShredder>() && x.type != mod.NPCType<Taser>() && x.type != mod.NPCType<RealityCannon>() && x.type != mod.NPCType<VoidStar>() && x.type != mod.NPCType<TeslaHand>() && !x.boss).ToList().ForEach(x =>
                    {
                        player.ApplyDamageToNPC(x, damage: x.lifeMax, knockback: 0f, direction: 0, crit: true);
                    });
                }
            }
            if (Assassin)
            {
                if (!player.mount.Active)
                {
                    AssassinStealth = !AssassinStealth;
                }
            }
            if (SagShield)
            {
                if (AAMod.AbilityKey.JustPressed && SagCooldown == 0)
                {
                    player.AddBuff(mod.BuffType<SagShield>(), 300);
                    SagCooldown = 18000;
                }
            }
            if (trueDynaskull)
            {
                if (AAMod.AbilityKey.JustPressed && AbilityCD == 0)
                {
                    AbilityCD = 180;
                    int i = Main.myPlayer;
                    float num72 = 8;
                    int num73 = 70;
                    float num74 = 1;
                    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                    float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
                    float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (player.gravDir == -1f)
                    {
                        num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
                    }
                    float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
                    float num81 = num80;
                    if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
                    {
                        num78 = (float)player.direction;
                        num79 = 0f;
                    }
                    else
                    {
                        num80 = num72 / num80;
                    }
                    vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                    vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, num78, num79, mod.ProjectileType("Dynabomb"), num73, num74, i, 0f, 0f);
                }
            }
            if (AbilityCD != 0)
            {
                AbilityCD--;
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
                    damage = damage * 5;
                    IsGoblin = true;
                }
            }


            if (perfectChaos)
            {
                target.AddBuff(mod.BuffType<DiscordInferno>(), 300);
            }

            if (trueFlesh)
            {
                target.AddBuff(BuffID.Ichor, 300);
            }

            if (trueNights)
            {
                target.AddBuff(BuffID.CursedInferno, 300);
            }

            if (valkyrieSet)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (Baolei)
            {
                if (!Main.dayTime)
                {
                    target.AddBuff(BuffID.OnFire, 1000);
                }
                else
                {
                    target.AddBuff(BuffID.Daybreak, 1000);
                }
            }

            if (Naitokurosu)
            {
                if (Main.dayTime)
                {
                    target.AddBuff(BuffID.Venom, 1000);
                }
                else
                {
                    target.AddBuff(mod.BuffType<Moonraze>(), 1000);
                }
            }

            if (Duality)
            {
                if (!Main.dayTime)
                {
                    target.AddBuff(mod.BuffType<Moonraze>(), 1000);
                }
                else
                {
                    target.AddBuff(BuffID.Daybreak, 1000);
                }
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

            if (StormClaw == true)
            {
                player.ApplyDamageToNPC(target, 20, 0, 0, false);
            }

            if (DiscordShredder)
            {
                player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(mod.BuffType<DiscordInferno>(), 300);
            }

            if (demonGauntlet)
            {
                if (WorldGen.crimson == false)
                {
                    target.AddBuff(BuffID.CursedInferno, 180);
                }
                if (WorldGen.crimson == true)
                {
                    target.AddBuff(BuffID.Ichor, 180);
                }
            }
            if (Time)
            {
                for (int i = 0; i < 255; i++)
                {

                    target.AddBuff(BuffID.Chilled, 1200);
                }
            }

            if (HeartP && player.statLife > (player.statLifeMax / 3))
            {
                target.AddBuff(mod.BuffType<DragonFire>(), 600);
            }
            else if (HeartP && player.statLife < (player.statLifeMax / 3))
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (HeartS && player.statLife > (player.statLifeMax / 3))
            {
                target.AddBuff(mod.BuffType<HydraToxin>(), 600);
            }
            else if (HeartS && player.statLife < (player.statLifeMax / 3))
            {
                target.AddBuff(mod.BuffType<Moonraze>(), 600);
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
        }

        public void RespawnIZ(Player player)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType("IZSpawn1");
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0, 1f);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 0f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        public int IZHoldTimer = 180;
        public bool InfZ = false;
        public int GetIZHealth = 2500000;
        public int RiftTimer;
        public int RiftDamage = 10;
        public int EscapeLine = 180;

        public override void UpdateLifeRegen()
        {
            if (trueFlesh)
            {
                player.lifeRegenTime++;
                player.lifeRegenTime++;
            }
        }
        
        public Vector2 OldHeadPos;
        public Vector2 OldBodyPos;
        public Vector2 OldLegPos;

        public override void UpdateBadLifeRegen()
        {
            int before = player.lifeRegen;
            bool drain = false;

            if (Abducted)
            {
                player.Center = RingLocation;
            }

            if (Unstable)
            {
                player.confused = true;
                player.moveSpeed *= Main.rand.NextFloat(.25f, 2f);
            }

            if (infinityOverload)
            {
                drain = true;
                player.lifeRegen -= 60;
            }

            if (YamataGravity || YamataAGravity)
            {
                if (player.mount.CanFly)
                {
                    player.mount.Dismount(player);
                }
                if (player.wingTimeMax > 30)
                {
                    player.wingTimeMax = 30;
                }
                if (YamataAGravity)
                {
                    player.moveSpeed *= .75f;
                }
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
            
            if (drain && before > 0)
            {
                player.lifeRegenTime = 0;
                player.lifeRegen -= before;
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
                player.magicDamage *= 0.8f;
                player.minionDamage *= 0.8f;
                player.meleeDamage *= 0.8f;
                player.thrownDamage *= 0.8f;
                player.rangedDamage *= 0.8f;
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
                player.lifeRegen -= Math.Abs((int)(player.velocity.X));
            }
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
            Unstable = false;
            Abducted = false;
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (trueFlesh)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 246, 0f, 0f, 46, default(Color), 1.381579f)];
                    dust.noGravity = true;
                }
            }
            if (trueNights)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 76, 0f, 0f, 46, default(Color), 1.381579f)];
                    dust.noGravity = true;
                }
            }
            if (demonGauntlet && !dwarvenGauntlet)
            {
                int ThisDust;
                if (WorldGen.crimson)
                {
                    ThisDust = 76;
                }
                else
                {
                    ThisDust = 264;
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ThisDust, 0f, 0f, 46, default(Color), 1.381579f)];
                    dust.noGravity = true;
                }
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (infinityOverload)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadB"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.1f;
                g *= 0.3f;
                b *= 0.7f;
                fullBright = true;
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadR"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.7f;
                g *= 0.2f;
                b *= 0.2f;
                fullBright = true;
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadG"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.1f;
                g *= 0.7f;
                b *= 0.1f;
                fullBright = true; if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadY"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.5f;
                g *= 0.5f;
                b *= 0.1f;
                fullBright = true; if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadP"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.6f;
                g *= 0.1f;
                b *= 0.6f;
                fullBright = true; if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("InfinityOverloadO"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 107, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
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
            if (infinityOverload)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType<Dusts.VoidDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }
            if (discordInferno)
            {
                for (int i = 0; i < 8; i++)
                {
                    int num4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType<Dusts.Discord>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }
            if (shroomed)
            {
                for (int i = 0; i < 3; i++)
                {
                    int num4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType<Dusts.ShroomDust>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0f, 0f, 0.45f);
            }
            if (riftbent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int num4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType<Dusts.CthulhuAuraDust>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
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

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

            if (trueAbyssal && proj.ranged)
            {
                target.AddBuff(mod.BuffType<HydratoxinFlaskBuff>(), 300);
            }

            if (MoonSet && proj.magic)
            {
                target.AddBuff(mod.BuffType<Moonraze>(), 300);
            }

            if (trueFlesh && proj.melee)
            {
                target.AddBuff(BuffID.Ichor, 300);
            }

            if (trueNights && proj.melee)
            {
                target.AddBuff(BuffID.CursedInferno, 300);
            }

            if (trueNights && proj.melee && Main.rand.Next(4) == 0)
            {
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType<Items.Armor.TrueNights.CursedFireball>(), damage, 0, Main.myPlayer);
                }
            }

            if (Duality)
            {
                if (!Main.dayTime)
                {
                    target.AddBuff(mod.BuffType<Moonraze>(), 1000);
                }
                else
                {
                    target.AddBuff(BuffID.Daybreak, 1000);
                }
            }

            if (Baolei && (proj.melee || proj.magic))
            {
                if (!Main.dayTime)
                {
                    target.AddBuff(BuffID.OnFire, 1000);
                }
                else
                {
                    target.AddBuff(BuffID.Daybreak, 1000);
                }
            }

            if (Naitokurosu && (proj.ranged || proj.minion))
            {
                if (Main.dayTime)
                {
                    target.AddBuff(BuffID.Venom, 1000);
                }
                else
                {
                    target.AddBuff(mod.BuffType<Moonraze>(), 1000);
                }
            }
            if (zeroSet && proj.ranged)
            {
                target.AddBuff(mod.BuffType<BrokenArmor>(), 1000);
            }
            if (perfectChaos && proj.melee)
            {
                target.AddBuff(mod.BuffType<DiscordInferno>(), 300);
            }

            if (dracoSet && proj.melee)
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (dreadSet && proj.ranged)
            {
                target.AddBuff(mod.BuffType<Moonraze>(), 600);
            }

            if (Time)
            {
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (DynaskullSet && proj.thrown)
            {
                target.AddBuff(BuffID.Confused, 180);
            }

            if (valkyrieSet && (proj.melee || proj.thrown))
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (depthSet && proj.minion)
            {
                target.AddBuff(BuffID.Poisoned, 180);
            }

            if (impSet && proj.minion)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }

            if (clawsOfChaos == true)
            {
                player.ApplyDamageToNPC(target, 5, 0, 0, false);
            }

            if (StormClaw == true)
            {
                player.ApplyDamageToNPC(target, 20, 0, 0, false);
            }

            if (DiscordShredder)
            {
                player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(mod.BuffType<DiscordInferno>(), 300);
            }

            if (trueDemon && proj.minion)
            {
                target.AddBuff(BuffID.ShadowFlame, 300);
            }

            if (darkmatterSetMe && proj.melee)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetRa && proj.ranged)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetMa && proj.magic)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetSu && proj.minion)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetTh && proj.thrown)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (Alpha && proj.thrown && Main.rand.Next(2) == 0 && !target.boss)
            {
                target.AddBuff(BuffID.Wet, 500);
            }

            if (demonGauntlet && proj.melee)
            {
                if (WorldGen.crimson == false)
                {
                    target.AddBuff(BuffID.CursedInferno, 180);
                }
                if (WorldGen.crimson == true)
                {
                    target.AddBuff(BuffID.Ichor, 180);
                }
            }
			
			if (player.HasBuff(mod.BuffType("DragonfireFlaskBuff")) && proj.melee)
			{
				target.AddBuff(mod.BuffType("DragonFire"), 900);
			}

			if (player.HasBuff(mod.BuffType("HydratoxinFlaskBuff")) && proj.melee)
			{
				target.AddBuff(mod.BuffType("Hydratoxin"), 900);
			}
        }
        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneMire || ZoneRisingMoonLake)
            {
                return mod.GetTexture("Map/MireMap");
            }
            if (ZoneInferno || ZoneRisingSunPagoda)
            {
                return mod.GetTexture("Map/InfernoMap");
            }
            if (ZoneVoid)
            {
                return mod.GetTexture("Map/VoidMap");
            }
            return null;
        }

        public static void SilentBossSpawn(Mod mod, Player player, string BossType)
        {
            int SpawnX = (int)player.Center.X - (int)MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble());
            int num = NPC.NewNPC(SpawnX, (int)(player.position.Y - 1200f), mod.NPCType(BossType), 0, 0f, 0f, 0f, 0f, 255);
            if (Main.netMode == 2 && num < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        public static void SilentBossSpawn(Mod mod, Player player, int BossType)
        {
            int SpawnX = (int)player.Center.X - (int)MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble());
            int num = NPC.NewNPC(SpawnX, (int)(player.position.Y - 1200f), BossType, 0, 0f, 0f, 0f, 0f, 255);
            if (Main.netMode == 2 && num < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
        }


        #region Draw Methods
        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0; bool dummy = false;
            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman) { return false; }
            ModItem mitem = ItemLoader.GetItem(type);
            if (mitem != null)
            {
                Item item = mitem.item;
                if (item.headSlot > 0) return BaseMod.BasePlayer.HasHelmet(player, type) && BaseMod.BaseDrawing.ShouldDrawHelmet(player, type);
                else if (item.bodySlot > 0) return BaseMod.BasePlayer.HasChestplate(player, type) && BaseMod.BaseDrawing.ShouldDrawChestplate(player, type);
                else if (item.legSlot > 0) return BaseMod.BasePlayer.HasLeggings(player, type) && BaseMod.BaseDrawing.ShouldDrawLeggings(player, type);
                else if (item.accessory) return BaseMod.BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseMod.BaseDrawing.ShouldDrawAccessory(player, type);
            }
            return false;
        }

        public static bool ShouldDrawArmSkin(Player drawPlayer, int type)
        {
            return BasePlayer.HasChestplate(drawPlayer, type, true) && BaseDrawing.ShouldDrawChestplate(drawPlayer, type);
        }
        public static Rectangle GetFrame(Player player, int itemtype, int count, int width, int height)
        {
            return BaseDrawing.GetFrame(count, width, height, 0, 2);
        }
        #endregion
        
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (Clueless)
            {
                drawInfo.upperArmorColor = Color.Black;
                drawInfo.middleArmorColor = Color.Black;
                drawInfo.lowerArmorColor = Color.Black;
                drawInfo.hairColor = Color.Black;
                drawInfo.eyeWhiteColor = Color.Black;
                drawInfo.eyeColor = Color.Black;
                drawInfo.faceColor = Color.Black;
                drawInfo.bodyColor = Color.Black;
                drawInfo.legColor = Color.Black;
                drawInfo.shirtColor = Color.Black;
                drawInfo.underShirtColor = Color.Black;
                drawInfo.pantsColor = Color.Black;
                drawInfo.shoeColor = Color.Black;
                drawInfo.headGlowMaskColor = Color.Black;
                drawInfo.bodyGlowMaskColor = Color.Black;
                drawInfo.armGlowMaskColor = Color.Black;
                drawInfo.legGlowMaskColor = Color.Black;
            }

        }
        public override void ModifyDrawLayers(List<PlayerLayer> list)
        {
            BaseDrawing.AddPlayerLayer(list, glAfterHead, PlayerLayer.Head, false);
            BaseDrawing.AddPlayerLayer(list, glAfterBody, PlayerLayer.Body, false);
            BaseDrawing.AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms, false);
            BaseDrawing.AddPlayerLayer(list, glAfterHandOn, PlayerLayer.HandOnAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterHandOff, PlayerLayer.HandOffAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms, false);
            BaseDrawing.AddPlayerLayer(list, glAfterWep, PlayerLayer.HeldItem, false);
            BaseDrawing.AddPlayerLayer(list, glAfterLegs, PlayerLayer.Legs, false);
            BaseDrawing.AddPlayerLayer(list, glAfterShield, PlayerLayer.ShieldAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterNeck, PlayerLayer.NeckAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterFace, PlayerLayer.FaceAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterAll, list[list.Count - 1], false);
        }

        public PlayerLayer glAfterWep = new PlayerLayer("AAMod", "glAfterWep", PlayerLayer.HeldItem, delegate (PlayerDrawInfo edi)
        {
            if (edi.shadow != 0) return;
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            Color lightColor = GetItemColor(drawPlayer, drawPlayer.Center);

            Item heldItem = drawPlayer.inventory[drawPlayer.selectedItem];
            BaseAAItem baseAAItem = null;
            if (heldItem.modItem != null && heldItem.modItem is BaseAAItem)
            {
                baseAAItem = (BaseAAItem)heldItem.modItem;
            }

            if (baseAAItem != null && baseAAItem.glowmaskTexture != null && baseAAItem.glowmaskDrawType != BaseAAItem.GLOWMASKTYPE_NONE)
            {
                Vector2? offsetNull = baseAAItem.HoldoutOffset();
                Vector2 offset = Vector2.Zero;
                if (offsetNull != null) offset = (Vector2)offsetNull;
                if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_SWORD)
                {
                    BaseDrawing.DrawHeldSword(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, null, 1, mod.GetTexture(baseAAItem.glowmaskTexture));
                }
                else
                if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_GUN)
                {
                    BaseDrawing.DrawHeldGun(Main.playerDrawData, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, false, false, 0f, 0f, null, 1, mod.GetTexture(baseAAItem.glowmaskTexture));
                }
            }
        });

        public PlayerLayer glAfterHead = new PlayerLayer("AAMod", "glAfterHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            DrawAfterHead(edi, default(PlayerHeadDrawInfo), false);
        });

        public static Color GetItemColor(Player drawPlayer, Vector2 position)
        {
            if (drawPlayer == null || position == default(Vector2) || drawPlayer.selectedItem < 0 || drawPlayer.selectedItem >= drawPlayer.inventory.Length || drawPlayer.inventory[drawPlayer.selectedItem] == null) return Color.White;
            Color c = Lighting.GetColor((int)((position.X + drawPlayer.width * 0.5f) / 16f), (int)((position.Y + drawPlayer.height * 0.5f) / 16f));
            return drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(c);
        }

        public PlayerLayer glAfterShield = new PlayerLayer("AAMod", "glAfterShield", PlayerLayer.ShieldAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (HasAndCanDraw(drawPlayer, mod.ItemType("TaiyangBaolei")))
            {
                if (!Main.dayTime)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TaiyangBaolei_Shield_Glow"), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
                }
                else
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TaiyangBaoleiA_Shield_Glow"), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
                }
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
                if (Main.dayTime)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/Naitokurosu_Neck_Glow"), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
                }
                else
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/NaitokurosuA_Neck_Glow"), edi.shieldShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
                }
            }
        });

        public PlayerLayer glAfterHandOn = new PlayerLayer("AAMod", "glAfterHandOn", PlayerLayer.HandOnAcc, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DemonGauntlet")))
            {
                Texture2D Glow = mod.GetTexture("Glowmasks/DemonGauntlet_HandsOn_Glow");
                Color GlowColor = AAColor.CursedInferno;
                if (WorldGen.crimson)
                {
                    GlowColor = AAColor.Ichor;
                }
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
                Color GlowColor = AAColor.CursedInferno;
                if (WorldGen.crimson)
                {
                    GlowColor = AAColor.Ichor;
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, Glow, edi.handOffShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, edi.shadow), drawPlayer.bodyFrame);
            }
        });

        public static void DrawAfterHead(PlayerDrawInfo edi, PlayerHeadDrawInfo edhi, bool mapHead)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = (mapHead ? edhi.drawPlayer : edi.drawPlayer);
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);
            object drawObj = null; if (mapHead) { drawObj = Main.spriteBatch; } else { drawObj = Main.playerDrawData; }
            Vector2 Position = (mapHead ? drawPlayer.position : edi.position);
            int dyeHead = (mapHead ? edhi.armorShader : edi.headArmorShader);
            Color colorArmorHead = (mapHead ? edhi.armorColor : edi.upperArmorColor);
            float scale = (mapHead ? edhi.scale : 0f);

            if (mapHead) { Position += new Vector2(0f, -3f * (1f - scale)); }

            if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DracoHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DracoHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("UraniumVisor")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/UraniumVisor_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAPlayer.Uranium, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("UraniumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/UraniumHeadgear_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAPlayer.Uranium, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("UraniumHood")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/UraniumHood_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAPlayer.Uranium, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/TrueNightsHelm_Glow_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAPlayer.Uranium, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("TrueFleshrendHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/TrueFleshrendHelm_Glow_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAPlayer.Uranium, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DoomsdayHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && !Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterVisor")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterVisor_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && !Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && !Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && !Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHeaddress")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHeaddress_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && !Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHat")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHat_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelm_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelmet_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHeadgear_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumMask_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("GripMaskRed")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/GripMaskRed_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DaybringerMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DaybringerMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("NightcrawlerMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/NightcrawlerMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RetrieverMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/RetrieverMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("ZeroMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/ZeroMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("TiedMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/TiedMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(FlashGlow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("LizEars")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/LizEars_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystalHelm")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/StarcrystalHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneHelmet")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/HeartstoneHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/TrueHallowedHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("ShroomHat")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/ShroomHat_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DJDuckHead")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DJDuckHead_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteVisor")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DoomiteVisor_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/TrueCopperHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosKabuto")))
            {
                if (drawPlayer.direction == -1)
                {
                    BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosKabutoBlue_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.headFrame, scale);
                }
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/PerfectChaosKabuto_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.headFrame, scale);
            }
        }
        public PlayerLayer glAfterBody = new PlayerLayer("AAMod", "glAfterBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("UraniumChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumChestplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("UraniumChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumChestplate_Female_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsPlate_Glow_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsPlate_Glow_Female"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("TrueFleshrendPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueFleshrendPlate_Glow_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (!drawPlayer.Male && HasAndCanDraw(drawPlayer, mod.ItemType("TrueFleshrendPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueFleshrendPlate_Glow_Female"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.bodyFrame);
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystaPlate")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/StarcrystaPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneChestguard")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/HeartstoneChestguard_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.bodyFrame);
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperPlate")))
            {
                if (drawPlayer.Male)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
                }
                else
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperPlate_Female_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
                }
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.Male)
                {
                    if (drawPlayer.direction == -1)
                    {
                        BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
                }
                else
                {
                    if (drawPlayer.direction == -1)
                    {
                        BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Female"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_Female_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
                }
            }
        });
        public PlayerLayer glAfterArm = new PlayerLayer("AAMod", "glAfterArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumChestplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumChestplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsPlate_Glow_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystaPlate")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/StarcrystalPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneChestguard")) && drawPlayer.statLife > 40)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HeartstoneChestguard_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomShirt")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomShirt_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteBreastplate")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperPlate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosPlate")))
            {
                if (drawPlayer.direction == -1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.bodyFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.bodyFrame);
            }
        });
        public PlayerLayer glAfterLegs = new PlayerLayer("AAMod", "glAfterLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>(mod);
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("UraniumBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/UraniumBoots_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueNightsBoots")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueNightsBoots_Glow_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Uranium, edi.shadow), drawPlayer.legFrame);
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("StarcrystalBoots")) && drawPlayer.statMana > drawPlayer.statMana * .2f)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/StarcrystalBoots_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Starcrystal, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("HeartstoneLeggings")) && drawPlayer.statLife < 40)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/HeartstoneLeggings_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Heartstone, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueHallowedGreaves")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueHallowedGreaves_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Hallow, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShroomPants")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/ShroomPants_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomiteGreaves")) && modPlayer.doomite)
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomiteGreaves_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("TrueCopperLeggings")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/TrueCopperLeggings_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Main.DiscoColor, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("PerfectChaosGreaves")))
            {
                if (drawPlayer.direction == -1)
                {
                    BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/PerfectChaos/PerfectChaosGreavesBlue_Legs"), edi.bodyArmorShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), edi.shadow), drawPlayer.legFrame);
                }
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/PerfectChaosGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, edi.shadow), drawPlayer.legFrame);
            }
        });
        public PlayerLayer glAfterAll = new PlayerLayer("GRealm", "glAfterAll", delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (drawPlayer.mount.Active) return;
            if (drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldUp)
            {
                Texture2D Shield = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusShield");
                Texture2D Ring = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusFreeRing");
                Texture2D RingGlow = mod.GetTexture("Glowmasks/SagittariusFreeRing_Glow");

                if (drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale > 0)
                {
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);
                    BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, drawPlayer.GetModPlayer<AAPlayer>(mod).RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);
                    BaseDrawing.DrawTexture(Main.spriteBatch, RingGlow, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>(mod).ShieldScale, drawPlayer.GetModPlayer<AAPlayer>(mod).RingRoatation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), GenericUtils.COLOR_GLOWPULSE, true);
                }
            }
        });
    }
}
