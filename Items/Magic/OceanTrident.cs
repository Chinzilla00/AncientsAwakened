using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class OceanTrident : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ocean Trident");
			Tooltip.SetDefault("Very slippery");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.magic = true;
			item.mana = 5;
			item.width = 56;
			item.height = 56;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shoot = ProjectileID.WaterBolt;
			item.shootSpeed = 7f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[proj].penetrate = 1;
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 12);
			recipe.AddIngredient(ItemID.Starfish);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddIngredient(ItemID.FallenStar, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}