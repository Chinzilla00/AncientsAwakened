using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Maskano
{
    [AutoloadEquip(EquipType.Body)]
    public class MaskPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Mask Lord's Chestplate");
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
            item.width = 28;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}