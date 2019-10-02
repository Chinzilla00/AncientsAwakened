using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class GoblinSlayer : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/GoblinSlayer";

        public override bool Autoload(ref string name)
		{
			name = "Goblin Slayer";
			return mod.Properties.Autoload;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Slayer");
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
            npc.height = 40;
            npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 80;
			npc.defense = 98;
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
            if (!AAConfigClient.Instance.NoAATownNPC)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (player.active)
                    {
                        if (NPC.downedGoblins == true)
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
			return "Goblin Slayer";
		}
        

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Goblin = NPC.FindFirstNPC(NPCID.GoblinTinkerer);
			if (Goblin >= 0 && Main.rand.Next(4) == 0)
			{
                chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat1") + Main.npc[Goblin].GivenName + Lang.TownNPCGoblinSlayer("GoblinSlayerChat2"));
            }
            int DD2Bartender = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (DD2Bartender >= 0 && Main.rand.Next(4) == 0)
            {
                return Main.npc[DD2Bartender].GivenName + Lang.TownNPCGoblinSlayer("GoblinSlayerChat3");
            }
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat4"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat5"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat6"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat7"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat8"));
            chat.Add(Lang.TownNPCGoblinSlayer("GoblinSlayerChat9"));
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
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Armor.GoblinSlayer.GoblinSlayerHelm>());
            shop.item[nextSlot].shopCustomPrice = new int?(10);
            shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Armor.GoblinSlayer.GoblinSlayerChest>());
            shop.item[nextSlot].shopCustomPrice = new int?(15);
            shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Armor.GoblinSlayer.GoblinSlayerGreaves>());
            shop.item[nextSlot].shopCustomPrice = new int?(12);
            shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Melee.GoblinSlayer>());
            shop.item[nextSlot].shopCustomPrice = new int?(15);
            shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.GoblinBattleStandard);
            shop.item[nextSlot].shopCustomPrice = new int?(5);
            shop.item[nextSlot].shopSpecialCurrency = AAMod.GoblinSoul;
            nextSlot++;
        }

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), mod.ItemType<Items.Melee.GoblinSlayer>());
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 80;
			knockback = 3f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.ThrowingKnife;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)

        {

            multiplier = 9f;

            randomOffset = 1f;

        }
    }
}