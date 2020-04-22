using System.Collections.Generic;

using Microsoft.Xna.Framework;

//using AAMod.NPCs.Bosses.Infinity;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class DreadRune : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Rune");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"An enchanted tablet radiating dark chaotic energy
Summons Yamata no Orochi
Can only be used in the mire at night
Non-Consumable");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(146, 30, 68);
                }
            }
        }

        public override bool UseItem(Player player)
		{
            if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadRuneTrue1"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadRuneTrue2"), new Color(146, 30, 68));
            DreadSigil.SpawnBoss(player, ModContent.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>(), false, new Vector2(player.Center.X, player.Center.Y - 100), Language.GetTextValue("Mods.AAMod.Common.YamataA"));
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            return true;
		}

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadTimeFalse"), new Color(45, 46, 70), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                if (!player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse1"), Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("Yamata")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(45, 46, 70), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("YamataA")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadFalse2"), new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenSpawn>()) || 
                    NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDeath>()) || || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("YamataTransition")))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DreadMireFalse"), new Color(45, 46, 70), false);
            return false;
        }
		
	}
}