using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using AAMod.Tiles;
using AAMod.Tiles.Plants;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Mushman : ModNPC
    {
        public override string Texture => "AAMod/NPCs/TownNPCs/Mushman";

        public override bool Autoload(ref string name)
        {
            name = "Mushman";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 7;
            NPCID.Sets.AttackFrameCount[npc.type] = 3;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 20;
            NPCID.Sets.HatOffsetY[npc.type] = -3;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 40;
            npc.defense = 38;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Truffle;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (WorldGen.roomY2 > Main.worldSurface)
            {
                return false;
            }
            int num = 0;
            int num2 = WorldGen.roomX1 - Main.zoneX / 2 / 16 - 1 - Lighting.offScreenTiles;
            int num3 = WorldGen.roomX2 + Main.zoneX / 2 / 16 + 1 + Lighting.offScreenTiles;
            int num4 = WorldGen.roomY1 - Main.zoneY / 2 / 16 - 1 - Lighting.offScreenTiles;
            int num5 = WorldGen.roomY2 + Main.zoneY / 2 / 16 + 1 + Lighting.offScreenTiles;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 >= Main.maxTilesX)
            {
                num3 = Main.maxTilesX - 1;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num5 > Main.maxTilesX)
            {
                num5 = Main.maxTilesX;
            }
            for (int i = num2 + 1; i < num3; i++)
            {
                for (int j = num4 + 2; j < num5 + 2; j++)
                {
                    if (Main.tile[i, j].active() && (Main.tile[i, j].type == ModContent.TileType<Mycelium>() || Main.tile[i, j].type == ModContent.TileType<Mushroom>() || Main.tile[i, j].type == ModContent.TileType<MadnessShroom>()))
                    {
                        num++;
                    }
                }
            }
            return num >= 100;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (AAWorld.downedMonarch == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            return null;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (Truffle >= 0 && Main.rand.Next(4) == 0)
            {
                chat.Add(Lang.TownNPCMushman("MushmanChat1"));
            }
            int WitchDoctor = NPC.FindFirstNPC(NPCID.WitchDoctor);
            if (WitchDoctor >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[WitchDoctor].GivenName + Lang.TownNPCMushman("MushmanChat2");
            }
            chat.Add(Lang.TownNPCMushman("MushmanChat3"));
            chat.Add(Lang.TownNPCMushman("MushmanChat4"));
            chat.Add(Lang.TownNPCMushman("MushmanChat5"));
            int Clothier = NPC.FindFirstNPC(NPCID.Clothier);
            if (Clothier >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[Clothier].GivenName + Lang.TownNPCMushman("MushmanChat6");
            }
            return chat; 
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Lang.TownNPCMushman("button1");
            button2 = Lang.TownNPCMushman("button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }

            if (!firstButton)
            {
                Main.PlaySound(SoundID.MenuTick, -1, -1, 1);

                Player player = Main.LocalPlayer;

                int Special = player.FindItem(mod.ItemType("MadnessShroom"));
                int Item = player.FindItem(ItemID.StrangePlant1);
                int Item2 = player.FindItem(ItemID.StrangePlant2);
                int Item3 = player.FindItem(ItemID.StrangePlant3);
                int Item4 = player.FindItem(ItemID.StrangePlant4);

                int DyeRed = player.FindItem(ItemID.RedHusk);
                int DyeOrange = player.FindItem(ItemID.OrangeBloodroot);
                int DyeYellow = player.FindItem(ItemID.YellowMarigold);
                int DyeGreen1 = player.FindItem(ItemID.GreenMushroom);
                int DyeGreen2 = player.FindItem(ItemID.LimeKelp);
                int DyeGreen3 = player.FindItem(ItemID.TealMushroom);
                int DyeBlue1 = player.FindItem(ItemID.CyanHusk);
                int DyeBlue2 = player.FindItem(ItemID.SkyBlueFlower);
                int DyeBlue3 = player.FindItem(ItemID.BlueBerries);
                int DyePurple1 = player.FindItem(ItemID.PurpleMucos);
                int DyePurple2 = player.FindItem(ItemID.VioletHusk);
                int DyePink = player.FindItem(ItemID.PinkPricklyPear);
                int DyeGray = player.FindItem(ItemID.BlackInk);

                string[] lootTable = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray", "Pink", "Brown" };
                int loot = Main.rand.Next(lootTable.Length);

                if (Special >= 0)
                {
                    player.inventory[Special].stack--;
                    if (player.inventory[Special].stack <= 0)
                    {
                        player.inventory[Special] = new Item();
                    }

                    Main.npcChatText = SpecialChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Rainbow>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (Item >= 0)
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (Item2 >= 0)
                {
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (Item3 >= 0)
                {
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (Item4 >= 0)
                {
                    player.inventory[Item4].stack--;
                    if (player.inventory[Item4].stack <= 0)
                    {
                        player.inventory[Item4] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeRed >= 0)
                {
                    player.inventory[DyeRed].stack--;
                    if (player.inventory[DyeRed].stack <= 0)
                    {
                        player.inventory[DyeRed] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Red>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeOrange >= 0)
                {
                    player.inventory[DyeOrange].stack--;
                    if (player.inventory[DyeOrange].stack <= 0)
                    {
                        player.inventory[DyeOrange] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Orange>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeYellow >= 0)
                {
                    player.inventory[DyeYellow].stack--;
                    if (player.inventory[DyeYellow].stack <= 0)
                    {
                        player.inventory[DyeYellow] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Yellow>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeGreen1 >= 0)
                {
                    player.inventory[DyeGreen1].stack--;
                    if (player.inventory[DyeGreen1].stack <= 0)
                    {
                        player.inventory[DyeGreen1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Green>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeGreen2 >= 0)
                {
                    player.inventory[DyeGreen2].stack--;
                    if (player.inventory[DyeGreen2].stack <= 0)
                    {
                        player.inventory[DyeGreen2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Green>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeGreen3 >= 0)
                {
                    player.inventory[DyeGreen3].stack--;
                    if (player.inventory[DyeGreen3].stack <= 0)
                    {
                        player.inventory[DyeGreen3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Green>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeBlue1 >= 0)
                {
                    player.inventory[DyeBlue1].stack--;
                    if (player.inventory[DyeBlue1].stack <= 0)
                    {
                        player.inventory[DyeBlue1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Blue>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeBlue2 >= 0)
                {
                    player.inventory[DyeBlue2].stack--;
                    if (player.inventory[DyeBlue2].stack <= 0)
                    {
                        player.inventory[DyeBlue2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Blue>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeBlue3 >= 0)
                {
                    player.inventory[DyeBlue3].stack--;
                    if (player.inventory[DyeBlue3].stack <= 0)
                    {
                        player.inventory[DyeBlue3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Blue>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyePurple1 >= 0)
                {
                    player.inventory[DyePurple1].stack--;
                    if (player.inventory[DyePurple1].stack <= 0)
                    {
                        player.inventory[DyePurple1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Purple>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyePurple2 >= 0)
                {
                    player.inventory[DyePurple2].stack--;
                    if (player.inventory[DyePurple2].stack <= 0)
                    {
                        player.inventory[DyePurple2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Purple>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyeGray >= 0)
                {
                    player.inventory[DyeGray].stack--;
                    if (player.inventory[DyeGray].stack <= 0)
                    {
                        player.inventory[DyeGray] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Gray>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else if (DyePink >= 0)
                {
                    player.inventory[DyePink].stack--;
                    if (player.inventory[DyePink].stack <= 0)
                    {
                        player.inventory[DyePink] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(ModContent.ItemType<Items.Mushrooms.Pink>(), 5);

                    Main.PlaySound(SoundID.Chat, -1, -1, 1);
                    return;
                }
                else
                {
                    Main.npcChatText = NoMushroomChat();
                    Main.npcChatCornerItem = 0;
                    Main.PlaySound(SoundID.MenuTick, -1, -1, 1);
                }
            }
        }

        public string NoMushroomChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Lang.TownNPCMushman("NoMushroomChat1"));
            chat.Add(Lang.TownNPCMushman("NoMushroomChat2"));
            chat.Add(Lang.TownNPCMushman("NoMushroomChat3"));
            return chat;
        }

        public string SpecialChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Lang.TownNPCMushman("SpecialChat1"));
            chat.Add(Lang.TownNPCMushman("SpecialChat2"));
            chat.Add(Lang.TownNPCMushman("SpecialChat3"));
            return chat;
        }

        public string MushroomChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Lang.TownNPCMushman("MushroomChat1"));
            chat.Add(Lang.TownNPCMushman("MushroomChat2"));
            chat.Add(Lang.TownNPCMushman("MushroomChat3"));
            return chat;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
		{
            shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SporeSac"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.RecallPotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("MyceliumSeeds"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.MushroomGrassSeeds);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.LesserManaPotion);
            nextSlot++;

            if (NPC.downedBoss3 == true)
            {
                shop.item[nextSlot].SetDefaults(ItemID.HealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ManaPotion);
                nextSlot++;
            }

            if (Main.hardMode == true)
            {
                shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GreaterManaPotion);
                nextSlot++;
            }
            if (NPC.downedMoonlord == true)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
                nextSlot++;
            }
            if (AAWorld.downedAncient == true)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("GrandHealingPotion"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GrandManaPotion"));
                nextSlot++;
            }
            if (AAWorld.downedSAncient == true)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("TheBigOne"));
                nextSlot++;
            }
        }

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Mushroom);
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType("Throwshroom");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)

        {
            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}