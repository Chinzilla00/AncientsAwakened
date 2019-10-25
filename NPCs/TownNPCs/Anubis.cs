using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Anubis : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Anubis";

        public override bool Autoload(ref string name)
        {
            name = "Legendscribe";
            return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 26;
            npc.dontTakeDamageFromHostiles = true;
			NPCID.Sets.ExtraFramesCount[npc.type] = 10;
			NPCID.Sets.AttackFrameCount[npc.type] = 5;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 40;
			NPCID.Sets.AttackAverageChance[npc.type] = 20;
			NPCID.Sets.HatOffsetY[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 68;
			npc.lifeMax = 160000;
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
			animationType = NPCID.Guide;
            npc.lavaImmune = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

		public override void HitEffect(int hitDirection, double damage)
		{
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active && !NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Anubis>()) && 
                    !NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Forsaken.FATransition>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Forsaken.FATransition2>()) &&
                    !NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Forsaken.ForsakenAnubis>()))
                {
                    return true;
                }
            }
            return false;
		}

		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
                default:
					return "Anubis";
			}
		}

        public static bool SwitchInfo = false;
        public static bool DoNext = false;
        public static bool Mushroom = false;
        public static bool Glowshroom = false;
        public static bool Grips = false;
        public static bool Brood = false;
        public static bool Hydra = false;
        public static bool Djinn = false;
        public static bool Serpent = false;
        public static bool AnubisB = false;
        public static bool Athena = false;
        public static bool Greed = false;
        public static bool Rajah = false;
        public static bool AthenaA = false;
        public static bool GreedA = false;
        public static bool Equinox = false;
        public static bool Sisters = false;
        public static bool Akuma = false;
        public static bool Yamata = false;
        public static bool Zero = false;
        public static bool Shen = false;
        public static bool RajahC = false;
        public static bool BaseChat = false;
        public static int ChatNumber = 0;

        public override void ResetEffects()
        {
            SwitchInfo = false;
            DoNext = false;
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            AnubisB = false;
            Athena = false;
            Greed = false;
            Rajah = false;
            AthenaA = false;
            GreedA = false;
            Equinox = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
            RajahC = false;
        }
        
        public override void SetChatButtons(ref string button, ref string button2)
        {
			string SwitchInfoT = Lang.TownNPCAnubis("SetChatButtons1");

            string DoNextT = Lang.TownNPCAnubis("SetChatButtons2");

            string MushT = Lang.TownNPCAnubis("SetChatButtons3");

            string GlowT = Lang.TownNPCAnubis("SetChatButtons4");

            string GripT = Lang.TownNPCAnubis("SetChatButtons5");

            string BroodT = Lang.TownNPCAnubis("SetChatButtons6");

            string HydraT = Lang.TownNPCAnubis("SetChatButtons7");

            string DjinnT = Lang.TownNPCAnubis("SetChatButtons8");

            string SerpentT = Lang.TownNPCAnubis("SetChatButtons9");

            string AnubisT = Lang.TownNPCAnubis("SetChatButtons14");

            string AthenaT = "Squakin' Headache";

            string GreedT = "What a Worm";

            string RajahT = "Hopping Hoodlum";

            string AthenaAT = "Harpy Hags are back";

            string GreedAT = "Riches and R.I.P. you";

            string EquinoxT = Lang.TownNPCAnubis("SetChatButtons13");

            string SistersT = Lang.TownNPCAnubis("SetChatButtons15");

            string AkumaT = Lang.TownNPCAnubis("SetChatButtons16");

            string YamataT = Lang.TownNPCAnubis("SetChatButtons17");

            string ZeroT = Lang.TownNPCAnubis("SetChatButtons18");

            string ShenT = Lang.TownNPCAnubis("SetChatButtons19");

            string RajahCT = "Wrath of the Wabbit";
            
            button = SwitchInfoT;

            if (ChatNumber == 0)
			{
			    button2 = DoNextT;
                DoNext = true;
            }
            else if (ChatNumber == 1)
            {
                button2 = MushT;
                Mushroom = true;
            }
            else if (ChatNumber == 2)
            {
                button2 = GlowT;
                Glowshroom = true;
            }
            else if (ChatNumber == 3)
            {
                button2 = GripT;
                Grips = true;
            }
            else if (ChatNumber == 4)
            {
                button2 = BroodT;
                Brood = true;
            }
            else if (ChatNumber == 5)
            {
                button2 = HydraT;
                Hydra = true;
            }
            else if (ChatNumber == 6 && NPC.downedBoss3)
            {
                button2 = DjinnT;
                Djinn = true;
            }
            else if (ChatNumber == 7 && NPC.downedBoss3)
            {
                button2 = SerpentT;
                Serpent = true;
            }
            else if (ChatNumber == 8 && NPC.downedPlantBoss)
            {
                button2 = AnubisT;
                AnubisB = true;
            }
            else if (ChatNumber == 9 && AAWorld.downedAnubis)
            {
                button2 = AthenaT;
                Athena = true;
            }
            else if (ChatNumber == 10 && AAWorld.downedAnubis)
            {
                button2 = GreedT;
                Greed = true;
            }
            else if (ChatNumber == 11 && Main.hardMode)
            {
                button2 = RajahT;
                Rajah = true;
            }
            else if (ChatNumber == 12 && NPC.downedMoonlord && AAWorld.downedAnubis && AAWorld.downedAthena)
            {
                button2 = AthenaAT;
                AthenaA = true;
            }
            else if (ChatNumber == 13 && NPC.downedMoonlord && AAWorld.downedAnubis && AAWorld.downedGreed)
            {
                button2 = GreedAT;
                GreedA = true;
            }
            else if (ChatNumber == 14 && AAWorld.downedGreedA && AAWorld.downedAthenaA)
            {
                button2 = EquinoxT;
                Equinox = true;
            }
            else if (ChatNumber == 15 && NPC.downedMoonlord && AAWorld.downedEquinox)
            {
                button2 = SistersT;
                Sisters = true;
            }
            else if (ChatNumber == 16 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = AkumaT;
                Akuma = true;
            }
            else if (ChatNumber == 17 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = YamataT;
                Yamata = true;
            }
            else if (ChatNumber == 18 && NPC.downedMoonlord && AAWorld.downedNC)
            {
                button2 = ZeroT;
                Zero = true;
            }
            else if (ChatNumber == 19 && AAWorld.downedAllAncients)
            {
                button2 = ShenT;
                Shen = true;
            }
            else if (ChatNumber == 20 && AAWorld.downedRajahsRevenge)
            {
                button2 = RajahCT;
                RajahC = true;
            }
            else
            {
                ChatNumber = 0;
                button2 = DoNextT;
                DoNext = true;
            }
        }

        public void ResetBools()
        {
            DoNext = false;
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            AnubisB = false;
            Athena = false;
            Greed = false;
            Rajah = false;
            AthenaA = false;
            GreedA = false;
            Equinox = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
            RajahC = false;
        }

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				ResetBools();
				ChatNumber += 1;
				if (ChatNumber > 23)
				{
					ChatNumber = 0;
				}
			}
			else
            {
                Player player = Main.LocalPlayer;
                int Item = player.FindItem(ModContent.ItemType<Items.Misc.AnubisBook>());
                if (Item >= 0 && !player.GetModPlayer<AAPlayer>().AnubisBook && Greed)
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = @"You got it! My limited edition copy of my esteemed biogrophy! Thanks, pal. You know what? As a gift, you can have it. Here, I'll even autograph it for you.
...Whoops, I accidentally used my runic quill to sign it. Oh well, now it's magic.";
                    //player.QuickSpawnItem(Terraria.ModLoader.ModContent.ItemType<Items.Magic.AnubisTome>(), 1);

                    Main.PlaySound(24, -1, -1, 1);
                    return;
                }
                Main.npcChatText = BossChat();
			}
		}

        public override bool PreAI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Bosses.Anubis.Anubis>()))
            {
                Vector2 position = npc.Center + (Vector2.One * -20f);
                int num84 = 40;
                int height3 = num84;
                for (int num85 = 0; num85 < 3; num85++)
                {
                    int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                }
                for (int num87 = 0; num87 < 15; num87++)
                {
                    int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                    Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                    Main.dust[num88].noGravity = true;
                    Main.dust[num88].noLight = true;
                    Main.dust[num88].velocity *= 3f;
                    Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                    num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
                    Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                    Main.dust[num88].velocity *= 2f;
                    Main.dust[num88].noGravity = true;
                    Main.dust[num88].fadeIn = 1f;
                    Main.dust[num88].color = Color.Black * 0.5f;
                    Main.dust[num88].noLight = true;
                    Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
                }
                for (int num89 = 0; num89 < 10; num89++)
                {
                    int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                    Main.dust[num90].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                    Main.dust[num90].noGravity = true;
                    Main.dust[num90].noLight = true;
                    Main.dust[num90].velocity *= 3f;
                    Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
                }
                for (int num91 = 0; num91 < 30; num91++)
                {
                    int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                    Main.dust[num92].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                    Main.dust[num92].noGravity = true;
                    Main.dust[num92].velocity *= 3f;
                    Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
                }
                npc.active = false;
            }
            return false;
        }

        public static bool DoG => CalamityMod.World.CalamityWorld.downedDoG;

        public static string BossChat()
        {
            Player player = Main.LocalPlayer;
            if (Mushroom)
            {
                return AAWorld.downedMonarch ? Lang.TownNPCAnubis("downedMonarchY") : 
                    Lang.TownNPCAnubis("downedMonarchN");
            }
            else if (Glowshroom)
            {
                return AAWorld.downedFungus ? Lang.TownNPCAnubis("downedFungusY") : 
                    Lang.TownNPCAnubis("downedFungusN");
            }
            else if (Grips)
            {
                return AAWorld.downedGrips ? Lang.TownNPCAnubis("downedGripsY") : 
                    Lang.TownNPCAnubis("downedGripsN");
            }
            else if (Brood)
            {
                return AAWorld.downedBrood ? Lang.TownNPCAnubis("downedBroodY") :
                    Lang.TownNPCAnubis("downedBroodN");
            }
            else if (Hydra)
            {
                return AAWorld.downedHydra ? Lang.TownNPCAnubis("downedHydraY") : 
                    Lang.TownNPCAnubis("downedHydraN");
            }
            else if (Djinn)
            {
                return AAWorld.downedDjinn ? Lang.TownNPCAnubis("downedDjinnY") : 
                    Lang.TownNPCAnubis("downedDjinnN");
            }
            else if (Serpent)
            {
                return AAWorld.downedSerpent ? Lang.TownNPCAnubis("downedSerpentY") : 
                    Lang.TownNPCAnubis("downedSerpentN");
            }
            else if (AnubisB)
            {
                if (!BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
                {
                    player.QuickSpawnItem(ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return "You LOST the scepter?! I can't go handing these things out like candy, you know! Anyways, here's another one."; 
                }

                return AAWorld.downedAnubis ? "You could have gone a little easier on me, ya know. My back still hurts from that." :
                    "I hear there’s this lorekeeper guy that’s really jacked and handsome, and all the ladies love him for his amazing soul-judging abilities. What a guy.";
            }
            else if (Athena)
            {
                return AAWorld.downedAthena ? "Thank the Equinox, you shut those annoying little squakers up! I was about to roast one of them over a spit and have fried chicken for dinner if they shrieked at me one more time." :
                    "You know those screechin' harpies up in the sky? Well there are these REALLY obnoxious ones called seraphs who just WILL NOT SHUT UP!!! They have a leader in that sky palace to the east. Maybe if you give her the ol' one-two, they'll shut their yappers.";
            }
            else if (Greed)
            {
                return AAWorld.downedGreed ? (player.GetModPlayer<AAPlayer>().AnubisBook ? "Hey thanks for getting my book back. Greed stole it a while ago, probably because of the gold highlights I used to bind it. Look around that cave, maybe there's some other stuff he's stolen?" : 
                    "Hey uh...did you find my thing yet? No? Just dig around in that loot pile down there, I'm sure it's there somewhere.") :
                    "Hey uh, there's this HUGE hoard of treasure underground somewhere with lots of gold in it, but it's guarded by this really stingy worm. You should go check it out for a boatload of booty, but uh...there's something of mine down there. Could you go get it for me? Don't worry, when you see it, you'll know it's mine.";
            }
            else if (Rajah)
            {
                return AAWorld.downedRajah ? "You bested Rajah? Pft, yeah right, I've seen him trounce supposed gods before, there is no way you beat him..!" :
                    "Hey, you know those bunnies that hop around all the time? I uh...I wouldn't harass them if I were you. There's a legend around here of a sort of 'Guardian' of sorts that prtotects them from danger. Why is it a legend? Because apparently nobody has ever fought this thing and lived. Neat story, eh?";
            }
            else if (AthenaA)
            {
                return AAWorld.downedAthenaA ? "Huh? What's a Varian you ask? That's something I haven't heard in years...so long ago that I barely even remember the name. Although I do recall something about another one kicking around somewhere..." :
                    "Hey, did that annoying little witch find you? Sorry for telling her where you were, she wouldn't stop screeching in my ear. Anyways, looks like Athena wants a rematch. Stay on your guard, bud. This seems like a trap...";
            }
            else if (GreedA)
            {
                if (ModLoader.GetMod("CalamityMod") != null)
                {
                    
                    Mod calamity = ModLoader.GetMod("CalamityMod");
                    if (calamity != null && DoG && AAWorld.downedGreedA)
                    {
                        return "Ya know, you duking it out with the Devourer of Gods reminded me of Greed a bit...I mean think about it. They both have wormhole capabilities and they both adapt to what they eat. Could they possibly be...nah, that'd be rediculous...or..?";
                    }
                }
                return AAWorld.downedGreedA ? "So he WAS hiding his true power all along. I wonder why, though...could he be hiding from something, perhaps..?" :
                    "You know, I seem to remember a story about ol' grabby-mc-steal-your-crap. When he first showed up in these parts, he was much stronger than he was when you kicked his rear end. Maybe he got weaker as time went on..? Or maybe...nah, that couldn't be it.";
            }
            else if (Equinox)
            {
                return AAWorld.downedEquinox ? Lang.TownNPCAnubis("downedEquinoxY") : 
                    Lang.TownNPCAnubis("downedEquinoxN");
            }
            else if (Sisters)
            {
                return AAWorld.downedSisters ? Lang.TownNPCAnubis("downedSistersY") : 
                    Lang.TownNPCAnubis("downedSistersN");
            }
            else if (Akuma)
            {
                return AAWorld.downedAkuma ? Lang.TownNPCAnubis("downedAkumaY") : 
                    Lang.TownNPCAnubis("downedAkumaN");
            }
            else if (Yamata)
            {
                return AAWorld.downedYamata ? Lang.TownNPCAnubis("downedYamataY") :
                    Lang.TownNPCAnubis("downedYamataN");
            }
            else if (Zero)
            {
                return AAWorld.downedZero ? Lang.TownNPCAnubis("downedZeroY") : 
                    Lang.TownNPCAnubis("downedZeroN");
            }
            else if (Shen)
            {
                return AAWorld.downedShen ? Lang.TownNPCAnubis("downedShenY") :
                    Lang.TownNPCAnubis("downedShenN");
            }
            else if (RajahC)
            {
                return AAWorld.downedShen ?  Lang.TownNPCAnubis("downedRajahCY") :
                    Lang.TownNPCAnubis("downedRajahCN");
            }
            else
            {
                return GuideChat();
            }
        }

        public static string GuideChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            if (!AAWorld.downedYamata)
            {
                chat.Add(Lang.TownNPCAnubis("AkumaGuideChat"));
            }

            if (!AAWorld.downedAkuma)
            {
                chat.Add(Lang.TownNPCAnubis("YamataGuideChat"));
            }
            if (Main.rand.Next(2) == 0)
            {
                chat.Add(Lang.TownNPCAnubis("BroodMotherGuideChat"));
            }
            else
            {
                chat.Add(Lang.TownNPCAnubis("HydraGuideChat"));
            }
            chat.Add(Lang.TownNPCAnubis("VoidGuideChat"));
            if (Main.hardMode)
            {
                chat.Add(Lang.TownNPCAnubis("HardModeGuideChat1"));
                chat.Add(Lang.TownNPCAnubis("HardModeGuideChat2"));
            }

            if (AAWorld.downedEquinox)
            {
                chat.Add(Lang.TownNPCAnubis("EquinoxBossGuideChat"));
            }
            return chat;
        }

        public override string GetChat()
        {
            Mod GRealm = ModLoader.GetMod("Grealm");
            Mod Fargos = ModLoader.GetMod("Fargowiltas");
            Mod Redemption = ModLoader.GetMod("Redemption");
            Mod Thorium = ModLoader.GetMod("ThoriumMod");

            int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(GRealm.NPCType("HordeZombie"));
            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(Fargos.NPCType("Mutant"));
            int Newb = Redemption == null ? -1 : NPC.FindFirstNPC(Redemption.NPCType("Newb"));
            int Cobbler = Thorium == null ? -1 : NPC.FindFirstNPC(Thorium.NPCType("Cobbler"));
            int ConfusedZombie = Thorium == null ? -1 : NPC.FindFirstNPC(Thorium.NPCType("ConfusedZombie"));

            WeightedRandom<string> chat = new WeightedRandom<string>();

            Player player = Main.LocalPlayer;
            AAPlayer mPlayer = player.GetModPlayer<AAPlayer>();

            if (player.head == ModContent.ItemType<Items.Vanity.Mask.AnubisMask>() && Main.rand.Next(5) == 0)
            {
                return Lang.TownNPCAnubis("AnubisChatMask");
            }

            chat.Add(Lang.TownNPCAnubis("AnubisChat1"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat2"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat3"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat4"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat5"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat6"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat7"));
            if (AAWorld.downedDjinn)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat8"));
            }
            chat.Add(Lang.TownNPCAnubis("AnubisChat9"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat10"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat11") + (WorldGen.crimson ? Lang.TownNPCAnubis("AnubisChat12") : Lang.TownNPCAnubis("AnubisChat13")) + Lang.TownNPCAnubis("AnubisChat14"));
            chat.Add(Lang.TownNPCAnubis("AnubisChat15"));
            


            int FemaleNPC = NPC.FindFirstNPC(FindFemaleNPC());


            if (Main.bloodMoon && FemaleNPC != NPCID.PartyGirl)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat16") + Main.npc[FemaleNPC].GivenName + Lang.TownNPCAnubis("AnubisChat17"));
            }
            else if (Main.bloodMoon && FemaleNPC == NPCID.PartyGirl)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat18") + Main.npc[FemaleNPC].GivenName + Lang.TownNPCAnubis("AnubisChat19"));
            }

            if (player.head == 200 && player.body == 198 && player.legs == 142)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat20"));
            }

            if (BirthdayParty.GenuineParty || BirthdayParty.ManualParty)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat21"));
            }

            if (HordeZombie >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat23") + Main.npc[HordeZombie].GivenName + Lang.TownNPCAnubis("AnubisChat24"));
            }

            if (Mutant >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat25") + Main.npc[Mutant].GivenName + Lang.TownNPCAnubis("AnubisChat26"));
            }

            if (Newb >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat27") + Main.npc[Newb].GivenName + Lang.TownNPCAnubis("AnubisChat28"));
            }

            if (Cobbler >= 0)
            {
                chat.Add(Main.npc[Cobbler].GivenName + Lang.TownNPCAnubis("AnubisChat29"));
            }

            if (ConfusedZombie >= 0)
            {
                chat.Add(Lang.TownNPCAnubis("AnubisChat30") + Main.npc[ConfusedZombie].GivenName + Lang.TownNPCAnubis("AnubisChat31"));
            }

            if (AAWorld.AMessage && !BasePlayer.HasItem(player, ModContent.ItemType<Items.BossSummons.Scepter>()))
            {
                if (!mPlayer.GivenAnuSummon)
                {
                    mPlayer.GivenAnuSummon = true;
                    player.QuickSpawnItem(ModContent.ItemType<Items.BossSummons.Scepter>(), 1);
                    return "Hey, thanks for getting back to me. I wanna test your strength. After you thrashed those mechanical meatheads, I'm interested in seeing how you fair against someone like me. Here, take this scepter and go use it in the desert on the surface whenever you're ready. I'm ready whenever.";
                }
            }

            return chat;
        }

        public static string WHATTHEFUCKDOIDOANUBIS()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            
            return chat;
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
            projType = ModContent.ProjectileType<JudgementNPC>();
            attackDelay = 5;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }

        public int FindFemaleNPC()
        {
            int FemaleNPC = Main.rand.Next(6);
            switch (FemaleNPC)
            {
                case 0:
                    FemaleNPC = NPCID.Nurse;
                    break;
                case 1:
                    FemaleNPC = NPCID.Dryad;
                    break;
                case 2:
                    FemaleNPC = NPCID.Stylist;
                    break;
                case 3:
                    FemaleNPC = NPCID.Mechanic;
                    break;
                case 4:
                    FemaleNPC = NPCID.Steampunker;
                    break;
                default:
                    FemaleNPC = NPCID.PartyGirl;
                    break;
            }
            return FemaleNPC;
        }
    }
}