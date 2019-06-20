using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Gibs
{
    [AutoloadEquip(EquipType.Legs)]
	public class GibsShorts : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Revenant Legs");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override bool DrawLegs()
        {
            return false;
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 10;
            item.vanity = true;
        }
    }
}