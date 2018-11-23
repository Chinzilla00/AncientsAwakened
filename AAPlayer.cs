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

        //buffs

        //pets
        public bool Broodmini = false;
        public bool Raidmini = false;
        public bool MiniProbe = false;
        public bool Sharkron = false;
        public bool RoyalKitten = false;

        //Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);

        public override void ResetEffects()
        {
            AshRemover = false;
            FogRemover = false;
            clawsOfChaos = false;
            demonGauntlet = false;
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            fleshrendSet = false;
            enderMinion = false;
            enderMinionEX = false;
            ChairMinion = false;
            ChairMinionEX = false;
            BabyPhoenix = false;
            LungMinion = false;
            DragonMinion = false;
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
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
            Broodmini = false;
            Raidmini = false;
            MiniProbe = false;
            Sharkron = false;
            Baolei = false;
            Naitokurosu = false;
            RoyalKitten = false;
            AshCurse = !Main.dayTime && ((!AAWorld.downedAkuma && !Main.expertMode) || (!AAWorld.downedAkumaA && Main.expertMode));
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
            bool useFogless = FogRemover && (Main.dayTime && ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode))) && ZoneMire;
            player.ManageSpecialBiomeVisuals("Fog", useFog);
            player.ManageSpecialBiomeVisuals("Fogless", useFogless);
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
            if (DragonShell)
            {
                npc.AddBuff(BuffID.Daybreak, 300);
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
                target.AddBuff(BuffID.Electrified, 180);
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
                target.AddBuff(BuffID.Electrified, 180);
            }

            if (darkmatterSetRa && proj.ranged && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Electrified, 180);
            }

            if (darkmatterSetMa && proj.magic && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Electrified, 180);
            }

            if (darkmatterSetSu && proj.minion && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Electrified, 180);
            }

            if (darkmatterSetTh && proj.thrown && Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Electrified, 180);
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
    }
}
