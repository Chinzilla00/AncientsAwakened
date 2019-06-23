using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Tails

{
    [AutoloadEquip(EquipType.Body)]
    public class TailsBody : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tails Body Costume");
            Tooltip.SetDefault(@"A body suit made to resemble a certain two-tailed fox.
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 216, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 9;
            item.vanity = true;
        }
    }
}