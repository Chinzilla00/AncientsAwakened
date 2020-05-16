using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Equinox;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AAMod.Items.BossSummons
{
    public class EquinoxWorm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Worm");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"A worm created using celestial materials
Summons the Equinox Worms
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
            item.rare = ItemRarityID.Purple;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<Tiles.Altar.WormSpawn>());
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken"), 175, 75, 255, false); }
                else if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken")), new Color(175, 75, 255), -1);
                }
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("DaybringerHead"), false, 0, 0);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("NightcrawlerHead"), false, 0, 0);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}