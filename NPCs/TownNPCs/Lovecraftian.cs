using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Lovecraftian : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "AAMod/NPCs/TownNPCs/Lovecraftian";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Lovecraftian";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[npc.type] = 10;
			NPCID.Sets.AttackFrameCount[npc.type] = 5;
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
			npc.damage = 60;
			npc.defense = 58;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 2f;
			animationType = NPCID.Guide;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			for (int k = 0; k < 255; k++)
			{
				Player player = Main.player[k];
				if (player.active)
				{
                    if (NPC.downedBoss1 == true)
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
				case 0:
					return "Nobunaga";
				case 1:
					return "Hattori";
				case 2:
					return "Hanzo";
                case 3:
                    return "Genji";
                case 4:
                    return "Oda";
                default:
					return "Hideyoshi";
			}
		}
        
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int TravellingMerchant = NPC.FindFirstNPC(NPCID.TravellingMerchant);
			if (TravellingMerchant >= 0 && Main.rand.Next(4) == 0)
			{
                chat.Add("I've known " + Main.npc[TravellingMerchant].GivenName + " for a while. He's quite a man of culture.");
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.Next(4) == 0)
            {
                return "I'm not really a fan of " + Main.npc[DD2Bartender].GivenName + "'s ale. It's a bit strong for my taste. I prefer Sake to be entirely honest.";
            }
            chat.Add("The chaos biomes weren't always....err....chaotic.");
            chat.Add("Have you seen my sword? I can't seem to find it anywhere.");
            chat.Add("We used to be the most powerful nation in all of terraria...then...HE came...");
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
				//shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 30;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<Eyeshot>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)

        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}