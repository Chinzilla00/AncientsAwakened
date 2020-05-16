using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Moon

{
    [AutoloadEquip(EquipType.Body)]
    public class MoonRobe : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Robe");
            Tooltip.SetDefault(@"The Robe of a legendary lunar mage
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}