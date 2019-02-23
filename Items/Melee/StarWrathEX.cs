using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class StarWrathEX : ModItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.StarWrath);
			item.autoReuse = true;
			item.rare = 10;
			item.width = 48;
			item.height = 56;
			item.scale = 1.2f;
			item.shootSpeed = 10f;
			item.knockBack = 7f;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.damage = 150;
			item.useTime = 12;
			item.useAnimation = 12;
        }

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Star Wrath EX");
		  Tooltip.SetDefault("Causes stars to rain from the sky\nStars can reach enemy through any obstacles");
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector12 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = item.shootSpeed;
			float num119 = vector12.Y;
			if (num119 > player.Center.Y - 200f)
			{
				num119 = player.Center.Y - 200f;
			}
			for (int num120 = 0; num120 < 3; num120++)
			{
				vector2 = player.Center + new Vector2((float)(-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				vector2.Y -= (float)(100 * num120);
				Vector2 vector13 = vector12 - vector2;
				if (vector13.Y < 0f)
				{
					vector13.Y *= -1f;
				}
				if (vector13.Y < 20f)
				{
					vector13.Y = 20f;
				}
				vector13.Normalize();
				vector13 *= num75;
				float num82 = vector13.X;
				float num83 = vector13.Y;
				float speedX5 = num82;
				float speedY6 = num83 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("StarWrathEXP"), damage*3/2, knockBack, Main.myPlayer);
			}
			return false;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(mod.ItemType("EXSoul"));
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
