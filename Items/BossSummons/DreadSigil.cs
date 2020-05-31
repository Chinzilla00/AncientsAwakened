
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class DreadSigil : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"A ragged old tablet said to contain the dark magic of a new moon
Summons Yamata
Can only be used at night in the mire
Non-Consumable");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = ItemRarityID.Green;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 500;
            item.consumable = false;
            item.rare = ItemRarityID.Red;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 10);
            recipe.AddIngredient(null, "DarkMatter", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
		{
            SpawnBoss(player, ModContent.NPCType<NPCs.Bosses.Yamata.Yamata>(), true, new Vector2(player.Center.X, player.Center.Y - 100),  Language.GetTextValue("Mods.AAMod.Common.Yamata"));
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            if (!AAWorld.downedYamata)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadSigilTrue1"), new Color(45, 46, 70));
            }
            if (AAWorld.downedYamata)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadSigilTrue2"), new Color(45, 46, 70));
            }

            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadTimeFalse"), new Color(45, 46, 70), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
			{
                if (!AAWorld.downedYamata && !player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadSigilMireFalse"), new Color(45, 46, 70), false);
                    return false;
                }
				if (NPC.AnyNPCs(mod.NPCType("Yamata")))
				{
					if(player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(45, 46, 70), false);
					return false;
				}
                if (NPC.AnyNPCs(mod.NPCType("YamataA")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("YamataTransition")))
                {
                    return false;
                }
                return true;
			}
			if(player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadMireFalse"), new Color(45, 46, 70), false);			
			return false;
		}

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
                npcCenter = player.Center;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(npcName + " " + Language.GetTextValue("Mods.AAMod.Common.BosshasAwoken"), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " " + Language.GetTextValue("Mods.AAMod.Common.BosshasAwoken")), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                Globals.AANet.SendNetMessage(Globals.AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

    }
}