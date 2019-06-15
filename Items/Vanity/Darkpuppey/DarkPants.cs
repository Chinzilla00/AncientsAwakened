using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Darkpuppey
{
    [AutoloadEquip(EquipType.Legs)]
	public class DarkPants : ModItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dark Flower Pants");
            Tooltip.SetDefault(@"Smells like flowers and... honey?
'Great for impersonating Ancients Awakened Devs!'");
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
            item.width = 22;
            item.height = 18;
            item.rare = 7;
            item.vanity = true;
        }
    }
}