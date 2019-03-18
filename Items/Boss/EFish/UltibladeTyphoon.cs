using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
	public class UltibladeTyphoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultiblade Typhoon");
			Tooltip.SetDefault(@"Casts 3 fast homing razorwheels
Razorblade Typhoon EX");
		}

		public override void SetDefaults()
		{
			item.mana = 16;
			item.damage = 150;
			item.useStyle = 5;
			item.shootSpeed = 6f;
			item.shoot = 409;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item84;
			item.useAnimation = 30;
			item.useTime = 15;
			item.autoReuse = true;
			item.rare = 9;
			item.noMelee = true;
			item.knockBack = 6f;
			item.scale = 0.9f;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.magic = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage*2, knockBack, player.whoAmI);
				Main.projectile[proj].penetrate = 10;
				Main.projectile[proj].usesLocalNPCImmunity = true;
				Main.projectile[proj].localNPCHitCooldown = 1;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(ItemID.RazorbladeTyphoon);
			recipe.AddIngredient(null, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}