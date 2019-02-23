using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Melee
{
	public class RustyCutlass : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Cutlass");
			Tooltip.SetDefault("Even being rusty, it's still hard 'n sharp");
		}
		
		public override void SetDefaults()
		{
			item.damage = 21;
			item.melee = true;
			item.width = 34;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 20000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;  
		}
	}
}