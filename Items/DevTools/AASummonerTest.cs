using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.DevTools
{
    public class AASummonerTest : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AA SummonerTest");
            Tooltip.SetDefault(@"Test the minion's stat");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string text = "";
            text += "Your max minionslots: " + Main.player[Main.myPlayer].maxMinions + "\n";
            text += "Used minionslots: " + Main.player[Main.myPlayer].slotsMinions;

            TooltipLine line = new TooltipLine(mod, "newtooltip", text);
            list.Insert(2,line);
        } 
    }
}
