using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class HiveArtillery : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hive Artillery");
			Tooltip.SetDefault("Shoots dozens of evil african bees"
			+"\nBees ignore enemy invincibility frames"
			+"\nBee Gun EX");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.BeeGun);
			item.damage = 30;
			item.mana = 6;
			item.useAnimation = 5;
			item.useTime = 5;
			item.scale = 1f;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
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
			int num163 = Main.rand.Next(1, 3);
			if (Main.rand.Next(4) == 0)
			{
				num163++;
			}
			if (Main.rand.Next(4) == 0)
			{
				num163++;
			}
			if (player.strongBees && Main.rand.Next(2) == 0)
			{
				num163++;
			}
			for (int num164 = 0; num164 < num163; num164++)
			{
				float num165 = num82;
				float num166 = num83;
				num165 += (float)Main.rand.Next(-35, 36) * 0.02f;
				num166 += (float)Main.rand.Next(-35, 36) * 0.02f;
				int num167 = Projectile.NewProjectile(vector2.X, vector2.Y, num165, num166, player.beeType(), player.beeDamage(damage), player.beeKB(knockBack), player.whoAmI, 0f, 0f);
				Main.projectile[num167].magic = true;
				Main.projectile[num167].usesLocalNPCImmunity = true;
				Main.projectile[num167].localNPCHitCooldown = 1;
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeGun);
			recipe.AddIngredient(ItemID.ChainGun);
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
