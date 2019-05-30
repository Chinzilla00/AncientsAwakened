using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Armor.Dev.Grox
{
    [AutoloadEquip(EquipType.Head)]
	public class AngryPirateHood : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Angry Pirate's Hood");
            Tooltip.SetDefault(@"Hatred towards fish that can't code radiates from this hood.
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override bool DrawHead()
        {
            return false;
        }
        
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 7;
            item.vanity = true;
        }
	}
}