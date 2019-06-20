using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Aves
{
    [AutoloadEquip(EquipType.Legs)]
	public class DJDuckPants : BaseAAItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("DJ Duck Pants");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
		}
        
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(158, 255, 61);
                }
            }
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = 9;
            item.vanity = true;
        }
    }
}