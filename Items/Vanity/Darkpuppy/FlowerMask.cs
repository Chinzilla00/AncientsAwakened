using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Darkpuppy
{
    [AutoloadEquip(EquipType.Head)]
	public class FlowerMask : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flowery Mask");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 246, 124);
                }
            }
        }


        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.rare = 9;
            item.vanity = true;
        }
	}
}