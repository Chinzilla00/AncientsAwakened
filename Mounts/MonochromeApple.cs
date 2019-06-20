using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items;

namespace AAMod.Mounts
{
	public class MonochromeApple : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Monochromatic Apple");
			Tooltip.SetDefault("Will attract an equestrian friend");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 300000;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("BegPony");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 130, 150);
                }
            }
        }
    }
}