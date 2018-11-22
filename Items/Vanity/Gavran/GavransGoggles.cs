using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Gavran
{
    [AutoloadEquip(EquipType.Head)]
	public class GavransGoggles : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gavran's Shark Mask");
            Tooltip.SetDefault(
@"'Great for impersonating Ancients Awakened Devs!'");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(72, 232, 193);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 14;
            item.rare = 9;
            item.vanity = true;
        }
	}
}