using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Throwing
{
	public class Galaxy_Fan : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galaxy Knives");
		}
		
		public override void SetDefaults()
		{
			item.rare = 10;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.useStyle = 1;
			item.damage = 105;
			item.useAnimation = 22;
			item.useTime = 22;
			item.width = 26;
			item.height = 26;
			item.shoot = mod.ProjectileType("Galaxy_Knife");
			item.shootSpeed = 18f;
			item.knockBack = 2.5f;
			item.thrown = true;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.autoReuse = true;
			item.crit = 20;
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
			int numberProjectiles = 3 + Main.rand.Next(3);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(16)); // 16 degree spread.
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Throwing/Galaxy_Fan_Glowmask");
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