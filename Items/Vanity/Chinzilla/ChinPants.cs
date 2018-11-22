using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Chinzilla
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChinPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fluffy Chinchilla Pants");
			Tooltip.SetDefault("Great for impersonating AA devs!");
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
			item.width = 22;
			item.height = 14;
			item.rare = 10;
			item.vanity = true;
		}
	}
}