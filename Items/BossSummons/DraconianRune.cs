using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using AAMod.NPCs.Bosses.Akuma;
using System.Collections.Generic;

using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class DraconianRune : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Sun Rune");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"An enchanted tablet bursting with flaming chaotic energy
Summons Akuma Awakened
Only Usable during the day in the inferno
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 191, 255);
                }
            }
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (!player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneFalse1"), Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneFalse2"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneFalse2"), new Color(0, 191, 255), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.Shen>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDeath>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Shen.ShenDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("AkumaTransition")))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneInfernoFalse"), new Color(180, 41, 32), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneTrue1"), new Color(175, 75, 255));
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DraconianRuneTrue2"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("AkumaA"), false, 0, 0, Language.GetTextValue("Mods.AAMod.Common.AkumaA"), false);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }
    }
}