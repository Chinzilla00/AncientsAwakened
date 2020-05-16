using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class Festivity : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Independence");
			Tooltip.SetDefault("Shoots 3 firework rockets"
			+"\nCelebration EX");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.FireworksLauncher);
			item.damage = 375;
         

        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FireworksLauncher);
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, -6);
		}
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num83 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
			}
			float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = player.direction;
				num83 = 0f;
				num84 = 11f;
			}
			else
			{
				num84 = 11f / num84;
			}
			num82 *= num84;
			num83 *= num84;
			for (int num212 = 0; num212 < 3; num212++)
			{
				float num213 = num82;
				float num214 = num83;
				num213 += Main.rand.Next(-40, 41) * 0.05f;
				num214 += Main.rand.Next(-40, 41) * 0.05f;
				Vector2 vector29 = vector2 + Vector2.Normalize(new Vector2(num213, num214).RotatedBy(-1.57079637f * player.direction)) * 6f;
				Projectile.NewProjectile(vector29.X, vector29.Y, num213*1.5f, num214*1.5f, 167 + Main.rand.Next(4), damage, knockBack, player.whoAmI, 0f, 1f);
			}
            return false;
        }
    }
}
