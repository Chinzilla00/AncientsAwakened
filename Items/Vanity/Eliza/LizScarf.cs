using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Eliza
{
    [AutoloadEquip(EquipType.Neck)]
	public class LizScarf : ModItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midnight Scarf");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
		}



        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }


        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = 11;
            item.vanity = true;
            item.accessory = true;
        }
    }
}