using BaseMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroRune : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ERR0R_NULL");
            Tooltip.SetDefault(@"ACTIVATES THE GR0UND ZER0 C0DE F0R THE NEAREST ZER0 UNIT
N0N-C0NSUMABLE");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
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
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("ZeroFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("ZeroProtocol")))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("ZeroFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("ZeroVoidZoneFalse"), new Color(255, 0, 0), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("ZeroRuneTrue"), Color.Red.R, Color.Red.G, Color.Red.B);

            if (Main.netMode != 1)
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