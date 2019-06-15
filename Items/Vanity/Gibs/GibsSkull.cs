using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Gibs
{
    [AutoloadEquip(EquipType.Head)]
	public class GibsSkull : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Revenant Skull");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 10;
            item.vanity = true;
        }
    }
}