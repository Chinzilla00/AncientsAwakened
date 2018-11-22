using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Chinzilla
{
    [AutoloadEquip(EquipType.Head)]
	public class ChinMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Fluffy Chinchilla Mask");
			Tooltip.SetDefault("'Great for impersonating AA devs!'");
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 128, 64);
                }
            }
        }
        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.rare = 10;
			item.vanity = true;
		}
	}
}