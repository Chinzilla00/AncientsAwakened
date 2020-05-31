using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using AAMod.NPCs.Bosses.Shen;
using AAMod.NPCs.Bosses.Rajah;
using AAMod.NPCs.Enemies.Terrarium.PreHM;
using AAMod.NPCs.Enemies.Terrarium.Hardmode;
using AAMod.NPCs.Enemies.Terrarium.PostPlant;
using AAMod.NPCs.Bosses.Serpent;
using AAMod.NPCs.Enemies.Snow;
using AAMod.NPCs.Enemies.Sky;
using AAMod.NPCs.Enemies.Cavern;
using AAMod.Items.Currency;
using System;
using Terraria.Localization;
using log4net;
using AAMod.Items.Materials;
using AAMod.Items.Pets;
using AAMod.Items.Accessories;
using AAMod.Items.Armor.AncientGold;
using AAMod.Items.Melee;
using AAMod.Items.Ranged.Ammo;
using AAMod.Items.Ranged;
using AAMod.Items.Summoning;
using AAMod.Items.BossSummons;
using AAMod.Items.Usable;
using AAMod.Dusts;
using AAMod.NPCs.Enemies.Other;
using AAMod.NPCs.Bosses.Wyrmling;
using AAMod.NPCs.Enemies.Inferno;
using AAMod.NPCs.Bosses.Wyrm;
using AAMod.NPCs.Enemies.Mire;
using AAMod.NPCs.Enemies.Mire.Toxitoad;
using AAMod.NPCs.Enemies.Void;
using AAMod.NPCs.Critters;
using AAMod.Misc;

namespace AAMod.Globals
{
    public class AAModGlobalNPC : GlobalNPC
    {
        //debuffs
        public bool CursedHellfire = false;
        public bool TimeFrozen = false;
        public bool infinityOverload = false;
        public bool terraBlaze = false;
        public bool Hydratoxin = false;
        public bool Moonraze = false;
        public bool Electrified = false;
        public bool InfinityScorch = false;
        public bool irradiated = false;
        public bool DiscordInferno = false;
        public bool riftBent = false;
        public bool BrokenArmor = false;
        public bool DynaEnergy1 = false;
        public bool DynaEnergy2 = false;
        public bool Spear = false;
        public bool AssassinHurt = false;
        public bool FFlames = false;

        public static int Toad = -1;
        public static int Rose = -1;
        public static int Brain = -1;
        public static int Rajah = -1;

        public override bool InstancePerEntity => true;

        public bool IsBunny(NPC npc)
        {
            return npc.type == NPCID.Bunny || npc.type == NPCID.GoldBunny || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.PartyBunny;
        }

        public override void ResetEffects(NPC npc)
        {
            CursedHellfire = false;
            infinityOverload = false;
            terraBlaze = false;
            TimeFrozen = false;
            Hydratoxin = false;
            Moonraze = false;
            Electrified = false;
            InfinityScorch = false;
            DiscordInferno = false;
            irradiated = false;
            riftBent = false;
            BrokenArmor = false;
            DynaEnergy1 = false;
            DynaEnergy2 = false;
            Spear = false;
            AssassinHurt = false;
            FFlames = false;
        }

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc); 

            if (NPCID.Sets.TownCritter[npc.type])
            {
                npc.dontTakeDamageFromHostiles = true;
            }

            if (IsBunny(npc) && AAWorld.downedRajahsRevenge)
            {
                npc.dontTakeDamage = true;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.type == NPCID.KingSlime || npc.type == NPCID.Plantera || 
                npc.type == ModContent.NPCType<SerpentBody>() || npc.type == ModContent.NPCType<SerpentHead>() || npc.type == ModContent.NPCType<SerpentTail>() ||
                npc.type == ModContent.NPCType<SnakeHead>() || npc.type == ModContent.NPCType<SnakeBody>() || npc.type == ModContent.NPCType<SnakeBody2>() || npc.type == ModContent.NPCType<SnakeTail>())
            {
                ApplyDPSDebuff(npc.onFire, 20, ref npc.lifeRegen);
            }

            if (DiscordInferno)
            {
                npc.damage -= 10;

                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= (int)npc.velocity.X * (npc.velocity.X > 0?  1 : -1)  + 52;
            }

            if (BrokenArmor)
            {
                npc.defense *= (int).8f;
            }

            if(AssassinHurt)
			{
				npc.defense -= 20;
			}

            bool shen = npc.type == ModContent.NPCType<Shen>() || npc.type == ModContent.NPCType<ShenA>();

            ApplyDPSDebuff(terraBlaze, shen ? 46 : 26, shen ? 30 : 10, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(infinityOverload, 60, 40, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(Spear, 5, 5, ref npc.lifeRegen, ref damage);
            ApplyDPSDebuff(InfinityScorch, 80, 40, ref npc.lifeRegen, ref damage);

            ApplyDPSDebuff(CursedHellfire, 30, ref npc.lifeRegen);
            ApplyDPSDebuff(Moonraze, 100, ref npc.lifeRegen);
            ApplyDPSDebuff(Hydratoxin, (int)npc.velocity.X * (npc.velocity.X > 0?  1 : -1) / 2, ref npc.lifeRegen);
            ApplyDPSDebuff(Electrified, 40, ref npc.lifeRegen);
            if(npc.lifeMax > 0) ApplyDPSDebuff(FFlames, 40 * (npc.life / npc.lifeMax), ref npc.lifeRegen);
        }

        public void ApplyDPSDebuff(bool debuff, int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (debuff)
            {
                if (lifeRegen > 0)
                {
                    lifeRegen = 0;
                }

                lifeRegen -= lifeRegenValue;

                if (damage < damageValue)
                {
                    damage = damageValue;
                }
            }
        }

        public void ApplyDPSDebuff(bool debuff, int lifeRegenValue, ref int lifeRegen)
        {
            if (debuff)
            {
                if (lifeRegen > 0)
                {
                    lifeRegen = 0;
                }

                lifeRegen -= lifeRegenValue;
            }
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.type >= NPCID.Golem && npc.type <= NPCID.GolemFistRight)
            {
                if (item.pick > 0)
                {
                    damage = item.damage + item.pick;
                }
                else
                {
                    npc.defense = 20;
                }
            }
        }

        public override bool PreAI(NPC npc)
        {
            if(npc.type != NPCID.MartianSaucerCore && (npc.boss || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail))
            {
                Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().bossactive = true;
            }
            return base.PreAI(npc);
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if(AssassinHurt)
			{
				damage *= 1.1f;
			}
			return true;
		}

        internal ILog Logging = LogManager.GetLogger("AAMod");

        public override void NPCLoot(NPC npc)
        {
            switch (npc.type) 
            {
                default:
                    break;
                case NPCID.GoblinSummoner:
                    DownedBools.downedGobSummoner = true;
                    break;
                case NPCID.DD2OgreT2:
                    DownedBools.downedOgre = true;
                    break;
                case NPCID.DD2Betsy:
                    DownedBools.downedBetsy = true;
                    break;
                case NPCID.Mothron:
                    DownedBools.downedMoth = true;
                    break;
                case NPCID.FireImp:
                    npc.DropLoot(ModContent.ItemType<DevilSilk>(), Main.rand.Next(2, 3));
                    break;
                case NPCID.RedDevil:
                    if (Main.rand.Next(3) == 0)
                    {
                        npc.DropLoot(ModContent.ItemType<PureEvil>());
                    }
                    break;
                case NPCID.Demon:
                    npc.DropLoot(ModContent.ItemType<DevilSilk>(), Main.rand.Next(4, 5));
                    break;
                case NPCID.VoodooDemon:
                    npc.DropLoot(ModContent.ItemType<DevilSilk>(), Main.rand.Next(5, 6));
                    break;
                case NPCID.Plantera:
                    npc.DropLoot(ModContent.ItemType<PlanteraPetal>(), Main.rand.Next(30, 40));
                    npc.DropLoot(ItemID.ChlorophyteOre, Main.rand.Next(50, 80));
                    break;
                case NPCID.DukeFishron:
                    if (Main.rand.NextFloat() < 0.1f)
                    {
                        npc.DropLoot(ModContent.ItemType<Seashroom>());
                    }
                    break;
                case NPCID.EnchantedSword:
                    if (Main.rand.NextFloat() < 0.1f)
                    {
                        npc.DropLoot(ItemID.Excalibur);
                    }
                    break;
                case NPCID.CrimsonAxe:
                    if (Main.rand.NextFloat() < 0.1f)
                    {
                        npc.DropLoot(ItemID.BloodLustCluster);
                    }
                    break;
                case NPCID.CursedHammer:
                    if (Main.rand.NextFloat() < 0.1f)
                    {
                        npc.DropLoot(ModContent.ItemType<ShadowBand>());
                    }
                    break;
                case NPCID.LostGirl:
                case NPCID.Nymph:
                case NPCID.DoctorBones:
                    if (Main.rand.NextBool(20) && Main.expertMode)
                    {
                        npc.DropLoot(ModContent.ItemType<AncientGoldLeg>());
                    }
                    break;
                case NPCID.Tim:
                case NPCID.RuneWizard:
                    if (Main.rand.NextBool(20) && Main.expertMode)
                    {
                        npc.DropLoot(ModContent.ItemType<AncientGoldBody>());
                    }
                    break;
                case NPCID.EyeofCthulhu:
                    if (Main.rand.NextBool(4))
                    {
                        npc.DropLoot(ModContent.ItemType<CthulhusBlade>());
                    }
                    break;
                case NPCID.AngryBones:
                case NPCID.AngryBonesBig:
                case NPCID.AngryBonesBigHelmet:
                case NPCID.AngryBonesBigMuscle:
                    if (Main.rand.NextFloat() < 0.01f)
                    {
                        npc.DropLoot(ModContent.ItemType<AncientPoker>());
                    }
                    break;
                case NPCID.Probe:
                    npc.DropLoot(ModContent.ItemType<Energy_Cell>(), Main.rand.Next(3, 12));
                    break;
                case NPCID.TheDestroyer:
                    npc.DropLoot(ModContent.ItemType<Energy_Cell>(), Main.rand.Next(8, 16));

                    if (Main.rand.NextFloat() < .34f)
                    {
                        npc.DropLoot(ModContent.ItemType<Laser_Rifle>());
                    }
                    break;
                case NPCID.SkeletronPrime:
                    npc.DropLoot(ModContent.ItemType<Energy_Cell>(), Main.rand.Next(8, 16));

                    if (Main.rand.NextFloat() < .34f)
                    {
                        npc.DropLoot(ModContent.ItemType<Laser_Rifle>());
                    }
                    break;
                case NPCID.WallofFlesh:
                    if (Main.rand.NextFloat() < .1f)
                    {
                        npc.DropLoot(ModContent.ItemType<HK_MP5>());
                    }
                    break;
                case NPCID.MartianSaucerCore:
                    if (Main.rand.NextFloat() < .12f)
                    {
                        npc.DropLoot(ModContent.ItemType<Alien_Rifle>());
                    }

                    if (Main.rand.NextFloat() < .03f)
                    {
                        npc.DropLoot(ModContent.ItemType<Energy_Conduit>());
                    }
                    break;
                case NPCID.CursedSkull:
                    if (Main.rand.NextFloat() < .12f)
                    {
                        npc.DropLoot(ModContent.ItemType<SkullStaff>());
                    }
                    break;
                case NPCID.Vulture:
                    npc.DropLoot(ModContent.ItemType<vulture_feather>(), Main.rand.Next(1, 3));
                    break;
                case NPCID.Drippler:
                    if (Main.rand.NextFloat() < .005f)
                    {
                        npc.DropLoot(ModContent.ItemType<BloodyMary>());
                    }
                    break;
                case NPCID.TacticalSkeleton:
                case NPCID.SkeletonCommando:
                case NPCID.SkeletonSniper:
                    if (Main.rand.Next(50) == 0)
                    {
                        npc.DropLoot(ModContent.ItemType<M79Parts>());
                    }
                    break;
                case NPCID.QueenBee:
                    if (Main.rand.NextFloat() < .01f)
                    {
                        npc.DropLoot(ModContent.ItemType<BugSwatter>());
                    }
                    npc.DropLoot(ItemID.Stinger, Main.rand.Next(14, 20));
                    break;
                case NPCID.SkeletronHand:
                    npc.DropLoot(ItemID.Bone, Main.rand.Next(4, 8));
                    break;
                case NPCID.SkeletronHead:
                    npc.DropLoot(ItemID.Bone, Main.rand.Next(30, 45));
                    break;
                case NPCID.ArmoredViking:
                case NPCID.UndeadViking:
                    if (NPC.downedBoss3 && Main.rand.Next(3) == 0)
                    {
                        npc.DropLoot(ModContent.ItemType<VikingRelic>(), Main.rand.Next(1, 3));
                    }
                    break;
                case NPCID.GoldBunny:
                    if (NPC.downedGolemBoss)
                    {
                        npc.DropLoot(ModContent.ItemType<GoldenCarrot>());
                    }
                    break;
            }

            if (DynaEnergy1)
            {
                Projectile.NewProjectile(npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.DynaEnergy>(), 60, 1, Main.myPlayer);
            }

            if (DynaEnergy2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.DynaEnergy>(), 60, 1, Main.myPlayer);
                }
            }

            if (Main.rand.NextBool(8192))
            {
                npc.DropLoot(ModContent.ItemType<ShinyCharm>());
            }

            if (AASets.Goblins[npc.type] && NPC.downedGoblins)
            {
                if (Main.rand.NextBool(20))
                {
                    npc.DropLoot(ModContent.ItemType<GoblinSoul>());
                }
            }

            if (IsBunny(npc) && NPC.downedGolemBoss)
            {
                if (Main.rand.NextBool(80))
                {
                    npc.DropLoot(ModContent.ItemType<GoldenCarrot>());
                }
            }

            if (Main.hardMode)
            {
                Player player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
                if (player.GetModPlayer<AAPlayer>().ZoneMire && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.NextBool(5))
                    {
                        npc.DropLoot(ModContent.ItemType<SoulOfSpite>());
                    }
                }

                if (player.GetModPlayer<AAPlayer>().ZoneInferno && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.NextBool(5))
                    {
                        npc.DropLoot(ModContent.ItemType<SoulOfSmite>());
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    if (Main.rand.NextBool(2500))
                    {
                        npc.DropLoot(ModContent.ItemType<MireKey>());
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    if (Main.rand.NextBool(2500))
                    {
                        npc.DropLoot(ModContent.ItemType<InfernoKey>());
                    }
                }
                if (player.GetModPlayer<AAPlayer>().ZoneVoid)
                {
                    if (Main.rand.NextBool(1250))
                    {
                        npc.DropLoot(ModContent.ItemType<DoomstopperKey>());
                    }
                }
                if (player.GetModPlayer<AAPlayer>().Terrarium && NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(100))
                    {
                        npc.DropLoot(ModContent.ItemType<TerraCrystal>());
                    }
                }

                if ((player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneMire) && NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(100))
                    {
                        npc.DropLoot(ModContent.ItemType<ChaosCrystal>());
                    }
                }
            }


            if (Main.hardMode && IsBunny(npc) && Rajah != -1)
            {
                Player player = Main.player[Player.FindClosest(npc.Center, npc.width, npc.height)];

                int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                if (bunnyKills % 100 == 0 && bunnyKills < 1000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.RajahGlobalInfo1"), 107, 137, 179);
                    }

                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, true, new Vector2(npc.Center.X, npc.Center.Y - 2000), Language.GetTextValue("Mods.AAMod.Common.RajahRabbit"));

                }

                if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.RajahGlobalInfo2") + player.name.ToUpper() + "!", 107, 137, 179);
                    }

                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, true, new Vector2(npc.Center.X, npc.Center.Y - 2000), Language.GetTextValue("Mods.AAMod.Common.RajahRabbit"));
                }

                if (bunnyKills % 50 == 0 && bunnyKills % 100 != 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.RajahGlobalInfo3"), 107, 137, 179);
                    }
                }
            }

            if (Main.bloodMoon)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<BloodRune>());
            }

            if ((npc.type >= NPCID.PirateDeckhand && npc.type <= NPCID.PirateCaptain) || npc.type == NPCID.Parrot || npc.type == NPCID.PirateShip)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<PirateBooty>());
            }

            if (npc.type == NPCID.Frankenstein || npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat || npc.type == NPCID.SwampThing ||
                npc.type == NPCID.CreatureFromTheDeep || npc.type == NPCID.Fritz || npc.type == NPCID.Reaper || npc.type == NPCID.ThePossessed ||
                npc.type == NPCID.Mothron || npc.type == NPCID.Butcher || npc.type == NPCID.DeadlySphere || npc.type == NPCID.DrManFly ||
                npc.type == NPCID.Nailhead || npc.type == NPCID.Psycho || npc.type == NPCID.Eyezor)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<MonsterSoul>());
            }

            if ((npc.type >= NPCID.PirateDeckhand && npc.type <= NPCID.PirateCaptain) || npc.type == NPCID.Parrot || npc.type == NPCID.PirateShip)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<PirateBooty>());
            }

            if ((npc.type >= NPCID.Scarecrow1 && npc.type <= NPCID.HeadlessHorseman) || (npc.type >= NPCID.MourningWood && npc.type <= NPCID.Poltergeist && npc.type !=  NPCID.PumpkingBlade))
            {
                if (Main.rand.Next(8)== 0) npc.DropLoot(ModContent.ItemType<HalloweenTreat>());
            }

            if (npc.type >= NPCID.ZombieElf && npc.type <= NPCID.Krampus)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<ChristmasCheer>());
            }

            if (npc.type >= NPCID.BrainScrambler && npc.type <= NPCID.MartianSaucer && npc.type != NPCID.ForceBubble)
            {
                if (Main.rand.Next(8) == 0) npc.DropLoot(ModContent.ItemType<MartianCredit>());
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            Rectangle hitbox = npc.Hitbox;
            if (FFlames)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<ForsakenDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (CursedHellfire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 75, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (Electrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (infinityOverload)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadB>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.3f, 0.7f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadR>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.2f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadG>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadY>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.5f, 0.5f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadP>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.6f, 0.1f, 0.6f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<InfinityOverloadO>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.8f, 0.5f, 0.1f);
            }

            if (Moonraze)
            {
                int dustCount = Math.Max(1, Math.Min(5, Math.Max(npc.width, npc.height) / 10));
                for (int i = 0; i < dustCount; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, ModContent.DustType<Dusts.Moonraze>(), 0f, 1f, 0);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (DiscordInferno)
            {
                for (int i = 0; i < 8; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, ModContent.DustType<Dusts.Discord>(), 0f, -2.5f, 0);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (Hydratoxin)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Dusts.HydratoxinDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.3f, 0.7f);
            }

            

            if (terraBlaze)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.2f);
            }

            if (InfinityScorch)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, ModContent.DustType<Dusts.VoidDust>(), default, 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.1f);
            }

        }

        public void ClearPoolWithExceptions(IDictionary<int, float> pool)
        {
            try
            {
                Dictionary<int, float> keepPool = new Dictionary<int, float>();
                foreach (var kvp in pool)
                {
                    int npcID = kvp.Key;
                    ModNPC mnpc = NPCLoader.GetNPC(npcID);
                    if (mnpc != null && mnpc.mod != null) //splitting so you can add other exceptions if need be
                    {
                        if (mnpc.mod.Name.Equals("GRealm")) //do not remove GRealm spawns!
                        {
                            keepPool.Add(npcID, kvp.Value);
                        }
                    }
                }
                pool.Clear();

                foreach (var newkvp in keepPool)
                {
                    pool.Add(newkvp.Key, newkvp.Value);
                }

                keepPool.Clear();
            }
            catch (Exception e)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(e.StackTrace);
                }
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneTowerNebula || spawnInfo.player.ZoneTowerSolar || spawnInfo.player.ZoneTowerStardust || spawnInfo.player.ZoneTowerVortex || 
                Main.eclipse || 
                Main.invasionType == InvasionID.MartianMadness ||
                Main.invasionType == InvasionID.CachedPumpkinMoon ||
                Main.invasionType == InvasionID.CachedFrostMoon)
            {
                return;
            }
            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneStars)
            {
                pool.Add(Main.dayTime ? ModContent.NPCType<Sunwatcher>() : ModContent.NPCType<Nightguard>(), .2f);
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                ClearPoolWithExceptions(pool);
                if ((spawnInfo.player.position.Y < (Main.worldSurface * 16.0)) && (Main.dayTime || AAWorld.downedAkuma))
                {
                    pool.Add(ModContent.NPCType<Wyrmling>(), .25f);
                    pool.Add(ModContent.NPCType<InfernalSlime>(), .05f);
                    pool.Add(ModContent.NPCType<Flamebrute>(), .25f);
                    pool.Add(ModContent.NPCType<InfernoSalamander>(), .5f);
                    pool.Add(ModContent.NPCType<NPCs.Enemies.Other.DragonClaw>(), .05f);

                    if (Main.hardMode)
                    {
                        pool.Add(ModContent.NPCType<MagmaSwimmer>(), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(ModContent.NPCType<BlazePhoenix>(), .1f);
                    }

                    if (AAWorld.downedSisters)
                    {
                        pool.Add(ModContent.NPCType<BlazeClaw>(), .05f);
                    }
                }
                else if (spawnInfo.player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(ModContent.NPCType<Wyrmling>(), .25f);
                    pool.Add(ModContent.NPCType<Flamebrute>(), .25f);
                    pool.Add(ModContent.NPCType<InfernoSalamander>(), .5f);
                    pool.Add(ModContent.NPCType<NPCs.Enemies.Other.DragonClaw>(), .05f);

                    if (Main.hardMode)
                    {
                        pool.Add(ModContent.NPCType<MagmaSwimmer>(), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(ModContent.NPCType<Wyrm>(), .008f);
                        pool.Add(ModContent.NPCType<ChaoticDawn>(), .01f);

                        if (spawnInfo.player.ZoneSnow)
                        {
                            pool.Add(ModContent.NPCType<Dragron>(), .01f);
                        }

                        if (spawnInfo.player.ZoneUndergroundDesert)
                        {
                            pool.Add(ModContent.NPCType<InfernoGhoul>(), .1f);
                        }
                    }
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(ModContent.NPCType<Lung>(), .01f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                ClearPoolWithExceptions(pool);
                if ((spawnInfo.player.position.Y < (Main.worldSurface * 16.0)) && (!Main.dayTime || AAWorld.downedYamata))
                {
                    pool.Add(ModContent.NPCType<Mosster>(), .025f);
                    pool.Add(ModContent.NPCType<Newt>(), .05f);
                    pool.Add(ModContent.NPCType<NPCs.Enemies.Other.HydraClaw>(), .025f);
                    pool.Add(ModContent.NPCType<MireSkulker>(), .02f);
                    pool.Add(ModContent.NPCType<MireSlime>(), .025f);

                    if (Main.hardMode)
                    {
                        pool.Add(ModContent.NPCType<FogAngler>(), SpawnCondition.WaterCritter.Chance * 0.05f);
                        pool.Add(ModContent.NPCType<Toxitoad>(), .005f);
                        pool.Add(ModContent.NPCType<Kappa>(), .025f);
                    }

                    if (AAWorld.downedSisters)
                    {
                        pool.Add(ModContent.NPCType<AbyssClaw>(), .01f);
                    }
                }
                else if (spawnInfo.player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(ModContent.NPCType<Mosster>(), .025f);
                    pool.Add(ModContent.NPCType<Newt>(), .05f);
                    pool.Add(ModContent.NPCType<NPCs.Enemies.Other.HydraClaw>(), .025f);
                    pool.Add(ModContent.NPCType<MireSkulker>(), .02f);

                    if (Main.hardMode)
                    {
                        pool.Add(ModContent.NPCType<FogAngler>(), SpawnCondition.WaterCritter.Chance * 0.1f);
                        pool.Add(ModContent.NPCType<Miresquito>(), .025f);
                        pool.Add(ModContent.NPCType<ChaoticTwilight>(), .005f);

                        if (spawnInfo.player.ZoneSnow)
                        {
                            pool.Add(ModContent.NPCType<Miregron>(), .005f);
                        }

                        if (spawnInfo.player.ZoneUndergroundDesert)
                        {
                            pool.Add(ModContent.NPCType<MireGhoul>(), .025f);
                        }
                    }
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(ModContent.NPCType<Soulsucker>(), .01f);
                }

                if (!AAWorld.downedSisters && AAWorld.downedHydra && !NPC.AnyNPCs(ModContent.NPCType<HarukaShadow>()))
                pool.Add(ModContent.NPCType<HarukaShadow>(), .00005f);
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                ClearPoolWithExceptions(pool);

                if (AAWorld.downedSag)
                {
                    pool.Add(ModContent.NPCType<SagittariusMini>(), .005f);
		            pool.Add(ModContent.NPCType<VoidFlowey>(), .00000000000000000000000001f); // This is unlikely to spawn within the lifetime of the observable universe.
                }

                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Vortex>(), 0.002f);
                    pool.Add(ModContent.NPCType<Scout>(), .005f);
                }

                if (NPC.downedMoonlord)
                {
                    pool.Add(ModContent.NPCType<Searcher>(), .005f);

                    if (AAWorld.downedZero)
                    {
                        pool.Add(ModContent.NPCType<Null>(), .005f);
                    }
                }
                else
                {
                    pool.Add(ModContent.NPCType<Searcher1>(), .005f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().Terrarium)
            {
                ClearPoolWithExceptions(pool);

                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Bladon>(), .05f);
                    pool.Add(ModContent.NPCType<TerraDeadshot>(), .05f);
                    pool.Add(ModContent.NPCType<TerraWizard>(), .05f);
                    pool.Add(ModContent.NPCType<TerraWarlock>(), .05f);
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .03f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .03f);
                    return;
                }
                else if (Main.hardMode)
                {
                    pool.Add(ModContent.NPCType<TerraProbe>(), .07f);
                    pool.Add(ModContent.NPCType<TerraWatcher>(), .07f);
                    pool.Add(ModContent.NPCType<TerraSquire>(), .07f);
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .03f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .03f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .03f);
                    return;
                }
                else if (NPC.downedBoss2)
                {
                    pool.Add(ModContent.NPCType<PurityWeaver>(), .05f);
                    pool.Add(ModContent.NPCType<PuritySphere>(), .05f);
                    pool.Add(ModContent.NPCType<PurityCrawler>(), .05f);
                    pool.Add(ModContent.NPCType<PuritySquid>(), .05f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneAcropolis)
            {
                ClearPoolWithExceptions(pool);
                pool.Add(NPCID.Harpy, .06f);
                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Seraph>(), .03f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>().ZoneHoard)
            {
                ClearPoolWithExceptions(pool);

                pool.Add(NPCID.GiantWormHead, .005f);
                pool.Add(NPCID.GoldWorm, .001f);
                pool.Add(NPCID.Worm, .005f);

                if (NPC.downedPlantBoss)
                {
                    pool.Add(ModContent.NPCType<Scavenger>(), .03f);
                }
            }
            if(spawnInfo.player.GetModPlayer<AAPlayer>().StripeManSpawn)
            {
                if(NPC.goldCritterChance >= 30) NPC.goldCritterChance = 30;
                if(!spawnInfo.player.calmed && !spawnInfo.player.GetModPlayer<AAPlayer>().luckycalm)
                {
                    foreach (int npctype in Config.ListRareNpc)
                    {
                        if (pool.Keys.Contains(npctype) && pool[npctype] <= 0.05f)
                        {
                            pool[npctype] = 0.05f;
                        }
                    }
                }
            }
            else if(spawnInfo.player.GetModPlayer<AAPlayer>().AncientGoldLeg)
            {
                if(NPC.goldCritterChance >= 40) NPC.goldCritterChance = 40;
            }
            else
            {
                NPC.goldCritterChance = 150;
            }
        }

        public void VanillaNPCSpawn(Player player)
        {
            int spawnRangeXMin = (int)(player.position.X / 16f) - (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeXMax = (int)(player.position.X / 16f) + (int)(NPC.sWidth / 16 * 0.7);
            int spawnRangeYMin = (int)(player.position.Y / 16f) - (int)(NPC.sHeight / 16 * 0.7);
            int spawnRangeYMax = (int)(player.position.Y / 16f) + (int)(NPC.sHeight / 16 * 0.7);

            int x = Main.rand.Next(spawnRangeXMin, spawnRangeXMax);
			int y = Main.rand.Next(spawnRangeYMin, spawnRangeYMax);

            int npcid = 0;

            if (!Main.tile[x, y].active())
            {
                if (Sandstorm.Happening && player.ZoneSandstorm && TileID.Sets.Conversion.Sand[Main.tile[x, y].type] && NPC.Spawning_SandstoneCheck(x, y))
                {
                    if (Main.hardMode && Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 541, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (player.ZoneDungeon && NPC.downedPlantBoss)
                {
                    if (Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 287, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        int Skeletontype = 0;
                        switch (Main.rand.Next(3))
                        {
                            case 0: Skeletontype = 291; break;
                            case 1: Skeletontype = 292; break;
                            case 3: Skeletontype = 293; break;
                        }
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, Skeletontype, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 290, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface && Main.dayTime && Main.eclipse)
                {
                    bool flag = false;
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        flag = true;
                    }
                    if (flag && Main.rand.Next(40) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 477, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.worldSurface)
                {
                    if (player.ZoneSnow && Main.hardMode && Main.cloudAlpha > 0f && Main.rand.Next(15) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 243, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (player.ZoneHoly && Main.hardMode && Main.rand.Next(30) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 244, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y <= Main.worldSurface/2 && NPC.AnyDanger() && Main.hardMode && NPC.downedGolemBoss && Main.rand.Next(100) == 0 && !NPC.AnyNPCs(399))
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 399, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.hardMode && Main.rand.Next(25) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 109, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(100) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 53, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (Main.rand.Next(100) == 0 && Main.bloodMoon)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 536, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    
                    if (Main.dayTime)
                    {
                        if (Main.rand.Next(50) == 0)
                        {
                            npcid = NPC.NewNPC(x * 16 + 8, y * 16, 1, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[npcid].SetDefaults(-4, -1f);
                        }
                    }
                }
                else if (Main.hardMode && y > Main.worldSurface && Main.rand.Next(40) == 0)
                {
                    if (Main.rand.Next(2) == 0 && player.ZoneCorrupt)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 473, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.Next(2) == 0 && player.ZoneCrimson)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 474, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (Main.rand.Next(2) == 0 && player.ZoneHoly)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 475, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    else
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 85, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (Main.hardMode && Main.tile[x, y - 1].wall == 2 && Main.rand.Next(10) == 0)
                {
                    npcid = NPC.NewNPC(x * 16 + 8, y * 16, 85, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].type == 60 && Main.rand.Next(100) == 0 && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(x * 16 + 8, y * 16, 52, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (Main.tile[x, y].type == 60 && Main.hardMode && Main.rand.Next(45) == 0 && !Main.dayTime)
                {
                    npcid = NPC.NewNPC(x * 16 + 8, y * 16, 205, 0, 0f, 0f, 0f, 0f, 255);
                }
                else if (y > Main.maxTilesY - 190)
                {
                    if (Main.hardMode && !NPC.savedTaxCollector && Main.rand.Next(10) == 0 && !NPC.AnyNPCs(534))
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 534, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (y <= Main.maxTilesY - 190 && y > Main.rockLayer)
                {
                    if (Main.rand.Next(50) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 195, 0, 0f, 0f, 0f, 0f, 255);
                    }
                    if (y > (Main.rockLayer + Main.maxTilesY) / 2.0 && Main.rand.Next(50) == 0)
                    {
                        npcid = NPC.NewNPC(x * 16 + 8, y * 16, 45, 0, 0f, 0f, 0f, 0f, 255);
                    }
                }
            }

            if (Main.netMode == NetmodeID.Server && npcid < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid, 0f, 0f, 0f, 0, 0, 0);
                return;
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
            if (player.GetModPlayer<AAPlayer>().luckycalm)
			{
				spawnRate = (int)((double)spawnRate * 30f);
				maxSpawns = (int)(maxSpawns * 0.009f);
            }

            if(!player.GetModPlayer<AAPlayer>().luckycalm && player.GetModPlayer<AAPlayer>().StripeManSpawn && !player.calmed && player.active && !player.dead && player.activeNPCs < maxSpawns && Main.rand.NextDouble()*(spawnRate/1.333f) < 1 )
            {
                VanillaNPCSpawn(player);
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Demolitionist && !Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<M79Round>());
                nextSlot++;
            }

            /* This is beginning to be an issue
            if (type == NPCID.WitchDoctor && Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mortar>());
                nextSlot++;
            }
            */

            if (type == NPCID.Dryad)
            {
                if (Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMush)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<MyceliumSeeds>());
                    nextSlot++;
                }
                if (AAWorld.downedRajah)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoldenCarrot>());
                    shop.item[nextSlot].shopCustomPrice = Item.sellPrice(0, 30, 0, 0);
                    nextSlot++;
                }
            }

            if (type == NPCID.Truffle && NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
                shop.item[nextSlot].shopCustomPrice = Item.sellPrice(3, 0, 0, 0);
                nextSlot++;
            }
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DeepGreenSolution>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LimeSolution>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<FungicideSolution>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<WhiteSolution>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<YellowSolution>());
                nextSlot++;
            }
        }

        // SpawnBoss(player, "MyBoss", true, 0, 0, "DerpyBoi", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.NPCType(type), spawnMessage, overrideDirection, overrideDirectionY, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, ModContent.NPCType<MyBoss>(), true, 0, 0, "DerpyBoi 2", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            if (overrideDirection == 0)
            {
                overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            }

            if (overrideDirectionY == 0)
            {
                overrideDirectionY = -1;
            }

            Vector2 npcCenter = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
            SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, "MyBoss", true, player.Center + new Vector2(0, -800f), "DerpFromAbove", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.NPCType(type), spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, ModContent.NPCType<MyBoss>(), true, player.Center + new Vector2(0, 800f), "DerpFromBelow", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
            {
                npcCenter = player.Center;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                }

                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;

                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                    {
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    }

                    if (namePlural)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                BaseUtility.Chat(npcName + " " + Language.GetTextValue("Mods.AAMod.Common.NPCarrive"), 175, 75, 255, false);
                            }
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + Language.GetTextValue("Mods.AAMod.Common.NPCarrive")), new Color(175, 75, 255));
                        }
                    }
                    else
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false);
                            }
                        }
                        else if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.BroadcastChatMessage(
                                NetworkText.FromKey("Announcement.HasAwoken", new object[] { NetworkText.FromLiteral(npcName) }), 
                                new Color(175, 75, 255)
                            );
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                Globals.AANet.SendNetMessage(Globals.AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

        public static void SpawnRajah(Player player, bool spawnMessage = false, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
            {
                npcCenter = player.Center;
            }

            int RajahType = ModContent.NPCType<Rajah>();
            if (NPC.killCount[NPCID.Bunny] >= 1000)
            {
                RajahType = ModContent.NPCType<SupremeRajah>();
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(RajahType))
                {
                    return;
                }

                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, RajahType, 0, 0, 0, player.whoAmI);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate = true;
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                Globals.AANet.SendNetMessage(Globals.AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)RajahType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }
    }
}
