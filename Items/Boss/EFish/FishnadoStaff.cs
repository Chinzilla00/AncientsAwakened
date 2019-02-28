using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Boss.EFish
{
	public class FishnadoStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishnado Staff");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(2621);
			item.damage = 150;
			item.rare = 11;
			item.shoot = mod.ProjectileType("Fishnado");
		}
	}
}