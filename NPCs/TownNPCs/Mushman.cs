using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Mushman : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "AAMod/NPCs/TownNPCs/Mushman";
            }
        }

        public override bool Autoload(ref string name)
        {
            name = "Mushman";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 24;
            NPCID.Sets.ExtraFramesCount[npc.type] = 7;
            NPCID.Sets.AttackFrameCount[npc.type] = 3;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 20;
            NPCID.Sets.HatOffsetY[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 56;
            npc.aiStyle = 7;
            npc.damage = 40;
            npc.defense = 38;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Truffle;
        }


        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
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
            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                default:
                    return "Mushman";
            }
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (Truffle >= 0 && Main.rand.Next(4) == 0)
            {
                chat.Add("Those glowing truffles are all just such downers.");
            }
            int WitchDoctor = NPC.FindFirstNPC(NPCID.WitchDoctor);
            if (WitchDoctor >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[WitchDoctor].GivenName + " offered to let me get in his hot tub one time. I denied because I had better things to do";
            }
            chat.Add("The Mushroom Monarch isn't all he seems, you know.");
            chat.Add("Don't ask where I get the mushrooms for my potions.");
            chat.Add("I got potions, you got money. Wanna trade?");
            int Clothier = NPC.FindFirstNPC(NPCID.Clothier);
            if (Clothier >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[Clothier].GivenName + " asked me one time if red truffles tasted as good as blue ones. Obviously not. Blue truffles are way saltier.";
            }
            return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Strange Plants";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }

            if (!firstButton)
            {
                Main.PlaySound(12, -1, -1, 1);

                Player player = Main.LocalPlayer;
                AAPlayer p = player.GetModPlayer<AAPlayer>(mod);

                int Item = player.FindItem(ItemID.StrangePlant1);
                int Item2 = player.FindItem(ItemID.StrangePlant2);
                int Item3 = player.FindItem(ItemID.StrangePlant3);
                int Item4 = player.FindItem(ItemID.StrangePlant4);

                string[] lootTable = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Brown", "Gray", "Pink" };
                int loot = Main.rand.Next(lootTable.Length);

                if (Item >= 0) //Item 1: 3 Blueberries
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 3);

                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item2 >= 0)
                {
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 3);

                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item3 >= 0)
                {
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 3);

                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item4 >= 0)
                {
                    player.inventory[Item4].stack--;
                    if (player.inventory[Item4].stack <= 0)
                    {
                        player.inventory[Item4] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(mod.ItemType(lootTable[loot]), 3);

                    Main.PlaySound(24, -1, -1, 1);
                }
                else
                {
                    Main.npcChatText = NoMushroomChat();
                    Main.npcChatCornerItem = 0;
                    Main.PlaySound(12, -1, -1, 1);
                }
            }
        }

        public string NoMushroomChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add("I need strange plants for something. Bring me some and I'll give you some special alchemical mushrooms. Good for making potions.");
            chat.Add("...no plants?");
            chat.Add("Plants please. I won't give you mushrooms without them.");
            return chat;
        }

        public string MushroomChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add("Thank you. These mushrooms are way more useful than worthless dyes, right?");
            chat.Add("Here. More colored mushrooms for all your brewing needs");
            chat.Add("What do I use these plants for? Uh...things.");
            return chat;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
		{
            shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SporeSac"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.RecallPotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
            nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.LesserManaPotion);

            if (NPC.downedBoss3 == true)
            {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ManaPotion);
            }
            if (Main.hardMode == true)
            {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GreaterManaPotion);
            }
            if (NPC.downedMoonlord == true)
            {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
            }
            if (AAWorld.downedAncient == true)
            {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GrandHealingPotion"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("GrandManaPotion"));
            }
            if (AAWorld.downedSAncient == true)
            {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("TheBigOne"));
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