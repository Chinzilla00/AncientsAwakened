using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.Items.Magic
{
	public class NovaFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Nova Flare");
		Tooltip.SetDefault("Shoots homing flares from the sky"
		+"\nLunar Flare EX");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(3570);
			item.useTime = 15;
			item.useAnimation = 15;
			item.damage = 175;
			item.mana = 15;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = item.shootSpeed;
			float num82 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num83 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float num84 = (float)Math.Sqrt((double)(num82 * num82 + num83 * num83));
			float num85 = num84;
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = (float)player.direction;
				num83 = 0f;
				num84 = 11f;
			}
			else
			{
				num84 = 11f / num84;
			}
			num82 *= num84;
			num83 *= num84;
			int num117 = 6;
			for (int num118 = 0; num118 < num117; num118++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-350, 351);
				vector2.Y -= (float)(100 * num118);
				num82 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num83 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				float ai2 = num83 + vector2.Y;
				if (num83 < 0f)
				{
					num83 *= -1f;
				}
				if (num83 < 20f)
				{
					num83 = 20f;
				}
				num84 = (float)Math.Sqrt((double)(num82 * num82 + num83 * num83));
				num84 = num75 / num84;
				num82 *= num84;
				num83 *= num84;
				Vector2 vector11 = new Vector2(num82, num83) / 2f;
				int p = Projectile.NewProjectile(vector2.X, vector2.Y, vector11.X*1.5f, vector11.Y*1.5f, 616, damage, knockBack, player.whoAmI);
				Main.projectile[p].usesLocalNPCImmunity = true;
				Main.projectile[p].localNPCHitCooldown = 1;
				Main.projectile[p].tileCollide = false;
				Main.projectile[p].timeLeft -= 60;
				Main.projectile[p].ranged = false;
				Main.projectile[p].magic = true;
			}
            return false;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(3570);
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}