using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class ForsakenStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Forsaken Staff");
			Tooltip.SetDefault("Shoots 2 homing blasts of forsaken energy which explode into forsaken sparks");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 97;
			item.magic = true;
			item.mana = 10;
			item.width = 76;
			item.height = 76;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ForsakenStaffBlast");
			item.shootSpeed = 16f;
			item.rare = 11;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
            return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DesertStaff>(), 1);
			recipe.AddIngredient(null, "SoulFragment", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}