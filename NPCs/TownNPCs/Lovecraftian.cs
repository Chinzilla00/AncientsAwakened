using System;
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
            npc.damage = 40;
            npc.defense = 38;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
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
			Mod Fargos = ModLoader.GetMod("FargoMod");
			Mod GRealm = ModLoader.GetMod("Grealm");

            WeightedRandom<string> chat = new WeightedRandom<string>();


            int Pirate = NPC.FindFirstNPC(NPCID.Pirate);
            int Mutant = (Fargos == null ? -1 : NPC.FindFirstNPC(Fargos.NPCType("Mutant")));
            int HordeZombie = (GRealm == null ? -1 : NPC.FindFirstNPC(GRealm.NPCType("HordeZombie")));

            chat.Add("You know, where I’m from, I’m what your world would call ‘hot stuff.’");

            chat.Add("I wasn’t the only thing that came here. A whole bunch of other stuff came through with me when a spacial rift opened up in my world. Stuff like the Eye of Cthulhu and the Brain of Cthulhu were already here though. No clue where those two came from.");

            chat.Add("...What are you looking at? You act like you've never seen a squid-person before.");

            chat.Add("Yes I’m a woman. What about it? Is it the tentacle beard that threw you off?");

            chat.Add("If you have any sense of self preservation, I’d avoid that sunken ship in the ocean just off the coast. Scary things from my neck of the woods hang out there, especially... nevermind.");

            chat.Add("Ever just find things in your tentacles that you don’t know how they got there? No? Just me?");

            //chat.Add("Hey, your world is pretty interesting. Could you bring me some samples from different biomes for me to study ? If you do, I can make some neat stuff to trade with you.");

            
            

            //If Pirate is present
            if (Pirate >= 0)
            {
                chat.Add("Oh. This is awkward. Poor " + Main.npc[Pirate].GivenName + ". His ship was the one that got destroyed when I fell out of that rift.");
            }

            //If mutant is present

            if (Mutant >= 0)
            {
                chat.Add("That " + Main.npc[Mutant].GivenName + " is talking out of his ass. Cthulhu would most likely squash him without any effort.");
            }

            //If Horde Zombie is present
            if (HordeZombie >= 0)
            {
                chat.Add("That dead guy shambling around freaks me out, and that’s saying something considering I’m a walking horror story. I don’t know, I just feel like he knows too much...");
            }


            //Post - Moon Lord
            if (NPC.downedMoonlord)
            {
                chat.Add("Fun fact; The Moon Lord and Cthulhu are brothers. At least that’s what some pink pixie lady I met one time told me.");
            }

            //Providing materials

            //Purity
            //chat.Add("Thanks. These forests are so green, reminds me of home... Except where I'm from, it's green everywhere.");

            return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
        }

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
            if (AAWorld.downedBrood)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.AshJar>());
                nextSlot++;
            }
            if (AAWorld.downedHydra)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.DarkwaterFlask>());
                nextSlot++;
            }
            if (AAWorld.downedBrood && AAWorld.downedHydra)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.OrderBottle>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.VoidBomb>());
                nextSlot++;
            }

        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<EyeShot>();
            attackDelay = 1;
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

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}
