using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Throwing
{
	public class Galaxy_Javelin : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 164;
			item.thrown = true;
			item.width = 22;
			item.noUseGraphic = true;
			item.maxStack = 1;
			item.consumable = false;
			item.height = 44;
			item.useTime = 11;
			item.useAnimation = 11;
			item.shoot = mod.ProjectileType("Galaxy_Javelin_Pro");
			item.shootSpeed = 19f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 3;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galactic Javelin");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Galaxy_Fragment", 18);
			recipe.SetResult(this);
			recipe.AddTile(null, "Soul_Forge_Placed");
			recipe.AddRecipe();
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(02));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Throwing/Galaxy_Javelin_Glowmask");
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
		}
	}
}
