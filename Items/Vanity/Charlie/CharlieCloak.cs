using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Charlie

{
    [AutoloadEquip(EquipType.Body)]
    public class CharlieCloak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Wraith Cloak");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(60, 12, 98);
                }
            }
        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 11;
            item.vanity = true;
        }
    }
}