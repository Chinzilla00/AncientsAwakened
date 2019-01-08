using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Darkpuppy
{
    [AutoloadEquip(EquipType.Legs)]
	public class FlowerBoots : ModItem
	{
		public override void SetStaticDefaults()
       
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flowery Boots");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 246, 124);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 14;
            item.rare = 9;
            item.vanity = true;
        }
    }
}