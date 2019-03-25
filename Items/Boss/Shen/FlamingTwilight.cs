using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Boss.Shen
{
	public class FlamingTwilight : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 125;
			item.ranged = true;
			item.width = 76;
			item.height = 36;
			item.useTime = 5;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.UseSound = SoundID.Item34;
			item.value = 1000000;
			item.rare = 11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("FlamingTwilightP");
			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Gel;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Twilight");
			Tooltip.SetDefault("Releases a wide arc of blue and red flames"
			+"\nConsumes gel as ammo"
			+"\n33% chance not to consume gel");
        }

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 4;
			float rotation = MathHelper.ToRadians(8);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			switch (Main.rand.Next(2))
			{
				case 0:
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FlamingTwilightP"), damage*4, knockBack, player.whoAmI);
				}
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FlamingTwilightPD"), damage*4, knockBack, player.whoAmI);
				}
				break;
				case 1:
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FlamingTwilightP2"), damage*4, knockBack, player.whoAmI);
				}
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FlamingTwilightPD"), damage*4, knockBack, player.whoAmI);
				}
				break;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Discordium"), 5);
            recipe.AddIngredient(mod.ItemType("ChaosScale"), 5);
            recipe.AddIngredient(mod.ItemType("Dawnstrike"));
            recipe.AddIngredient(mod.ItemType("Darksprayer"));
            recipe.AddTile(mod.TileType("ASC"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
