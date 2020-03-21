using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    /*[AutoloadHead]
	public class Alpha : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Alpha";

        public override bool Autoload(ref string name)
		{
			name = "Mud Fish";
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
			NPCID.Sets.HatOffsetY[npc.type] = 3;
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
            animationType = NPCID.Guide;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
		}

        private readonly bool spawnable = AAWorld.downedMonarch || AAWorld.downedFungus || AAWorld.downedGrips || AAWorld.downedBrood || AAWorld.downedHydra || AAWorld.downedSerpent || AAWorld.downedDjinn || AAWorld.downedSag ||
        AAWorld.downedAnubis || AAWorld.downedGreed || AAWorld.downedAthena || AAWorld.downedRajah || AAWorld.downedAnubisA || AAWorld.downedGreedA || AAWorld.downedAthenaA || AAWorld.downedEquinox || AAWorld.downedSisters || 
        AAWorld.downedAkuma || AAWorld.downedYamata || AAWorld.downedZero || AAWorld.downedRajah || AAWorld.downedShen;


        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (spawnable)
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
            return "Alphakip";
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("If I ever hear you say the phrase 'Kwispy', this game's rating will become AO real quick.");
            chat.Add(@"You think I'm suspicious? I mean you could say I'm a bit

Fishy");
            chat.Add("Fun fact, I'm not actually a fish. I'm amphibious.");
            chat.Add("Go ask Anubis about where to go, he's smarter than I am about stuff.");

			return chat;
		}


		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Vanity Shop";
			button2 = "Weapon Shop";
		}

		public static bool VanityShop = true;

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				VanityShop = true;
				shop = true;
			}
			else
			{
				VanityShop = false;
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			if (VanityShop)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Alphakip.AlphaBag>());
				shop.item[nextSlot].value = 50000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Vanity.Dallin.FezLordsBag>());
				shop.item[nextSlot].value = 50000;
				nextSlot++;
			}
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
    }*/
}