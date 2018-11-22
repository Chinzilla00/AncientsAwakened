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
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 7;
            NPCID.Sets.AttackFrameCount[npc.type] = 3;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 20;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
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
            npc.knockBackResist = 2f;
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
                return Main.npc[WitchDoctor].GivenName + "offered to let me get in his hot tub one time. I denied because I had better things to do";
            }
            chat.Add("The Mushroom Monarch isn't all he seems, you know.");
            chat.Add("Don't ask where I get the mushrooms for my potions.");
            chat.Add("I got potions, you got money. Wanna trade?");
            int Clothier = NPC.FindFirstNPC(NPCID.Clothier);
            if (WitchDoctor >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[WitchDoctor].GivenName + "Asked me one time if red truffles tasted as good as blue ones. Obviously not. Blue truffles are way saltier.";
            }
            return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
        }

		/* 
		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();

			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.Next(4) == 0)
			{
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}
			chat.Add("Sometimes I feel like I'm different from everyone else here.");
			chat.Add("What's your favorite color? My favorite colors are white and black.");
			chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
			chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
			chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}
		*/

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("Mycelium"));
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