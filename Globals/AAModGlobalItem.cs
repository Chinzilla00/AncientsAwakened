using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                /*item.color = Color.White;
                if (WorldGen.crimson)
                {
                    item.color = Color.DarkRed;
                }
                else
                {
                    item.color = Color.Violet;
                }*/
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.AnkhShield)
            {
                player.meleeSpeed += 0.07f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.AnkhShield)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.text == "Grants immunity to knockback and fire blocks")
                    {
                        tooltips.Remove(line1);
                    }
                }
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.text == "Grants immunity to most debuffs")
                    {
                        tooltips.Remove(line2);
                    }
                }
                TooltipLine line = new TooltipLine(mod, "AnkhShield", @"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
7% melee speed");
            }
        }
    }
}
