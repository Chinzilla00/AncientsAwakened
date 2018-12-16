using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;
using AAMod.NPCs.Bosses.Broodmother;
using AAMod.NPCs.Bosses.Hydra;
using AAMod.NPCs.Bosses.Daybringer;
using AAMod.NPCs.Bosses.Nightcrawler;
using AAMod.NPCs.Bosses.Orthrus;
using AAMod.NPCs.Bosses.Raider;
using AAMod.NPCs.Bosses.Retriever;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Yamata;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using AAMod.NPCs.Bosses.Zero;
using AAMod.NPCs.Bosses.MushroomMonarch;
using AAMod.NPCs.Bosses.Shen;

using System;
using BaseMod;

namespace AAMod
{
    public class AAModGlobalNPC : GlobalNPC
	{
        //debuffs
        public bool TimeFrozen = false;
        public bool infinityOverload = false;
        public bool terraBlaze = false;
        public bool Dragonfire = false;
        public bool Hydratoxin = false;
        public bool Moonraze = false;
        public bool Electrified = false;
        public bool InfinityScorch = false;
        public bool DiscordInferno = false;
        public static int Toad = -1;

        public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public override void ResetEffects(NPC npc)
		{
            infinityOverload = false;
            terraBlaze = false;
            TimeFrozen = false;
            Dragonfire = false;
            Hydratoxin = false;
            Moonraze = false;
            Electrified = false;
            InfinityScorch = false;
            DiscordInferno = false;
        }

		public override void SetDefaults(NPC npc)
		{
		}

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {

            int before = npc.lifeRegen;
            bool drain = false;
            bool noDamage = damage <= 1;
            int damageBefore = damage;
            int num = npc.lifeRegenExpectedLossPerSecond;

            if (infinityOverload)
            {
                drain = true;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 60;
                if (damage < 40)
                {
                    damage = 40;
                }
            }

            if (InfinityScorch)
            {
                drain = true;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 80;
                if (damage < 40)
                {
                    damage = 40;
                }
            }
            if (npc.type == NPCID.KingSlime || npc.type == NPCID.Plantera)
            {
                if (npc.onFire)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= 20;
                }
            }

            if (noDamage)
                damage -= damageBefore;
            if (drain && before > 0)
                npc.lifeRegen -= before;
            if (terraBlaze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 16;
                if (damage < 2)
                {
                    damage = 2;
                }
            }

            if (Moonraze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int num7 = 0;
                int num8 = 4;
                if (num7 == 0)
                {
                    num7 = 1;
                }
                npc.lifeRegen -= num7 * 2 * 100;
                if (num < num7 * 100 / num8)
                {
                    num = num7 * 100 / num8;
                }
            }

            if (Electrified)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 8;
                if (npc.velocity.X >= 0 || npc.velocity.X <= 0)
                {
                    npc.lifeRegen -= 32;
                }
            }

            if (DiscordInferno)
            {
                npc.lifeRegen -= 52;
                npc.damage -= 10;
                if (npc.velocity.X < -2f || npc.velocity.X > 2f)
                {
                    npc.velocity.X *= 0.8f;
                }
                if (npc.velocity.Y < -2f || npc.velocity.Y > 2f)
                {
                    npc.velocity.Y *= 0.8f;
                }
            }

            if (Dragonfire)
            {
                npc.damage -= 10;
            }
            if (Hydratoxin)
            {
                if (npc.velocity.X < -2f || npc.velocity.X > 2f)
                {
                    npc.velocity.X *= 0.8f;
                }
                if (npc.velocity.Y < -2f || npc.velocity.Y > 2f)
                {
                    npc.velocity.Y *= 0.8f;
                }
            }

        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
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

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.FireImp)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(2, 3));
            }
            if (npc.type == NPCID.Demon)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(4, 5));
            }
            if (npc.type == NPCID.VoodooDemon)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(5, 6));
            }
            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PlanteraPetal"), Main.rand.Next(30, 40));
            }
            if (npc.type == NPCID.GreekSkeleton)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GladiatorsGlory"));
                }
            }

            if (npc.type == NPCID.DukeFishron)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Seashroom"));
                }
            }

            if (npc.type == NPCID.EnchantedSword)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Excalibur);
                }
            }

            if (npc.type == NPCID.CrimsonAxe)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BloodLustCluster);
                }
            }

            if (npc.type == NPCID.CursedHammer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shadowban"));
                }
            }

            if (Main.rand.Next(4096) == 0)   //item rarity
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShinyCharm")); //Item spawn
            }

            if (NPC.downedMoonlord == true)
            {
                if (npc.type == NPCID.GoblinSummoner)   //this is where you choose the npc you want
                {
                    if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GoblinDoll"), 1);
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss == true)
            {
                if (npc.type == NPCID.RedDevil)   //this is where you choose the npc you want
                {
                    if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PureEvil"), 1);
                        }
                    }
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CthulhusBlade"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if (npc.type == NPCID.GiantFlyingFox)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheFox"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if (npc.type == NPCID.Necromancer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Exorcist"));
                }
            }
            if (npc.type == NPCID.AngryBones ||
                npc.type == NPCID.AngryBonesBig ||
                npc.type == NPCID.AngryBonesBigHelmet ||
                npc.type == NPCID.AngryBonesBigMuscle)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientPoker"));
                }
            }
            if (npc.type == NPCID.Paladin)
            {
                if (Main.rand.NextFloat() < .17f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Helmet"));
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Chestplate"));
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Boots"));
                }
            }
            //Probes (Lil destroyer laser shits)
            if (npc.type == NPCID.Probe)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(3, 12));
            }
            //The Destroyer
            if (npc.type == NPCID.TheDestroyer)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(8, 16));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Laser_Rifle"));
                }
            }
            //Skeletrono Primeus (Skeletron Prime)
            if (npc.type == NPCID.SkeletronPrime)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(8, 16));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Laser_Rifle"));
                }
            }
            //Wall Of Flesh
            if (npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Nightmare_Ore"), Main.rand.Next(50, 60));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("HK_MP5"));
                }
            }
            //Mothership
            if (npc.type == NPCID.MartianSaucerCore)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Alien_Rifle"));
                }
                if (Main.rand.NextFloat() < .03f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Energy_Conduit"));
                }
            }
            //Wall Of Flesh
            if (npc.type == NPCID.Vulture)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("vulture_feather"), Main.rand.Next(1, 3));
            }
            //Drippler
            if (npc.type == NPCID.Drippler)
            {
                if (Main.rand.NextFloat() < .005f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Bloody_Mary"));
                }
            }
            if ((npc.type == NPCID.GoblinArcher
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
                && NPC.downedGoblins)
            {
                    Item.NewItem(npc.getRect(), mod.ItemType("GoblinSoul"), Main.rand.Next(0, 1));
                
            }

            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneMire) && Main.hardMode)
            {
                if (Main.rand.Next(0, 100) >= 80)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSpite"), 1);
                }
            }
            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneInferno) && Main.hardMode)
            {
                if (Main.rand.Next(0, 100) >= 80)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSmite"), 1);
                }
            }
        }

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
            Rectangle hitbox = npc.Hitbox;
            if (Electrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadB"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadR"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadG"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadY"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadP"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadO"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
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
                for (int i = 0; i < 20; i++)
                {
                    int num3 = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        6,
                        259,
                        158
                    });
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, num3, 0f, -2.5f, 0, new Color(115, 149, 171), 1f);
                    Main.dust[num4].alpha = 200;
                    Main.dust[num4].velocity *= 1.4f;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (DiscordInferno)
            {
                for (int i = 0; i < 20; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].velocity *= 1.4f;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (terraBlaze)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107, default(Color), 3.5f);
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
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, mod.DustType<Dusts.VoidDust>(), default(Color), 3.5f);
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

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (NPC.AnyNPCs(mod.NPCType<GripOfChaosBlue>()) ||
                NPC.AnyNPCs(mod.NPCType<GripOfChaosRed>()) ||
                NPC.AnyNPCs(mod.NPCType<MushroomMonarch>()) ||
                NPC.AnyNPCs(mod.NPCType<Broodmother>()) ||
                NPC.AnyNPCs(mod.NPCType<Hydra>()) ||
                NPC.AnyNPCs(mod.NPCType<Raider>()) ||
                NPC.AnyNPCs(mod.NPCType<Retriever>()) ||
                NPC.AnyNPCs(mod.NPCType<Orthrus>()) ||
                NPC.AnyNPCs(mod.NPCType<Daybringer>()) ||
                NPC.AnyNPCs(mod.NPCType<Nightcrawler>()) ||
                NPC.AnyNPCs(mod.NPCType<Daybringer>()) ||
                NPC.AnyNPCs(mod.NPCType<Akuma>()) ||
                NPC.AnyNPCs(mod.NPCType<AkumaA>()) ||
                NPC.AnyNPCs(mod.NPCType<Yamata>()) ||
                NPC.AnyNPCs(mod.NPCType<YamataA>()) ||
                NPC.AnyNPCs(mod.NPCType<Zero>()) ||
                NPC.AnyNPCs(mod.NPCType<ZeroAwakened>()))
            {
                spawnRate = 0;
                maxSpawns = 0;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
            {
                pool[0] = 0f;

                if (NPC.downedMoonlord)
                {
                    pool.Add(mod.NPCType("Searcher"), 1f);
                    if (AAWorld.downedZero && !Main.expertMode)
                    {
                        pool.Add(mod.NPCType("Null"), 1f);
                    }
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
            if (type == NPCID.Clothier)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Vanity.Pepsi.PepsimanCan>());
                nextSlot++;
            }
        }

        public override bool PreAI(NPC npc)
        {

            if (!npc.boss ||
                npc.type != NPCID.WallofFlesh ||
                npc.type != NPCID.SkeletronHand ||
                npc.type != NPCID.DungeonGuardian ||
                npc.type != NPCID.WallofFlesh ||
                npc.type != NPCID.WallofFleshEye ||
                npc.type != NPCID.PrimeCannon ||
                npc.type != NPCID.PrimeLaser ||
                npc.type != NPCID.PrimeSaw ||
                npc.type != NPCID.PrimeVice ||
                npc.type != NPCID.EaterofWorldsBody ||
                npc.type != NPCID.EaterofWorldsTail ||
                npc.type != NPCID.EaterofWorldsHead ||
                npc.type != NPCID.TheDestroyerBody ||
                npc.type != NPCID.TheDestroyerTail ||
                npc.type != NPCID.GolemFistLeft ||
                npc.type != NPCID.GolemFistRight ||
                npc.type != NPCID.GolemHead ||
                npc.type != NPCID.GolemHeadFree ||
                npc.type != NPCID.PlanterasHook ||
                npc.type != NPCID.PlanterasTentacle ||
                npc.type != NPCID.Creeper ||
                npc.type != NPCID.PumpkingBlade ||
                npc.type != NPCID.MartianSaucerCannon ||
                npc.type != NPCID.MartianSaucerCore ||
                npc.type != NPCID.MartianSaucerTurret ||
                npc.type != NPCID.MoonLordCore ||
                npc.type != NPCID.MoonLordFreeEye ||
                npc.type != NPCID.MoonLordHand ||
                npc.type != NPCID.MoonLordHead ||
                npc.type != NPCID.MoonLordLeechBlob ||
                npc.type != NPCID.AncientCultistSquidhead ||
                npc.type != NPCID.CultistBossClone ||
                npc.type != NPCID.CultistDragonBody1 ||
                npc.type != NPCID.CultistDragonBody2 ||
                npc.type != NPCID.CultistDragonBody3 ||
                npc.type != NPCID.CultistDragonBody4 ||
                npc.type != NPCID.CultistDragonHead ||
                npc.type != NPCID.CultistDragonTail ||
                npc.type != NPCID.CultistTablet ||
                npc.type != NPCID.MothronEgg ||
                npc.type != NPCID.MothronSpawn ||
                npc.type != NPCID.Mothron ||
                npc.type != NPCID.PirateShipCannon ||
                npc.type != NPCID.LunarTowerSolar ||
                npc.type != NPCID.LunarTowerNebula ||
                npc.type != NPCID.LunarTowerVortex ||
                npc.type != NPCID.LunarTowerStardust ||
                npc.type != NPCID.AncientLight ||
                npc.type != NPCID.AncientDoom ||
                npc.type != NPCID.SandElemental ||
                npc.type != NPCID.DD2EterniaCrystal ||
                npc.type != NPCID.DD2AttackerTest ||
                npc.type != NPCID.DD2Betsy ||
                npc.type != NPCID.DD2DarkMageT1 ||
                npc.type != NPCID.DD2DarkMageT3 ||
                npc.type != NPCID.DD2DrakinT2 ||
                npc.type != NPCID.DD2DrakinT3 ||
                npc.type != NPCID.DD2GoblinBomberT1 ||
                npc.type != NPCID.DD2GoblinBomberT2 ||
                npc.type != NPCID.DD2GoblinBomberT3 ||
                npc.type != NPCID.DD2GoblinT1 ||
                npc.type != NPCID.DD2GoblinT2 ||
                npc.type != NPCID.DD2GoblinT3 ||
                npc.type != NPCID.DD2JavelinstT1 ||
                npc.type != NPCID.DD2JavelinstT2 ||
                npc.type != NPCID.DD2JavelinstT3 ||
                npc.type != NPCID.DD2KoboldFlyerT2 ||
                npc.type != NPCID.DD2KoboldFlyerT3 ||
                npc.type != NPCID.DD2KoboldWalkerT2 ||
                npc.type != NPCID.DD2KoboldWalkerT3 ||
                npc.type != NPCID.DD2LanePortal ||
                npc.type != NPCID.DD2LightningBugT3 ||
                npc.type != NPCID.DD2OgreT2 ||
                npc.type != NPCID.DD2OgreT3 ||
                npc.type != NPCID.DD2SkeletonT1 ||
                npc.type != NPCID.DD2SkeletonT3 ||
                npc.type != NPCID.DD2WitherBeastT2 ||
                npc.type != NPCID.DD2WitherBeastT3 ||
                npc.type != NPCID.DD2WyvernT1 ||
                npc.type != NPCID.DD2WyvernT2 ||
                npc.type != NPCID.DD2WyvernT3 ||
                npc.type != NPCID.ShadowFlameApparition)
            {
                if (TimeFrozen)
                {
                    npc.position = npc.oldPosition;
                    npc.frameCounter--;
                    return false;
                }
            }
            Player targetPlayer = Main.player[npc.target];
            try
            {
                if (npc.type == NPCID.Harpy || npc.type == NPCID.WyvernHead || npc.type == NPCID.MartianProbe)
                {
                    if (npc.timeLeft > 10 && targetPlayer.GetModPlayer<AAPlayer>(mod).ZoneVoid == true)
                    {
                        npc.timeLeft = 10;
                    }
                }
            }
            catch (Exception e) { BaseUtility.LogFancy("MNPC PREAI ERROR: ", e); }
            return true;
        }   
    }
}
