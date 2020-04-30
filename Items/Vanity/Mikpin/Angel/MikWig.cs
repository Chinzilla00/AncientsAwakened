using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Mikpin.Angel
{
    [AutoloadEquip(EquipType.Head)]
    public class MikWig : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Angelic Operator's Halo");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Testers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(188, 62, 62);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.vanity = true;
        }
    }
}