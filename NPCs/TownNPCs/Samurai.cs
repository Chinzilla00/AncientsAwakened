using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Events;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Samurai : ModNPC
	{
		public override string Texture
		{
			get
			{
                if (BirthdayParty.GenuineParty || BirthdayParty.ManualParty)
                {
                    return "AAMod/NPCs/TownNPCs/SamuraiParty";
                }
                else
                {
                    return "AAMod/NPCs/TownNPCs/Samurai";
                }
			}
		}

		public override string[] AltTextures
		{
			get
			{
				return new string[] { "AAMod/NPCs/TownNPCs/SamuraiParty" };
            }
        }

		public override bool Autoload(ref string name)
		{
			name = "Samurai";
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
			NPCID.Sets.HatOffsetY[npc.type] = -2;
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
                    if (AAWorld.downedGrips == true)
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
                chat.Add("I've known " + Main.npc[TravellingMerchant].GivenName + " for a while. He's quite the man of culture.");
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.Next(4) == 0)
            {
                return "I'm not really a fan of " + Main.npc[DD2Bartender].GivenName + "'s ale. It's a bit strong for my taste. I prefer Sake to be entirely honest.";
            }
            chat.Add("The chaos biomes weren't always so.... err.... chaotic.");
            chat.Add("Have you seen my sword? I can't seem to find it anywhere.");
            chat.Add("We used to be the most powerful nation in all of terraria... then... HE came...");
			chat.Add("I remember my old master giving me wise words such as: 'YOU ARE TRYING TO VIEW FLASH CONTENT BUT YOU DO NOT HAVE A FLASH PLAYER INSTALLED'... I’m still trying to figure that one out.");
			chat.Add("I'll be honest, I get half my lines from fortune cookies. Want one?");
			chat.Add("If you refuse to accept anything but the best, you very often get it.");
			chat.Add("Change can hurt, but it leads a path to something better.");
			chat.Add("You cannot love life until you live the life you love.");
			chat.Add("Land is always on the mind of a flying bird.");
			chat.Add("Our deeds determine us, as much as we determine our deeds.");
			chat.Add("Never give up. You're not a failure if you don't give up.");
			chat.Add("You already know the answer to the questions lingering inside your head.");
			chat.Add("It is now, and in this world, that we must live.");
			chat.Add("You can make your own happiness.");
			chat.Add("If winter comes, can spring be far behind?");
			chat.Add("A stranger is a friend you have not spoken to yet.");
			chat.Add("Your shoes will make you happy today.");
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
            shop.item[nextSlot].SetDefaults(ItemID.DynastyWood);
            nextSlot++;
            if (Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ItemID.RedDynastyShingles);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.InfernoSeeds>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.Sunpowder>());
                nextSlot++;
                if (AAWorld.downedBrood == true)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType<Items.BossSummons.DragonBell>());
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
            }
            if (!Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ItemID.BlueDynastyShingles);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.MireSeeds>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.Moonpowder>());
                nextSlot++;
                if (AAWorld.downedHydra == true)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType<Items.BossSummons.HydraChow>());
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
            }
			shop.item[nextSlot].SetDefaults(ItemID.Sake);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Pho);
			nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.PadThai);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Gi);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Kimono);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.FancyDishes);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Katana);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Shuriken);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaHood);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaShirt);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaPants);
            nextSlot++;
            if (Main.dayTime)
            {
                if (Main.hardMode == true)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("OrangeSolution"));
                    nextSlot++;
                }
            }
            if (!Main.dayTime)
            {
                if (Main.hardMode == true)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("IndigoSolution"));
                    nextSlot++;
                }
            }

            shop.item[nextSlot].SetDefaults(mod.ItemType("OrderSolution"));
            nextSlot++;
        }

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Katana);
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
            projType = ProjectileID.Shuriken;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}