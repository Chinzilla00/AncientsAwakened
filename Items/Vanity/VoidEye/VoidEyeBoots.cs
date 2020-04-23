using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.VoidEye
{
    [AutoloadEquip(EquipType.Legs)]
	public class VoidEyeBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Eye's Boots");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(148, 18, 142);
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