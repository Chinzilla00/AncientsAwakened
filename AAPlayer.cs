using System.IO;
using System.Linq;
using AAMod.Buffs;
using AAMod.Items.Dev;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.Akuma;
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
        // Biome bools.
        public bool ZoneMire = false;
        public bool ZoneInferno = false;
        public bool ZoneVoid = false;
        public bool ZoneMush = false;
        public bool ZoneRisingSunPagoda = false;
        public bool ZoneRisingMoonLake = false;
        public bool VoidUnit = false;
        public bool SunAltar = false;
        public bool MoonAltar = false;
        public bool AkumaAltar = false;
        public bool YamataAltar = false;
        public bool AshCurse;
        public int VoidGrav = 0;
        public static int Ashes = 0;
        // Armor bools.
        public bool steelSet;
        public bool goblinSlayer;
        public bool IsGoblin;
        public bool leatherSet;
        public bool silkSet;
        public bool roseSet;
        public bool mushiumSet;
        public bool kindledSet;
        public bool depthSet;
        public bool impSet;
        public bool DynaskullSet;
        public bool fleshrendSet;
        public bool nightsSet;
        public bool deathlySet;
        public bool tribalSet;
        public bool trueHallow;
        public bool trueNights;
        public bool trueFlesh;
        public bool trueTribal;
        public bool trueDeathly;
        public bool trueDemon;
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool dracoSet;
        public bool dreadSet;
        public bool zeroSet;
        public bool valkyrieSet;
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
        public int SnapCD = 18000;
        public bool death;
        public bool AshRemover;
        public bool FogRemover;
        public bool Baolei;
        public bool Naitokurosu;
        public bool DragonShell;
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
        //buffs

        //pets
        public bool Broodmini = false;
        public bool Raidmini = false;
        public bool MiniProbe = false;
        public bool Sharkron = false;
        public bool RoyalKitten = false;

        //NPCcount

        public static int yamata = -1;

        //Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);

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
            //Biome
            //Armor
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            fleshrendSet = false;
            goblinSlayer = false;
            tribalSet = false;
            trueTribal = false;
            impSet = false;
            trueDemon = false;
            trueDeathly = false;
            DynaskullSet = false;
            zeroSet = false;
            dracoSet = false;
            dreadSet = false;
            darkmatterSetMe = false;
            darkmatterSetRa = false;
            darkmatterSetMa = false;
            darkmatterSetSu = false;
            darkmatterSetTh = false;
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
            Naitokurosu = false;
            AshCurse = !Main.dayTime && ((!AAWorld.downedAkuma && !Main.expertMode) || (!AAWorld.downedAkumaA && Main.expertMode));
            //Debuffs
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Clueless = false;
            //Buffs
            //Pets
            Broodmini = false;
            Raidmini = false;
            MiniProbe = false;
            Sharkron = false;
            RoyalKitten = false;
            //EnemyChecks
            IsGoblin = false;
        }

        public override void UpdateBiomes()
        {
            ZoneMire = (AAWorld.mireTiles > 100) || NPC.AnyNPCs(mod.NPCType<Yamata>()) || NPC.AnyNPCs(mod.NPCType<YamataA>());
            ZoneInferno = (AAWorld.infernoTiles > 100) || (NPC.AnyNPCs(mod.NPCType<Akuma>()) || NPC.AnyNPCs(mod.NPCType<AkumaA>()));
            ZoneMush = (AAWorld.mushTiles > 100);
            ZoneVoid = (AAWorld.voidTiles > 20) || (NPC.AnyNPCs(mod.NPCType<Zero>()) || NPC.AnyNPCs(mod.NPCType<ZeroAwakened>()));
        }

        public override void UpdateBiomeVisuals()
        {

            bool useAkuma = NPC.AnyNPCs(mod.NPCType<AkumaA>()) || AkumaAltar;
            player.ManageSpecialBiomeVisuals("AAMod:AkumaSky", useAkuma);
            player.ManageSpecialBiomeVisuals("HeatDistortion", useAkuma);
            bool useYamata = NPC.AnyNPCs(mod.NPCType<YamataA>()) || YamataAltar;
            player.ManageSpecialBiomeVisuals("AAMod:YamataSky", useYamata);
            bool useInferno = (ZoneInferno || SunAltar) && !useAkuma;
            player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", useInferno);
            player.ManageSpecialBiomeVisuals("HeatDistortion", useInferno);
            bool useMire = (ZoneMire || MoonAltar) && !useYamata;
            player.ManageSpecialBiomeVisuals("AAMod:MireSky", useMire);
            bool useVoid = ZoneVoid || VoidUnit;
            player.ManageSpecialBiomeVisuals("AAMod:VoidSky", useVoid);
            bool useFog = !FogRemover && (Main.dayTime && ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode))) && ZoneMire;
            player.ManageSpecialBiomeVisuals("Fog", useFog);
        }

        public override bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>(mod);
            return (ZoneMire == modOther.ZoneMire && ZoneInferno == modOther.ZoneInferno && ZoneVoid == modOther.ZoneVoid && ZoneMush == modOther.ZoneMush);
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>(mod);
            modOther.ZoneInferno = ZoneInferno;
            modOther.ZoneMire = ZoneMire;
            modOther.ZoneVoid = ZoneVoid;
            modOther.ZoneMush = ZoneMush;
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
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            byte flags = reader.ReadByte();
            ZoneInferno = ((flags & 1) == 1);
            ZoneMire = ((flags & 2) == 2);
            ZoneVoid = ((flags & 3) == 3);
            ZoneMush = ((flags & 4) == 4);
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
            //crate chance
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
                if (liquidType == 0 && player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
                {
                    caughtType = mod.ItemType("InfernoCrate");
                }
                if ((liquidType == 0 || liquidType == 1)  && player.GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    caughtType = mod.ItemType("IceCrate");
                }
                else if (liquidType == 1 && ItemID.Sets.CanFishInLava[fishingRod.type] && player.ZoneUnderworldHeight)
                {
                    caughtType = mod.ItemType("HellCrate");
                }
            }
        }

        public override void PostUpdate()
        {
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                if (Main.dayTime && ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode)))
                {
                    if (!player.GetModPlayer<AAPlayer>(mod).FogRemover)
                    {
                        player.AddBuff(mod.BuffType<Clueless>(), 5);
                    }
                }
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
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (AshCurse)
                {
                    AshRain(player, mod);
                }
            }
        }

        public override void PreUpdate()
        {
            

            if ((Mind || Power || Reality || Soul || Space || Time) && (!dwarvenGauntlet && !InfinityGauntlet && !TrueInfinityGauntlet))
            {
                player.AddBuff(mod.BuffType<InfinityOverload>(), 180);
            }
            if (player.GetModPlayer<AAPlayer>().ZoneVoid || player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
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
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno && player.GetModPlayer<AAPlayer>(mod).AshCurse)
            {
                if (!player.GetModPlayer<AAPlayer>(mod).AshRemover)
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
                                    Main.dust[num9].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[num9].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;
                                    Dust expr_292_cp_0 = Main.dust[num9];
                                    expr_292_cp_0.velocity.Y = expr_292_cp_0.velocity.Y * Main.dust[num9].scale;
                                    if (!player.GetModPlayer<AAPlayer>(mod).AshCurse)
                                    {
                                        Main.dust[num9].velocity.X = Main.windSpeed + Main.rand.Next(-10, 10) * 0.1f;
                                        Dust expr_2EC_cp_0 = Main.dust[num9];
                                        expr_2EC_cp_0.velocity.X = expr_2EC_cp_0.velocity.X + Main.windSpeed * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[num9].velocity.X = (float)Math.Sqrt(Math.Abs(Main.windSpeed)) * Math.Sign(Main.windSpeed) * (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;
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
        


        public void YamataSnag()
        {
            if (yamata >= 0 && Main.npc[yamata].active)
            {
                float num = Main.npc[yamata].position.X + 40f;
                if (Main.npc[yamata].direction > 0)
                {
                    num -= 96f;
                }
                if (player.position.X + player.width > num && player.position.X < num + 140f && player.gross)
                {
                    player.noKnockback = false;
                    player.Hurt(PlayerDeathReason.LegacyDefault(), 50, Main.npc[yamata].direction, false, false, false, -1);
                }
                if (!player.gross && player.position.Y > (float)((Main.maxTilesY - 250) * 16) && player.position.X > num - 1920f && player.position.X < num + 1920f)
                {
                    player.AddBuff(37, 10, true);
                    Main.PlaySound(4, (int)Main.npc[yamata].position.X, (int)Main.npc[yamata].position.Y, 10, 1f, 0f);
                }
                if (player.gross)
                {
                    if (player.position.Y < (float)((Main.maxTilesY - 200) * 16))
                    {
                        player.AddBuff(38, 10, true);
                    }
                    if (Main.npc[yamata].direction < 0)
                    {
                        if (player.position.X + (float)(player.width / 2) > Main.npc[yamata].position.X + (float)(Main.npc[yamata].width / 2) + 40f)
                        {
                            player.AddBuff(38, 10, true);
                        }
                    }
                    else if (player.position.X + (float)(player.width / 2) < Main.npc[yamata].position.X + (float)(Main.npc[yamata].width / 2) - 40f)
                    {
                        player.AddBuff(38, 10, true);
                    }
                }
                if (Snagged)
                {
                    player.controlHook = false;
                    player.controlUseItem = false;
                    for (int i = 0; i < 1000; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].aiStyle == 7)
                        {
                            Main.projectile[i].Kill();
                        }
                    }
                    Vector2 center = player.Center;
                    float num2 = Main.npc[yamata].position.X + (float)(Main.npc[yamata].width / 2) - center.X;
                    float num3 = Main.npc[yamata].position.Y + (float)(Main.npc[yamata].height / 2) - center.Y;
                    float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
                    if (num4 > 3000f)
                    {
                        player.KillMe(PlayerDeathReason.ByOther(11), 1000.0, 0, false);
                        return;
                    }
                    if (Main.npc[yamata].position.X < 608f || Main.npc[yamata].position.X > (float)((Main.maxTilesX - 38) * 16))
                    {
                        player.KillMe(PlayerDeathReason.ByOther(12), 1000.0, 0, false);
                    }
                }
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
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
            // clone.someLocalVariable = someLocalVariable;
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
            if (InfinityGauntlet || TrueInfinityGauntlet)
            {
                if (AAMod.InfinityHotKey.JustPressed && SnapCD == 0)
                {
                    SnapCD = 18000;
                    Main.npc.Where(x => x.active && !x.townNPC && x.type != NPCID.TargetDummy && x.type != mod.NPCType<CrabGuardian>() && x.type != mod.NPCType<RiftShredder>() && x.type != mod.NPCType<Taser>() && x.type != mod.NPCType<RealityCannon>() && x.type != mod.NPCType<VoidStar>() && x.type != mod.NPCType<TeslaHand>() && !x.boss).ToList().ForEach(x =>
                    {

                        Main.NewText("Perfectly Balanced, as all things should be");
                        if (death || TrueInfinityGauntlet)
                        {
                            player.ApplyDamageToNPC(x, damage: x.lifeMax, knockback: 0f, direction: 0, crit: true);
                            death = false;
                        }
                        else
                        {
                            death = true;
                        }
                    });
                }
            }
            if (SnapCD != 0)
            {
                SnapCD--;
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
                player.magicDamage -= 10;
                player.minionDamage -= 10;
                player.meleeDamage -= 10;
                player.thrownDamage -= 10;
                player.rangedDamage -= 10;
            }
            if (hydraToxin)
            {
                foreach (Tile tile in Main.tile)
                {
                    if (tile.collisionType == player.whoAmI)
                    {
                        player.velocity.X = (player.velocity.X / 16) * 15;
                        player.velocity.Y = (player.velocity.Y / 16) * 15;
                    }
                }
            }
        }

        public override void UpdateDead()
        {
            infinityOverload = false;
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
        }
        

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

            if (Baolei && (proj.melee || proj.magic) && Main.rand.Next(2) == 0)
            {
                if (!Main.dayTime)
                {

                }
                if (Main.dayTime && Main.time < 23400 && Main.time > 30600)
                {
                    target.AddBuff(BuffID.OnFire, 1000);
                }
                if (Main.dayTime && Main.time >= 23400 && Main.time <= 30600)
                {
                    target.AddBuff(BuffID.Daybreak, 1000);
                }
            }

            if (Naitokurosu && (proj.ranged || proj.thrown) && Main.rand.Next(2) == 0)
            {
                if (Main.dayTime)
                {

                }
                if (!Main.dayTime && Main.time < 14400 && Main.time > 21600)
                {
                    target.AddBuff(BuffID.Venom, 1000);
                }
                if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
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
                player.ApplyDamageToNPC(target, 40, 0, 0, false);
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
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneMire)
            {
                return mod.GetTexture("Map/MireMap");
            }
            if (ZoneInferno)
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

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman) { return false; }
            Item item = ItemLoader.GetItem(type).item;
            if (item.headSlot > 0) return BaseMod.BasePlayer.HasHelmet(player, type) && BaseMod.BaseDrawing.ShouldDrawHelmet(player, type);
            else if (item.bodySlot > 0) return BaseMod.BasePlayer.HasChestplate(player, type) && BaseMod.BaseDrawing.ShouldDrawChestplate(player, type);
            else if (item.legSlot > 0) return BaseMod.BasePlayer.HasLeggings(player, type) && BaseMod.BaseDrawing.ShouldDrawLeggings(player, type);
            else if (item.accessory) return BaseMod.BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseMod.BaseDrawing.ShouldDrawAccessory(player, type);
            return false;
        }
        public static bool ShouldDrawArmSkin(Player drawPlayer, int type)
        {
            return BaseMod.BasePlayer.HasChestplate(drawPlayer, type, true) && BaseMod.BaseDrawing.ShouldDrawChestplate(drawPlayer, type);
        }
        public static Rectangle GetFrame(Player player, int itemtype, int count, int width, int height)
        {
            return BaseMod.BaseDrawing.GetFrame(count, width, height, 0, 2);
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
            BaseMod.BaseDrawing.AddPlayerLayer(list, glAfterHead, PlayerLayer.Head, false);
            BaseMod.BaseDrawing.AddPlayerLayer(list, glAfterBody, PlayerLayer.Body, false);
            BaseMod.BaseDrawing.AddPlayerLayer(list, glAfterArm, PlayerLayer.Arms, false);
            BaseMod.BaseDrawing.AddPlayerLayer(list, glAfterLegs, PlayerLayer.Legs, false);
            BaseMod.BaseDrawing.AddPlayerLayer(list, glAfterWings, PlayerLayer.Wings, true);
        }

        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> list)
        {
            BaseMod.BaseDrawing.AddPlayerHeadLayer(list, glAfterHead2, PlayerHeadLayer.Head, false);
        }
        
        public PlayerLayer glAfterHead = new PlayerLayer("AAMod", "glAfterHead", PlayerLayer.Head, delegate (PlayerDrawInfo edi)
        {
            DrawAfterHead(edi, default(PlayerHeadDrawInfo), false);
        });

        public PlayerHeadLayer glAfterHead2 = new PlayerHeadLayer("AAMod", "glAfterHead2", PlayerHeadLayer.Head, delegate (PlayerHeadDrawInfo edhi)
        {
            try
            {
                DrawAfterHead(default(PlayerDrawInfo), edhi, true);
            }
            catch (Exception e) { BaseMod.BaseUtility.Chat("PLAYER HEAD DRAW ERROR: " + e.Message, Color.Red, true); ErrorLogger.Log(e.Message); ErrorLogger.Log(e.StackTrace); ErrorLogger.Log("-------"); }
        });

        public static Color GetItemColor(Player drawPlayer, Vector2 position)
        {
            if (drawPlayer == null || position == default(Vector2) || drawPlayer.selectedItem < 0 || drawPlayer.selectedItem >= drawPlayer.inventory.Length || drawPlayer.inventory[drawPlayer.selectedItem] == null) return Color.White;
            Color c = Lighting.GetColor((int)((position.X + drawPlayer.width * 0.5f) / 16f), (int)((position.Y + drawPlayer.height * 0.5f) / 16f));
            return drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(c);
        }

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
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DracoHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayHelmet")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DoomsdayHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterVisor")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterVisor_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelm")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelm_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHelmet")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHelmet_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterHeaddress")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterHeaddress_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/DarkmatterMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHat")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHat_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelm")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelm_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHelmet")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHelmet_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumHeadgear")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumHeadgear_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RadiumMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Items/Armor/Radium/RadiumMask_Head"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("GripMaskRed")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/GripMaskRed_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("DaybringerMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/Daybringer_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("NightcrawlerMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/NightcrawlerMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("RetrieverMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/RetrieverMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }
            else if (!mapHead && HasAndCanDraw(drawPlayer, mod.ItemType("ZeroMask")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(drawObj, mod.GetTexture("Glowmasks/ZeroMask_Head_Glow"), dyeHead, drawPlayer, Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame, scale);
            }

        }
        public PlayerLayer glAfterBody = new PlayerLayer("AAMod", "glAfterBody", PlayerLayer.Body, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Body_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Body"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
        });
        public PlayerLayer glAfterArm = new PlayerLayer("AAMod", "glAfterArm", PlayerLayer.Arms, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoPlate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoPlate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayChestplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayChestplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterBreastplate_Arms_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterBreastplate")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Armor/Radium/RadiumPlatemail_Arms"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
        }); 
        public PlayerLayer glAfterLegs = new PlayerLayer("AAMod", "glAfterLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo edi)
        {
            Mod mod = AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            if (HasAndCanDraw(drawPlayer, mod.ItemType("DracoLeggings")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DracoLeggings_Legs_Glow"), edi.legArmorShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.legFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DoomsdayLeggings")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DoomsdayLeggings_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterGreaves")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Glowmasks/DarkmatterGreaves_Legs_Glow"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
            }
            else if (HasAndCanDraw(drawPlayer, mod.ItemType("RadiumCuisses")))
            {
                BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("GItems/Armor/Radium/RadiumCuisses_Legs"), edi.bodyArmorShader, drawPlayer, edi.position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), drawPlayer.bodyFrame);
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
                    BaseMod.BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseMod.BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
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
            Mod mod =AAMod.instance;
            Player drawPlayer = edi.drawPlayer;
            int accSlot = 0;
            bool social = false;
            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("DarkmatterJetpack"), ref social, ref accSlot))
            {
                int dye = BaseMod.BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1) dye = 0;
                DrawWingGlow(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/DarkmatterJetpack_Wings_Glow"), dye, drawPlayer);
                //BaseMod.BaseDrawing.DrawPlayerTexture(Main.playerDrawData, mod.GetTexture("Items/Accessories/Wings/DarkmatterJetpack_Wings_Glow"), edi.wingShader, drawPlayer, edi.position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, edi.shadow), frame);
            }
            else
            if (edi.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, mod.ItemType("DraconianWings"), ref social, ref accSlot))
            {
                int dye = BaseMod.BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                if (dye == -1) dye = 0;
                DrawWingGlow(1, Main.playerDrawData, edi, mod.GetTexture("Glowmasks/DraconianWings_Wings_Glow"), dye, drawPlayer);
                
            }
        });
    }
}
