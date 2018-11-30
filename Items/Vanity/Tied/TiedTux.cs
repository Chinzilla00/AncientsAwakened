using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Tied
{
    [AutoloadEquip(EquipType.Body)]
    public class TiedTux : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tied's Fancy Tux");
            Tooltip.SetDefault(
@"This tux was woven with pure spook
'Great for impersonating Ancients Awakened Devs!'");
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 8, 251);
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