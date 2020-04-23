using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Maskano
{
    [AutoloadEquip(EquipType.Legs)]
	public class MaskBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mask Lord's Boots");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(74, 167, 47);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.rare = 9;
            item.vanity = true;
        }
    }
}