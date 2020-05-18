using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Magic
{
    public class NovaFlare : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Nova Flare");
		Tooltip.SetDefault("Shoots homing flares from the sky"
		+"\nLunar Flare EX");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LunarFlareBook);
			item.useTime = 8;
			item.useAnimation = 8;
			item.damage = 175;
			item.mana = 15;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 vector2;
			float num75 = item.shootSpeed;
			float num84;
			int num117 = 6;
			for (int num118 = 0; num118 < num117; num118++)
			{
				vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-350, 351);
				vector2.Y -= 100 * num118;
				float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (num83 < 0f)
				{
					num83 *= -1f;
				}
				if (num83 < 20f)
				{
					num83 = 20f;
				}
				num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
				num84 = num75 / num84;
				num82 *= num84;
				num83 *= num84;
				Vector2 vector11 = new Vector2(num82, num83) / 2f;
				int p = Projectile.NewProjectile(vector2.X, vector2.Y, vector11.X*1.5f, vector11.Y*1.5f, ProjectileID.VortexBeaterRocket, damage, knockBack, player.whoAmI);
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
			recipe.AddIngredient(ItemID.LunarFlareBook);
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}