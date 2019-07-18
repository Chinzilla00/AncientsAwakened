using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
	public class StanLee : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/StanLee";

        public override bool Autoload(ref string name)
		{
			name = Lang.TownNPCStanLee("StanLeeName");
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

            chat.Add(Lang.TownNPCStanLee("StanLeeChat1"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat2"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat3"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat4"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat5"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat6"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat7"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat8"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat9"));
            chat.Add(Lang.TownNPCStanLee("StanLeeChat10"));
            if (Main.invasionType == InvasionID.MartianMadness)
            {
                chat.Add(Lang.TownNPCStanLee("StanLeeChat11"));
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