using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Moon
{
    [AutoloadEquip(EquipType.Head)]
	public class MoonHood : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Hood");
            Tooltip.SetDefault(@"The hood of a legendary lunar mage
'Great for impersonating Ancients Awakened Devs!'");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.vanity = true;
        }
	}
}