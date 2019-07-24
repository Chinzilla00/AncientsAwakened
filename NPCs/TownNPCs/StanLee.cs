using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class StanLee : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/StanLee";

        public override bool Autoload(ref string name)
		{
			name = "Illustrator";
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
            npc.dontTakeDamage = true;
            npc.dontTakeDamageFromHostiles = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return false;
		}

		public override string TownNPCName()
		{
            return "Stan Lee";
		}

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("You know, I guess one person can make a difference. 'Nuff said.");
            chat.Add("I thought you'd be taller.");
            chat.Add("Have you seen my shoe?");
            chat.Add("And then there was this one time I was a security guard, but uh...then I got fired...");
            chat.Add("Excelsior.");
            chat.Add("Hey, if you got a newspaper, could I borrow the sports section?.");
            chat.Add("I remember when I was a mail truck driver, got to deliver some mail to some famous guy. I think his name was Tony...Stank?");
            chat.Add("HAH..! THAT'S HILARIOUS!");
            chat.Add("Wow, nice suit.");
            chat.Add("Don't make me whip ya, you little punk.");
            if (Main.invasionType == InvasionID.MartianMadness)
            {
                chat.Add("Whats'a matter with you, ya never seen a spaceship before?");
            }
            return chat;
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
            shop.item[nextSlot].SetDefaults(ItemID.WebSlinger);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Accessories.IronBoots>());
            nextSlot++;
            if (NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Accessories.CapShield>());
                nextSlot++;
            }
            if (!NPC.downedMechBossAny)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Melee.Mjolnir>());
                nextSlot++;
            }
        }

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 50;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<StanMjolnir>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 8f;
            randomOffset = 5f;
        }
    }
}