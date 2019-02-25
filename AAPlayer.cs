using System.IO;
using System.Linq;
using AAMod.Buffs;
using AAMod.Items.Dev;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Akuma.Awakened;
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
namespace AAMod
{
    public class AAPlayer : ModPlayer
    {
        //Minions
        public bool enderMinion = false;
        public bool enderMinionEX = false;
        public bool ChairMinion = false;
        public bool ChairMinionEX = false;
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
        public bool AshCurse;
        public int VoidGrav = 0;
        public static int Ashes = 0;
        public int CthulhuCountdown = 10800;
        public bool Leave = false;
        // Armor bools.
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
        public bool uraniumSet;
        public bool techneciumSet;
        public bool trueHallow;
        public bool trueNights;
        public bool trueFlesh;
        public bool trueTribal;
        public bool trueDeathly;
        public bool trueDemon;
        public bool trueDynaskull;
        public bool terraSet;
        public bool chaosSet;
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool dracoSet;
        public bool dreadSet;
        public bool zeroSet;
        public bool valkyrieSet;
        public bool infinitySet;
        public bool Alpha;
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
        public bool DragonShell;
        public bool ammo20percentdown = false;
        public int AADash;
        public int dashTimeAA;
        public int dashDelayAA;
        public int[] AADoubleTapKeyTimer = new int[4];
        public int[] AAHoldDownKeyTimer = new int[4];
        public bool DiscordShredder;
        
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
        private static int UI_ScreenAnchorX = Main.screenWidth - 800;
        public static SpriteFont fontMouseText;

        //Misc
        public bool Compass = false;

        public override void ResetEffects()
        {
            //Minions
            enderMinion = false;
            enderMinionEX = false;
            ChairMinion = false;
            ChairMinionEX = false;
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
            //Armor
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            fleshrendSet = false;
            goblinSlayer = false;
            tribalSet = false;
            techneciumSet = false;
             trueTribal = false;
            impSet = false;
            trueDemon = false;
            trueDeathly = false;
            trueDynaskull = false;
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
            //Accessory
            SnapCD = 0;
            AbilityCD = 0;
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
            Naitokurosu = false;
            ammo20percentdown = false;
            AshCurse = !Main.dayTime && !AAWorld.downedAkuma;
            AADash = 0;
            DiscordShredder = false;

            BegAccessoryPrevious = BegAccessory;
            BegAccessory = BegHideVanity = BegForceVanity = HorseBuff = false;
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
            //EnemyChecks
            IsGoblin = false;

            //Misc
            Compass = false;
            
        }

        public override void Initialize()
        {
            ManaLantern = 0;
        }

        public override TagCompound Save()
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
        }

        public override void UpdateVanityAccessories()
        {
            for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == mod.ItemType<Items.Vanity.Beg.Pony>())
                {
                    BegHideVanity = false;
                    BegForceVanity = true;
                }
            }
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            // Make sure this condition is the same as the condition in the Buff to remove itself. We do this here instead of in ModItem.UpdateAccessory in case we want future upgraded items to set blockyAccessory
            if (BegAccessory)
            {
                player.AddBuff(mod.BuffType<Buffs.Horse>(), 60, true);
            }
        }

        public override void FrameEffects()
        {
            if ((HorseBuff || BegForceVanity) && !BegHideVanity)
            {
                player.legs = mod.GetEquipSlot("Pony_Legs", EquipType.Legs);
                player.body = mod.GetEquipSlot("Pony_Body", EquipType.Body);
                player.head = mod.GetEquipSlot("Pony_Head", EquipType.Head);
            }
        }

        public override void PreUpdateBuffs()
        {
            if (uraniumSet)
            {
                Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)), Color.Green, Color.Green, BaseDrawing.GetLightColor(new Vector2(PlayerPos.position.X, PlayerPos.position.Y)));
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), (color * .01f).R, (color * .01f).G, (color * .01f).B);
                float RadiationDistance = 32f;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.boss && Vector2.Distance(player.Center, nPC.Center) <= RadiationDistance)
                        {
                            player.ApplyDamageToNPC(nPC, 1, 0f, 0, false);
                        }
                    }
                }
            }
        }

        public override void UpdateBiomes()
        {
            ZoneMire = (AAWorld.mireTiles > 100) || NPC.AnyNPCs(mod.NPCType<Yamata>()) || NPC.AnyNPCs(mod.NPCType<YamataA>());
            ZoneInferno = (AAWorld.infernoTiles > 100) || (NPC.AnyNPCs(mod.NPCType<Akuma>()) || NPC.AnyNPCs(mod.NPCType<AkumaA>()));
            ZoneMush = (AAWorld.mushTiles > 100);
            Terrarium = (AAWorld.terraTiles >= 1);
            ZoneVoid = (AAWorld.voidTiles > 20) || (NPC.AnyNPCs(mod.NPCType<Zero>()) || NPC.AnyNPCs(mod.NPCType<ZeroAwakened>()));
            //ZoneStorm = (AAWorld.stormTiles >= 1);
            //ZoneShip = (AAWorld.shipTiles >= 1);
            ZoneRisingMoonLake = AAWorld.lakeTiles >= 1;
            ZoneRisingSunPagoda = AAWorld.pagodaTiles >= 1;
        }

        public void AADashMovement()
        {
            if (AADash == 1 && dashDelayAA < 0 && player.whoAmI == Main.myPlayer)
            {
                Rectangle rectangle = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[player.whoAmI] <= 0)
                    {
                        NPC nPC = Main.npc[i];
                        Rectangle rect = nPC.getRect();
                        if (rectangle.Intersects(rect) && (nPC.noTileCollide || player.CanHit(nPC)))
                        {
                            float num = 1500f * player.meleeDamage;
                            float num2 = 15f;
                            bool crit = false;
                            if (player.kbGlove)
                            {
                                num2 *= 2f;
                            }
                            if (player.kbBuff)
                            {
                                num2 *= 1.5f;
                            }
                            if (Main.rand.Next(100) < player.meleeCrit)
                            {
                                crit = true;
                            }
                            int direction = player.direction;
                            if (player.velocity.X < 0f)
                            {
                                direction = -1;
                            }
                            if (player.velocity.X > 0f)
                            {
                                direction = 1;
                            }
                            if (player.whoAmI == Main.myPlayer)
                            {
                                player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
                            }
                            nPC.immune[player.whoAmI] = 6;
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 4;
                        }
                    }
                }
            }

            if (dashDelayAA > 0)
            {
                if (player.eocDash > 0)
                {
                    player.eocDash--;
                }
                if (player.eocDash == 0)
                {
                    player.eocHit = -1;
                }
                dashDelayAA--;
                return;
            }
            if (dashDelayAA < 0)
            {
                float num7 = 12f;
                float num8 = 0.992f;
                float num9 = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float num10 = 0.96f;
                int num11 = 20;
                if (AADash == 1)
                {
                    for (int m = 0; m < 24; m++)
                    {
                        int num14 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 4f), player.width, player.height - 8, 244, 0f, 0f, 100, default(Color), 2.75f);
                        Main.dust[num14].velocity *= 0.1f;
                        Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        Main.dust[num14].noGravity = true;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num14].fadeIn = 0.5f;
                        }
                    }
                    num7 = 18f;
                    num8 = 0.976f;
                    num10 = 0.9f;
                    num11 = 20;
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
                    dashDelayAA = num11;
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
                if (AADash == 4)
                {
                    int num23 = 0;
                    bool flag3 = false;
                    if (dashTimeAA > 0)
                    {
                        dashTimeAA--;
                    }
                    if (dashTimeAA < 0)
                    {
                        dashTimeAA++;
                    }
                    if (player.controlRight && player.releaseRight)
                    {
                        if (dashTimeAA > 0)
                        {
                            num23 = 1;
                            flag3 = true;
                            dashTimeAA = 0;
                        }
                        else
                        {
                            dashTimeAA = 15;
                        }
                    }
                    else if (player.controlLeft && player.releaseLeft)
                    {
                        if (dashTimeAA < 0)
                        {
                            num23 = -1;
                            flag3 = true;
                            dashTimeAA = 0;
                        }
                        else
                        {
                            dashTimeAA = -15;
                        }
                    }
                    if (flag3)
                    {
                        player.velocity.X = 31.9f * (float)num23;
                        Point point5 = (player.Center + new Vector2((float)(num23 * player.width / 2 + 2), player.gravDir * (float)(-(float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point6 = (player.Center + new Vector2((float)(num23 * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        dashDelayAA = -1;
                        for (int num24 = 0; num24 < 60; num24++)
                        {
                            int num25 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 3f);
                            Dust expr_13AF_cp_0 = Main.dust[num25];
                            expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
                            Dust expr_13D6_cp_0 = Main.dust[num25];
                            expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
                            Main.dust[num25].velocity *= 0.2f;
                            Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                            Main.dust[num25].noGravity = true;
                            Main.dust[num25].fadeIn = 0.5f;
                        }
                    }
                }
            }
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
            bool useAkuma = (NPC.AnyNPCs(mod.NPCType<AkumaA>()) || AkumaAltar); //&& !useIZ;
            player.ManageSpecialBiomeVisuals("AAMod:AkumaSky", useAkuma);
            player.ManageSpecialBiomeVisuals("HeatDistortion", useAkuma);
            bool useYamata = (NPC.AnyNPCs(mod.NPCType<YamataA>()) || YamataAltar);
            player.ManageSpecialBiomeVisuals("AAMod:YamataSky", useYamata);
            bool useInferno = (ZoneInferno || SunAltar) && !useAkuma;
            player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", useInferno);
            player.ManageSpecialBiomeVisuals("HeatDistortion", useInferno);
            bool useMire = (ZoneMire || MoonAltar) && !useYamata;
            player.ManageSpecialBiomeVisuals("AAMod:MireSky", useMire);
            bool useZero = NPC.AnyNPCs(mod.NPCType<ZeroAwakened>());
            if (useZero)
            {
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", Main.player[Main.myPlayer].position, new object[0]);
                }
                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(Math.Min(1f, 0.01f + Intensity));
            }
            bool useVoid = (ZoneVoid || VoidUnit);
            player.ManageSpecialBiomeVisuals("AAMod:VoidSky", useVoid);
            bool useFog = !FogRemover && (Main.dayTime && !AAWorld.downedYamata) && ZoneMire;
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
                ZoneShip == modOther.ZoneShip);
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
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (fleshrendSet && Main.rand.Next(2) == 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Dust dust;
                        Vector2 position;
                        position.X = player.Center.X - 40;
                        position.Y = player.Center.Y - 40;
                        dust = Main.dust[Dust.NewDust(position, 80, 80, 108, 0f, 0f, 124, new Color(255, 50, 0), 1f)];
                    }
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && !target.dontTakeDamage && !target.friendly && target.immune[player.whoAmI] == 0)
                        {
                            player.ApplyDamageToNPC(target, 30, 0, 0, false); // target , damage, knockback, direction, crit
                        }

                    }
                }
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
                if ((liquidType == 0 ||  liquidType == 1) && player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
                {
                    caughtType = mod.ItemType("InfernoCrate");
                }
                if (liquidType == 0  && player.GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    caughtType = mod.ItemType("MireCrate");
                }
                else if (liquidType == 1 && ItemID.Sets.CanFishInLava[fishingRod.type] && player.ZoneUnderworldHeight)
                {
                    caughtType = mod.ItemType("HellCrate");
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (player.pulley)
            {
                AADashMovement();
            }
            else if (player.grappling[0] == -1 && !player.tongued)
            {
                AAHorizontalMovement();
                AADashMovement();
            }
            if (Main.hasFocus)
            {
                for (int k = 0; k < AADoubleTapKeyTimer.Length; k++)
                {
                    AADoubleTapKeyTimer[k]--;
                    if (AADoubleTapKeyTimer[k] < 0)
                    {
                        AADoubleTapKeyTimer[k] = 0;
                    }
                }
                for (int l = 0; l < 4; l++)
                {
                    bool flag5 = false;
                    bool flag6 = false;
                    switch (l)
                    {
                        case 0:
                            flag5 = (player.controlDown && player.releaseDown);
                            flag6 = player.controlDown;
                            break;
                        case 1:
                            flag5 = (player.controlUp && player.releaseUp);
                            flag6 = player.controlUp;
                            break;
                        case 2:
                            flag5 = (player.controlRight && player.releaseRight);
                            flag6 = player.controlRight;
                            break;
                        case 3:
                            flag5 = (player.controlLeft && player.releaseLeft);
                            flag6 = player.controlLeft;
                            break;
                    }
                    if (flag5)
                    {
                        if (AADoubleTapKeyTimer[l] > 0)
                        {
                            ModKeyDoubleTap(l);
                        }
                        else
                        {
                            AADoubleTapKeyTimer[l] = 15;
                        }
                    }
                    if (flag6)
                    {
                        AAHoldDownKeyTimer[l]++;
                        player.KeyHoldDown(l, AAHoldDownKeyTimer[l]);
                    }
                    else
                    {
                        AAHoldDownKeyTimer[l] = 0;
                    }
                }
            }
        }

        public void AAHorizontalMovement()
        {
            float num = (player.accRunSpeed + player.maxRunSpeed) / 2f;
            if (player.controlLeft && player.velocity.X > -player.accRunSpeed && dashDelayAA >= 0)
            {
                if (player.mount.Active && player.mount.Cart)
                {
                    if (player.velocity.X < 0f)
                    {
                        player.direction = -1;
                    }
                }
                else if ((player.itemAnimation == 0 || player.inventory[player.selectedItem].useTurn) && player.mount.AllowDirectionChange)
                {
                    player.direction = -1;
                }
                if (player.velocity.Y == 0f || player.wingsLogic > 0 || player.mount.CanFly)
                {
                    if (player.velocity.X > player.runSlowdown)
                    {
                        player.velocity.X = player.velocity.X - player.runSlowdown;
                    }
                    player.velocity.X = player.velocity.X - player.runAcceleration * 0.2f;
                    if (player.wingsLogic > 0)
                    {
                        player.velocity.X = player.velocity.X - player.runAcceleration * 0.2f;
                    }
                }
                if (player.onWrongGround)
                {
                    if (player.velocity.X < player.runSlowdown)
                    {
                        player.velocity.X = player.velocity.X + player.runSlowdown;
                    }
                    else
                    {
                        player.velocity.X = 0f;
                    }
                }
                if (player.velocity.X < -num && player.velocity.Y == 0f && !player.mount.Active)
                {
                    int num3 = 0;
                    if (player.gravDir == -1f)
                    {
                        num3 -= player.height;
                    }
                    else if (AADash == 4)
                    {
                        int num7 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num3), player.width + 8, 4, 244, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default(Color), 3f);
                        Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
                        Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
                        Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                }
            }
            else if (player.controlRight && player.velocity.X < player.accRunSpeed && dashDelayAA >= 0)
            {
                if (player.mount.Active && player.mount.Cart)
                {
                    if (player.velocity.X > 0f)
                    {
                        player.direction = -1;
                    }
                }
                else if ((player.itemAnimation == 0 || player.inventory[player.selectedItem].useTurn) && player.mount.AllowDirectionChange)
                {
                    player.direction = 1;
                }
                if (player.velocity.Y == 0f || player.wingsLogic > 0 || player.mount.CanFly)
                {
                    if (player.velocity.X < -player.runSlowdown)
                    {
                        player.velocity.X = player.velocity.X + player.runSlowdown;
                    }
                    player.velocity.X = player.velocity.X + player.runAcceleration * 0.2f;
                    if (player.wingsLogic > 0)
                    {
                        player.velocity.X = player.velocity.X + player.runAcceleration * 0.2f;
                    }
                }
                if (player.onWrongGround)
                {
                    if (player.velocity.X > player.runSlowdown)
                    {
                        player.velocity.X = player.velocity.X - player.runSlowdown;
                    }
                    else
                    {
                        player.velocity.X = 0f;
                    }
                }
                if (player.velocity.X > num && player.velocity.Y == 0f && !player.mount.Active)
                {
                    int num8 = 0;
                    if (player.gravDir == -1f)
                    {
                        num8 -= player.height;
                    }
                    else if (AADash == 4)
                    {
                        int num12 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num8), player.width + 8, 4, 244, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default(Color), 3f);
                        Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
                        Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                }
            }
        }

        public void ModKeyDoubleTap(int keyDir)
        {
            int num = 0;
            if (Main.ReversedUpDownArmorSetBonuses)
            {
                num = 1;
            }
            if (keyDir == num)
            {

            }
        }

        public override void PostUpdate()
        {
            if (player.GetModPlayer<AAPlayer>().ZoneMire || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.dayTime && !AAWorld.downedYamata)
                {
                    if (!player.GetModPlayer<AAPlayer>(mod).FogRemover || !(player.ZoneSkyHeight || player.ZoneOverworldHeight))
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
                        player.QuickSpawnItem(mod.ItemType("FishDiverMask" + addonA));
                        player.QuickSpawnItem(mod.ItemType("FishDiverJacket" + addonA));
                        player.QuickSpawnItem(mod.ItemType("FishDiverBoots" + addonA));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("KipronWings"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("AmphibianLongsword" + addonEX));
                        }
                        spawnedDevItems = true;
                        break;
                    case 2:
                        player.QuickSpawnItem(mod.ItemType("Pony"));
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
                        player.QuickSpawnItem(mod.ItemType("ChinMask"));
                        player.QuickSpawnItem(mod.ItemType("ChinSuit"));
                        player.QuickSpawnItem(mod.ItemType("ChinPants"));
                        if (dropType >= 1)
                        {
                            player.QuickSpawnItem(mod.ItemType("ChinsMagicCoin"));
                        }
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("ChinStaff" + addonEX));
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
                        if (dropType >= 2)
                        {
                            player.QuickSpawnItem(mod.ItemType("CatsEyeRifle" + addonEX));
                            spawnedDevItems = true;
                        }
                        break;
                    case 12:
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
                Main.NewText("Your soul ripples", 51, 255, 255);
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
            if (InfinityGauntlet || TrueInfinityGauntlet || Alpha)
            {
                if (AAMod.InfinityHotKey.JustPressed && SnapCD == 0)
                {
                    SnapCD = 18000;
                    Main.npc.Where(x => x.active && !x.townNPC && x.type != NPCID.TargetDummy && x.type != mod.NPCType<CrabGuardian>() /*&& x.type != mod.NPCType<IZHand1>() && x.type != mod.NPCType<IZHand2>()*/ && x.type != mod.NPCType<RiftShredder>() && x.type != mod.NPCType<Taser>() && x.type != mod.NPCType<RealityCannon>() && x.type != mod.NPCType<VoidStar>() && x.type != mod.NPCType<TeslaHand>() && !x.boss).ToList().ForEach(x =>
                    {
                        Main.NewText("Perfectly Balanced, as all things should be", Color.Purple);
                        player.ApplyDamageToNPC(x, damage: x.lifeMax, knockback: 0f, direction: 0, crit: true);
                    });
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

            if (trueFlesh && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Ichor, 300);
            }

            if (trueNights && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.CursedInferno, 300);
            }

            if (valkyrieSet && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (darkmatterSetMe && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (kindledSet && Main.rand.Next(2) == 0)
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

            if (demonGauntlet && Main.rand.Next(2) == 0)
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
            if (Time && Main.rand.Next(2) == 0)
            {
                for (int i = 0; i < 255; i++)
                {

                    target.AddBuff(BuffID.Chilled, 1200);
                }
            }

            if (zeroSet && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.WitheredArmor, 1000);
            }

            if (dracoSet && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (Alpha && Main.rand.Next(2) == 0 && !target.boss)
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
                Main.npc[npcID].netUpdate2 = true;
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

        public override void UpdateBadLifeRegen()
        {
            int before = player.lifeRegen;
            bool drain = false;
            

            if (infinityOverload)
            {
                drain = true;
                player.lifeRegen -= 60;
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
                player.moveSpeed *= player.statLife / player.statLifeMax;
            }
        }

        

        public override void UpdateDead()
        {
            infinityOverload = false;
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

            if (trueFlesh && proj.melee && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Ichor, 300);
            }

            if (trueNights && proj.melee && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.CursedInferno, 300);
            }

            if (trueNights && proj.melee && Main.rand.Next(3) == 0)
            {
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.Center, new Vector2(0, 0), mod.ProjectileType("CursedFireball"), damage, 0);
                }
            }

            if (Baolei && (proj.melee || proj.magic) && Main.rand.Next(2) == 0)
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

            if (Naitokurosu && (proj.ranged || proj.minion) && Main.rand.Next(2) == 0)
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
            if (zeroSet && (proj.melee || proj.ranged) && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.WitheredArmor, 1000);
            }

            if (dracoSet && (proj.melee || proj.magic) && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (dreadSet && (proj.ranged || proj.thrown) && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType<Moonraze>(), 600);
            }

            if (Time && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Chilled, 180);
                target.AddBuff(mod.BuffType("TimeFrozen"), 300);
            }

            if (DynaskullSet && proj.thrown && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Confused, 180);
            }

            if (valkyrieSet && (proj.melee || proj.thrown) && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (depthSet && proj.ranged && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 180);
            }

            if (impSet && proj.minion && Main.rand.Next(2) == 0)
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

            if (trueDemon && proj.minion && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.ShadowFlame, 300);
            }

            if (darkmatterSetMe && proj.melee && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetRa && proj.ranged && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetMa && proj.magic && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetSu && proj.minion && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (darkmatterSetTh && proj.thrown && Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("Electrified"), 500);
            }

            if (Alpha && proj.thrown && Main.rand.Next(2) == 0 && !target.boss)
            {
                target.AddBuff(BuffID.Wet, 500);
            }

            if (demonGauntlet && proj.melee && Main.rand.Next(2) == 0)
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
        

        #region Draw Methods
        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0; bool dummy = false;
            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static void DrawFlickerTexture(int drawType, object sb, PlayerDrawInfo edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default(Rectangle), float rotation = 0, Vector2 drawPos = default(Vector2), Vector2 framePos = default(Vector2))
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead) { return; }
            for (int j = 0; j < 7; j++)
            {
                Color color = new Color(110 - j * 10, 110 - j * 10, 110 - j * 10, 110 - j * 10);
                Vector2 vector = new Vector2((float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5));
                vector *= 0.4f;
                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings) { rotation = drawPlayer.bodyRotation; frame = new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4); framePos = new Vector2((float)(Main.wingsTexture[drawPlayer.wings].Width / 2), (float)(Main.wingsTexture[drawPlayer.wings].Height / 8)); }
                    Vector2 pos = (wings ? new Vector2((float)((int)(edi.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(9 * drawPlayer.direction))), (float)((int)(edi.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir))) : new Vector2((float)((int)(edi.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(edi.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))));
                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, pos + drawPos + (wings ? default(Vector2) : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0);
                        dd.shader = shader;
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default(Vector2) : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0);
                }
            }
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
            BaseDrawing.AddPlayerLayer(list, glAfterWings, PlayerLayer.Wings, false);
            BaseDrawing.AddPlayerLayer(list, glAfterShield, PlayerLayer.ShieldAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterNeck, PlayerLayer.NeckAcc, false);
            BaseDrawing.AddPlayerLayer(list, glAfterFace, PlayerLayer.FaceAcc, false);
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
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SoulStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/SoulStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Orange);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("SpaceStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/SpaceStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/SpaceStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Cyan);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("RealityStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/RealityStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/RealityStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Red);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("TimeStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/TimeStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/TimeStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Green);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("PowerStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/PowerStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/PowerStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Purple);
            }
            if (HasAndCanDraw(drawPlayer, mod.ItemType("MindStone")))
            {
                BaseDrawing.DrawPlayerTexture(drawPlayer, mod.GetTexture("Glowmasks/MindStone_Face_Glow"), edi.faceShader, drawPlayer, edi.position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame);
                BaseDrawing.DrawAfterimage(drawPlayer, mod.GetTexture("Glowmasks/MindStone_Face_Glow"), edi.faceShader, drawPlayer, 0.8f, 1f, 6, false, 0f, 0f, Color.Yellow);
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
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterVisor")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterVisor_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHeaddress")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHeaddress_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHat")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHat_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelm")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelm_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelmet")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelmet_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHeadgear")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHeadgear_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumMask_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("GripMaskRed")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/GripMaskRed_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DaybringerMask")))
            {
                BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/Daybringer_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.headFrame, scale);
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
        }
        public PlayerLayer glAfterBody = new PlayerLayer("AAMod", "glAfterBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            
        });
        public PlayerLayer glAfterArm = new PlayerLayer("AAMod", "glAfterArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            
        });
        public PlayerLayer glAfterLegs = new PlayerLayer("AAMod", "glAfterLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
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
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterGreaves")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterGreaves_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("RadiumCuisses")))
            {
                BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumCuisses_Legs"), edi.legArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            
        });

        public static void DrawWingGlow(int drawType, object sb, PlayerDrawInfo edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default(Rectangle), float rotation = 0, Vector2 drawPos = default(Vector2), Vector2 framePos = default(Vector2))
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead) { return; }
            for (int j = 0; j < 7; j++)
            {
                Color color = default(Color);
                Vector2 vector = new Vector2((float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5));
                vector *= 0.4f;
                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings) { rotation = drawPlayer.bodyRotation; frame = new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4); framePos = new Vector2((float)(Main.wingsTexture[drawPlayer.wings].Width / 2), (float)(Main.wingsTexture[drawPlayer.wings].Height / 8)); }
                    Vector2 pos = (wings ? new Vector2((float)((int)(edi.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(9 * drawPlayer.direction))), (float)((int)(edi.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir))) : new Vector2((float)((int)(edi.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(edi.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))));
                    if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default(Vector2) : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.spriteEffects, 0);
                }
            }
        }

        public PlayerLayer glAfterWings = new PlayerLayer("AAMod", "glAfterWings", PlayerLayer.Wings, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            int accSlot = 0;
            bool social = false;
            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterJetpack"), ref social, ref accSlot))
            {
                int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1) dye = 0;
                DrawWingGlow(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/DarkmatterJetpack_Wings_Glow"), dye, drawPlayer);
                //BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Accessories/Wings/DarkmatterJetpack_Wings_Glow"), edi.wingShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), frame);
            }
            else
            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("DraconianWings"), ref social, ref accSlot))
            {
                int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1) dye = 0;
                DrawWingGlow(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/DraconianWings_Wings_Glow"), dye, drawPlayer);

            }
        });
    }
}
