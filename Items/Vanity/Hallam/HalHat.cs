using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Hallam
{
    [AutoloadEquip(EquipType.Head)]
	public class HalHat : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hallam's Dapper Top Hat");
            Tooltip.SetDefault(
@"You can't help but feel fancy just wearing this
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
            item.width = 20;
            item.height = 14;
            item.rare = 9;
            item.vanity = true;
        }
	}
}