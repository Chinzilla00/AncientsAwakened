using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using AAMod.NPCs.Enemies.Terrarium.PreHM;
using AAMod.NPCs.Enemies.Terrarium.Hardmode;
using AAMod.NPCs.Enemies.Terrarium.PostPlant;
using AAMod.NPCs.Enemies.Terrarium.PostEquinox;
using System;
using BaseMod;
using Terraria.Localization;


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
        public bool irradiated = false;
        public bool DiscordInferno = false;
        public bool riftBent = false;
        public bool BrokenArmor = false;
        public static int Toad = -1;
        public static int Rose = -1;
        public static int Brain = -1;
        public static int Rajah = -1;

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
            irradiated = false;
            riftBent = false;
            BrokenArmor = false;
        }

        public override void SetDefaults(NPC npc)
        {
            if (AAWorld.downedShen == true)
            {
                if (npc.type == NPCID.GoblinSummoner)   //this is where you choose the npc you want
                {
                    npc.damage = 130;
                    npc.defense = 70;
                    npc.lifeMax = 10000;
                    npc.knockBackResist = 0.05f;
                    npc.value = 50000f;
                }
            }
        }

        public int RiftTimer;
        public int RiftDamage = 10;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            int before = npc.lifeRegen;
            bool drain = false;
            bool noDamage = damage <= 1;
            int damageBefore = damage;

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

            if (riftBent)
            {
                RiftTimer++;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen = 0;
                if (RiftTimer >= 120)
                {
                    RiftDamage += 10;
                    RiftTimer = 0;
                }
                if (RiftDamage >= 80)
                {
                    RiftDamage = 80;
                }
                npc.lifeRegen -= RiftDamage;
            }
            else
            {
                RiftDamage = 10;
                RiftTimer = 0;
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
                if (npc.type == mod.NPCType<ShenDoragon>() || npc.type == mod.NPCType<ShenA>())
                {
                    npc.lifeRegen -= 48;
                }
                else
                {
                    npc.lifeRegen -= 16;
                }
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
                if (num7 == 0)
                {
                    num7 = 1;
                }
                npc.lifeRegen -= num7 * 2 * 100;
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

            if (BrokenArmor)
            {
                npc.defense *= (int).8f;
            }

            if (Dragonfire)
            {
                npc.damage -= 10;
            }

            if (Hydratoxin)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= Math.Abs((int)npc.velocity.X);
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
            bool isBunny = npc.type == NPCID.Bunny || npc.type == NPCID.GoldBunny || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.PartyBunny;

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

            if (Main.rand.Next(8192) == 0)   //item rarity
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShinyCharm")); //Item spawn
            }

            if (AAWorld.downedAllAncients == true)
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
                if (Main.rand.NextFloat() < 0.01f)
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
            if (npc.type == NPCID.CursedSkull)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("SkullStaff"));
                }
            }
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
            if (npc.type == NPCID.AngryBones || npc.type == NPCID.DarkCaster)
            {
                if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("M79Parts"));
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                if (Main.rand.NextFloat() < .01f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("BugSwatter"));
                }
                Item.NewItem(npc.getRect(), ItemID.Stinger, Main.rand.Next(14, 20));
            }

            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem(npc.getRect(), ItemID.ChlorophyteOre, Main.rand.Next(50, 80));
            }

            if (npc.type == NPCID.SkeletronHand)
            {
                Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(4, 8));
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(30, 45));
            }

            if (Main.player[Main.myPlayer].ZoneJungle && Main.rand.Next(30) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Everleaf"), Main.rand.Next(1, 2));
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
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("GoblinSoul"), Main.rand.Next(1, 2));
                }
            }

            if (npc.type == NPCID.GoldBunny && NPC.downedGolemBoss)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GoldenCarrot"), 1);
            }

            if (isBunny && NPC.downedGolemBoss && Main.rand.Next(80) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GoldenCarrot"), 1);
            }


            if (Main.hardMode)
            {
                Player player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
                if (player.GetModPlayer<AAPlayer>(mod).ZoneMire && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.Next(0, 100) >= 80)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSpite"), 1);
                    }
                }
                if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno && player.position.Y > (Main.worldSurface * 16.0))
                {
                    if (Main.rand.Next(0, 100) >= 80)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSmite"), 1);
                    }
                }
                if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    if (Main.rand.Next(0, 2499) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MireKey"), 1);
                    }
                }
                if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
                {
                    if (Main.rand.Next(0, 2499) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoKey"), 1);
                    }
                }
                if (player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
                {
                    if (Main.rand.Next(0, 1249) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DoomstopperKey"), 1);
                    }
                }
                if (player.GetModPlayer<AAPlayer>(mod).Terrarium && NPC.downedPlantBoss)
                {
                    if (Main.rand.Next(0, 100) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TerraCrystal"), 1);
                    }
                }
            }


            if (Main.hardMode && isBunny)
            {
                Player player = Main.player[Player.FindClosest(npc.Center, npc.width, npc.height)];
                int bunnyKills = NPC.killCount[NPCID.Bunny];
                if (bunnyKills % 100 == 0 && bunnyKills < 1000)
                {
                    Main.NewText("Those who slaughter the innocent must be PUNISHED!", 107, 137, 179);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, mod.NPCType<NPCs.Bosses.Rajah.Rajah>(), true, new Vector2(npc.Center.X, npc.Center.Y - 2000), "Rajah Rabbit");

                }
                if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                {
                    Main.NewText("YOU HAVE COMMITTED AN UNFORGIVABLE SIN! I SHALL WIPE YOU FROM THIS MORTAL REALM! PREPARE FOR TRUE PAIN AND PUNISHMENT, " + player.name + "!", 107, 137, 179);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                    SpawnRajah(player, mod.NPCType<NPCs.Bosses.Rajah.Rajah>(), true, new Vector2(npc.Center.X, npc.Center.Y - 2000), "Rajah Rabbit");
                }
                if (bunnyKills % 50 == 0 && bunnyKills % 100 != 0)
                {
                    Main.NewText("The eyes of a wrathful creature gaze upon you...", 107, 137, 179);
                }
            }
        }

        public void Anticheat(NPC npc, string Text, Color TextColor, ref double damage)
        {
            if (damage > npc.lifeMax / 8)
            {
                Main.NewText(Text, TextColor);
                damage = 0;
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
                int dustCount = Math.Max(1, Math.Min(5, (Math.Max(npc.width, npc.height) / 10)));
                for (int i = 0; i < dustCount; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.Moonraze>(), 0f, 1f, 0, default(Color), 1f);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (riftBent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.CthulhuAuraDust>(), 0f, 1f, 0, default(Color), 1f);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
                Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0f, 0.45f, 0.45f);
            }

            if (DiscordInferno)
            {
                for (int i = 0; i < 8; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (Hydratoxin)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType<Dusts.HydratoxinDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107, default(Color), 1f);
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

            if (Dragonfire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType<Dusts.DragonflameDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107, default(Color), 1f);
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

            if (terraBlaze)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107, default(Color), 1f);
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
                            keepPool.Add(npcID, kvp.Value);
                    }
                }
                pool.Clear();

                foreach (var newkvp in keepPool)
                    pool.Add(newkvp.Key, newkvp.Value);

                keepPool.Clear();
            }
            catch (Exception e)
            {
                Main.NewText(e.StackTrace);
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];


            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneStars)
            {
                if (Main.dayTime)
                {
                    pool.Add(mod.NPCType("Sunwatcher"), .2f);
                }
                else
                {
                    pool.Add(mod.NPCType("Nightguard"), .2f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                ClearPoolWithExceptions(pool);
                if ((player.position.Y < (Main.worldSurface * 16.0)) && (Main.dayTime || AAWorld.downedAkuma))
                {
                    pool.Add(mod.NPCType("Wyrmling"), .3f);
                    pool.Add(mod.NPCType("InfernalSlime"), .7f);
                    pool.Add(mod.NPCType("Flamebrute"), .3f);
                    pool.Add(mod.NPCType("InfernoSalamander"), .5f);
                    pool.Add(mod.NPCType("DragonClaw"), .1f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("MagmaSwimmer"), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(mod.NPCType("Wyvern"), .2f);
                        pool.Add(mod.NPCType("BlazePhoenix"), .1f);
                    }
                }
                else if (player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(mod.NPCType("Wyrmling"), .3f);
                    pool.Add(mod.NPCType("Flamebrute"), .3f);
                    pool.Add(mod.NPCType("InfernoSalamander"), .5f);
                    pool.Add(mod.NPCType("DragonClaw"), .1f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("HotWasp"), .1f);
                        pool.Add(mod.NPCType("MagmaSwimmer"), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(mod.NPCType("Wyvern"), .2f);
                        pool.Add(mod.NPCType("Wyrm"), .1f);
                        pool.Add(mod.NPCType("ChaoticDawn"), .1f);
                        if (player.ZoneSnow)
                        {
                            pool.Add(mod.NPCType("Dragron"), .05f);
                        }
                        if (player.ZoneUndergroundDesert)
                        {
                            pool.Add(mod.NPCType("InfernoGhoul"), .1f);
                        }
                    }
                }
                if (AAWorld.downedSisters)
                {
                    pool.Add(mod.NPCType("BlazeClaw"), .05f);
                }
                if (AAWorld.downedAkuma)
                {
                    pool.Add(mod.NPCType("Lung"), .2f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                ClearPoolWithExceptions(pool);
                if ((player.position.Y < (Main.worldSurface * 16.0)) && (!Main.dayTime || AAWorld.downedYamata))
                {
                    pool.Add(mod.NPCType("MireSlime"), 1f);
                    pool.Add(mod.NPCType("Mosster"), .5f);
                    pool.Add(mod.NPCType("Newt"), 1f);
                    pool.Add(mod.NPCType("HydraClaw"), 1f);
                    pool.Add(mod.NPCType("MireSkulker"), .7f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("FogAngler"), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(mod.NPCType("Toxitoad"), .2f);
                        pool.Add(mod.NPCType("Kappa"), .4f);
                    }
                }
                else if (player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(mod.NPCType("Mosster"), .5f);
                    pool.Add(mod.NPCType("Newt"), 1f);
                    pool.Add(mod.NPCType("HydraClaw"), 1f);
                    pool.Add(mod.NPCType("MireSkulker"), .5f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("FogAngler"), SpawnCondition.WaterCritter.Chance * 0.2f);
                        pool.Add(mod.NPCType("Miresquito"), .4f);
                        pool.Add(mod.NPCType("ChaoticTwilight"), .1f);
                        if (player.ZoneSnow)
                        {
                            pool.Add(mod.NPCType("Miregron"), .05f);
                        }
                        if (player.ZoneUndergroundDesert)
                        {
                            pool.Add(mod.NPCType("MireGhoul"), .1f);
                        }
                    }
                }
                if (AAWorld.downedSisters)
                {
                    pool.Add(mod.NPCType("AbyssClaw"), .8f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
            {
                pool.Clear();
                pool.Add(mod.NPCType("Searcher1"), .05f);
                if (AAWorld.downedSag)
                {
                    pool.Add(mod.NPCType("SagittariusMini"), .025f);
                }
                if (NPC.downedPlantBoss)
                {
                    pool.Add(mod.NPCType("Vortex"), 0.05f);
                    pool.Add(mod.NPCType("Scout"), .05f);
                }
                if (NPC.downedMoonlord)
                {
                    pool.Add(mod.NPCType("Searcher"), .05f);
                    if (AAWorld.downedZero)
                    {
                        pool.Add(mod.NPCType("Null"), .05f);
                    }
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).Terrarium)
            {
                pool.Clear();
                if (NPC.downedPlantBoss)
                {
                    pool.Add(mod.NPCType<Bladon>(), .05f);
                    pool.Add(mod.NPCType<TerraDeadshot>(), .05f);
                    pool.Add(mod.NPCType<TerraWizard>(), .05f);
                    pool.Add(mod.NPCType<TerraWarlock>(), .05f);
                    pool.Add(mod.NPCType<PurityWeaver>(), .03f);
                    pool.Add(mod.NPCType<PuritySphere>(), .03f);
                    pool.Add(mod.NPCType<PurityCrawler>(), .03f);
                    pool.Add(mod.NPCType<PuritySquid>(), .03f);
                    return;
                }
                if (Main.hardMode)
                {
                    pool.Add(mod.NPCType<TerraProbe>(), .07f);
                    pool.Add(mod.NPCType<TerraWatcher>(), .07f);
                    pool.Add(mod.NPCType<TerraSquire>(), .07f);
                    pool.Add(mod.NPCType<PurityWeaver>(), .03f);
                    pool.Add(mod.NPCType<PuritySphere>(), .03f);
                    pool.Add(mod.NPCType<PurityCrawler>(), .03f);
                    pool.Add(mod.NPCType<PuritySquid>(), .03f);
                }
                if (NPC.downedBoss3)
                {
                    pool.Add(mod.NPCType<PurityWeaver>(), .1f);
                    pool.Add(mod.NPCType<PuritySphere>(), .1f);
                    pool.Add(mod.NPCType<PurityCrawler>(), .1f);
                    pool.Add(mod.NPCType<PuritySquid>(), .1f);
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Demolitionist && !Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("M79Round"));
                nextSlot++;
            }

            if (type == NPCID.WitchDoctor && Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Mortar"));
                nextSlot++;
            }

            if (type == NPCID.Dryad)
            {
                if (Main.player[Main.myPlayer].GetModPlayer<AAPlayer>(mod).ZoneMush)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("MyceliumSeeds"));
                    nextSlot++;
                }
                if (AAWorld.downedRajah)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("GoldenCarrot"));
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1, 0, 0, 0);
                    nextSlot++;
                }
            }

            if (type == NPCID.Truffle && NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(3, 0, 0, 0);
                nextSlot++;
            }
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("DeepGreenSolution"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("LimeSolution"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("FungicideSolution"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("WhiteSolution"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("YellowSolution"));
                nextSlot++;
            }
        }

        // SpawnBoss(player, "MyBoss", true, 0, 0, "DerpyBoi", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.NPCType(type), spawnMessage, overrideDirection, overrideDirectionY, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, 0, 0, "DerpyBoi 2", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            if (overrideDirection == 0)
                overrideDirection = (Main.rand.Next(2) == 0 ? -1 : 1);
            if (overrideDirectionY == 0)
                overrideDirectionY = -1;
            Vector2 npcCenter = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
            SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, "MyBoss", true, player.Center + new Vector2(0, -800f), "DerpFromAbove", false);
        public static void SpawnBoss(Player player, string type, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
        {
            Mod mod = AAMod.instance;
            SpawnBoss(player, mod.NPCType(type), spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, player.Center + new Vector2(0, 800f), "DerpFromBelow", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default(Vector2))
                npcCenter = player.Center;
            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = (!String.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName);
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == 0) { Main.NewText(npcName + " have awoken!", 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, (bool)spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, (string)overrideDisplayName, (bool)namePlural);
            }
        }

        public static void SpawnRajah(Player player, int bossType, bool spawnMessage = false, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default(Vector2))
                npcCenter = player.Center;
            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;

            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, (bool)spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, (string)overrideDisplayName, (bool)namePlural);
            }
        }
    }
}
