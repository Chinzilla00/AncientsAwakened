
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroRune : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ERR0R_NULL");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"ACTIVATES THE GR0UND ZER0 C0DE F0R THE NEAREST ZER0 UNIT
N0N-C0NSUMABLE");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 41));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Purple;
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
                    line2.overrideColor = AAColor.Oblivion;
                }
            }
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (NPC.AnyNPCs(mod.NPCType("Zero")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("ZeroProtocol")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitVoidZoneFalse"), new Color(255, 0, 0), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitRuneTrue"), Color.Red.R, Color.Red.G, Color.Red.B);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(mod.NPCType("ZeroDeactivated")))
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 300, mod.NPCType("ZeroProtocol"));
            }

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ZeroDeath"));
            return true;
        }
    }
}