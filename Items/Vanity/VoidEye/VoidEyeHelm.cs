using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.VoidEye
{
    [AutoloadEquip(EquipType.Head)]
	public class VoidEyeHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Eye's Mask");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(148, 18, 142);
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