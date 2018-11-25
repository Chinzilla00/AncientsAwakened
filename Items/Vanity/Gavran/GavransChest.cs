using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Gavran
{
    [AutoloadEquip(EquipType.Body)]
    public class GavransChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gavran's Swimming Vest");
            Tooltip.SetDefault(
@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(72, 232, 193);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = 9;
            item.vanity = true;
        }
    }
}