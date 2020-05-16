using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Items.Vanity.Tied
{
    [AutoloadEquip(EquipType.Legs)]
    public class TiedsLeggings : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Trousers");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 22;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 105, 0);
                }
            }
        }
    }
}