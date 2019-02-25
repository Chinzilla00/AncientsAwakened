using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Mounts
{
	public class FancyTruffle : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fancy Truffle");
			Tooltip.SetDefault("Exquisite");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
            item.rare = 11;
			item.useStyle = 1;
			item.value = 300000;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("PrinceFishron");
		}
       
    }
}