using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Apawn
{
    [AutoloadEquip(EquipType.Legs)]
	public class ApawnBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Apawn's Greaves");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Testers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(162, 116, 55);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = 9;
            item.vanity = true;
        }
    }
}