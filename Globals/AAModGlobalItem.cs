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
                TooltipLine line = new TooltipLine(mod, "AnkhShield", "7% melee speed");
                tooltips.Add(line);
            }
        }
    }
}
