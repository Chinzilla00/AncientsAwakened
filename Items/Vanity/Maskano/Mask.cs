using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Maskano
{
    [AutoloadEquip(EquipType.Head)]
	public class Mask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("The Mask");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(74, 167, 47);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 30;
            item.rare = 9;
            item.vanity = true;
        }
	}
}