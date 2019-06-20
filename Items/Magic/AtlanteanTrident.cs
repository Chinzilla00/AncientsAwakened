using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class AtlanteanTrident : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Trident");
			Tooltip.SetDefault("Fires off magical tridents");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 38;
			item.magic = true;
			item.mana = 8;
			item.width = 68;
			item.height = 68;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 500000;
			item.rare = 3;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AtlanteanTridentP");
			item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX*4, speedY*4).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage/2, knockBack, player.whoAmI);
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanTrident"));
			recipe.AddIngredient(mod.ItemType("BlazePike"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanTrident"));
			recipe.AddIngredient(mod.ItemType("SludgeShot"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}