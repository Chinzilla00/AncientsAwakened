using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Throwing
{
	public class Paladins_Knife : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 94;
			item.thrown = true;
			item.width = 22;
			item.noUseGraphic = true;
			item.height = 44;
			item.useTime = 13;
			item.useAnimation = 13;
			item.shoot = mod.ProjectileType("Paladins_Knife_Pro");
			item.shootSpeed = 16f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 3;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paladin's Knife");
			Tooltip.SetDefault("");
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(02));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}
	}
}
