using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class SludgeShot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sludge Shot");
			Tooltip.SetDefault("Eew! It's mossy!");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 24;
			item.magic = true;
			item.mana = 4;
			item.width = 45;
			item.height = 45;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SludgeShotP");
			item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "AbyssiumBar", 10);
			recipe.AddIngredient(null, "MirePod", 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}