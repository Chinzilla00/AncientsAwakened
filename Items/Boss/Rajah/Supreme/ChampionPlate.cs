using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class ChampionPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion Plate");
            Tooltip.SetDefault("Forged from Champium");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
			item.maxStack = 99;
            item.rare = 11;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

    }
}
