using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Boss.Shen
{
	public class DraconicRipper : ModItem
	{
		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 3;
			item.useTime = 3;
			item.width = 72;
			item.height = 34;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.damage = 60;
			item.shootSpeed = 15f;
			item.noMelee = true;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 11;
			item.knockBack = 3f;
			item.ranged = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconic Ripper");
			Tooltip.SetDefault("Shoots dozens of high-caliber bullets"
			+"\nIgnores enemy defense"
			+"\n77% chance not to consume ammo");
        }

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .77;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = player.MountedCenter;
			int numberProjectiles = 4 + Main.rand.Next(3); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
				float scale = 1f - (Main.rand.NextFloat() * .4f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(vector.X, vector.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		
		public override void HoldItem(Player player)
		{
			player.armorPenetration += 500;
		}
		
		public override void AddRecipes()
        {
            return;
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Discordium"), 5);
            recipe.AddIngredient(mod.ItemType("ChaosScale"), 5);
			recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddTile(mod.TileType("ASC"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
