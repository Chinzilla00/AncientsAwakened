using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Chinzilla
{
    [AutoloadEquip(EquipType.Body)]
	public class ChinSuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fluffy Chinchilla Suit");
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
			item.width = 30;
			item.height = 20;
			item.rare = 10;
			item.vanity = true;
		}
	}
}