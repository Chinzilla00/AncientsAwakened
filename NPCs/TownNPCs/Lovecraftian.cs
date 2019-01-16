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

        Mod Fargos = ModLoader.GetMod("FargoMod");
        Mod GRealm = ModLoader.GetMod("Grealm");
        private bool Purity = false;
        private bool Snow = false;
        private bool Desert = false;
        private bool Corruption = false;
        private bool Crimson = false;
        private bool Inferno = false;
        private bool Mire = false;
        private bool Void = false;
        private bool Hallow = false;


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
					return "Aletheia";
				case 1:
					return "C'thalpa";
				case 2:
					return "D’endrrah";
                case 3:
                    return "Ycnàgnnisssz";
                default:
                    return "Yidhra";				
			}
		}
        
        public override string GetChat()
        {
            int Pirate = NPC.FindFirstNPC(NPCID.Pirate);
            int Mutant = NPC.FindFirstNPC(Fargos.NPCType("Mutant"));
            int HordeZombie = NPC.FindFirstNPC(GRealm.NPCType("HordeZombie"));

            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("You know, where I’m from, I’m what you would call in your world ‘hot stuff.’");

            chat.Add("I wasn’t the only thing that came here when I did. A whole bunch of other stuff came through with me when a spacial rift opened up in my world. Stuff like the Eye of Cthulhu and the Brain of Cthulhu were already here though. No clue where those two came from.");

            chat.Add("...what are you looking at? Ever seen a squid-person before?");

            chat.Add("Yes I’m a woman. What about it? Is it the tentacle beard that threw you off?");

            chat.Add("If you have any sense of self preservation, I’d avoid that sunken ship in the ocean just off the coast. Scary things from my neck of the woods hang out there, especially...nevermind.");

            chat.Add("Ever just find things in your tentacles that you don’t know how they got there ? No ? Just me ?");

            chat.Add("Hey, your world is pretty interesting. Could you bring me some samples from different biomes for me to study ? If you do, I can make some neat stuff to trade with you.");

            
            

            //If Pirate is present
            if (Pirate >= 0 && Main.rand.Next(10) == 0)
            {
                chat.Add("Oh.This is awkward. Poor " + Main.npc[Pirate].GivenName + ". His ship was the one that got destroyed when I got ripped here unwillingly.");
            }

            //If mutant is present

            if (Mutant >= 0 && Main.rand.Next(10) == 1)
            {
                chat.Add("That " + Main.npc[Mutant].GivenName + " is talking out of his ass. Cthulhu would most likely squash him without any effort.");
            }

            //If Horde Zombie is present
            if (HordeZombie >= 0 && Main.rand.Next(10) == 2)
            {
                chat.Add("That dead guy shambling around freaks me out, and that’s saying something considering I’m a walking horror story. I don’t know, I just feel like he knows too much...");
            }


            //Post - Moon Lord
            if (NPC.downedMoonlord && Main.rand.Next(10) == 2)
            {
                chat.Add("Fun fact; The Moon Lord and Cthulhu are brothers. At least that’s what some pink pixie lady I met one time told me.");
            }

            //Providing materials

            //Purity
            chat.Add("Thanks. These forests are so green, reminds me of home...except where I'm from, it's green everywhere.");

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
            //button = "Shop"; button2 = "Research";
        }

        public static int[] ResearchItems = new int[1];
        public static int[] ResearchItemCounts = new int[1];

        public static void OnLoad(Mod mod)
        {
            ResearchItems = new int[] { ItemID.Sunflower, ItemID.IceBlock, ItemID.Cactus, ItemID.RottenChunk, ItemID.Vertebrae, mod.ItemType<Items.Materials.DragonScale>(), mod.ItemType<Items.Materials.MirePod>(), mod.ItemType<Items.Materials.DragonScale>(), mod.ItemType("AdvancedCircuitry"), mod.ItemType("LivingBranch"), mod.ItemType("AncientBoneFragments"), mod.ItemType("AcidSac"), mod.ItemType("BloodFang"), mod.ItemType("Corpsethorn") };
            ResearchItemCounts = new int[] { 2, 40, 1, 1, 1, 1, 1, 1, 2, 2, 1, 3, 5, 5 };
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
