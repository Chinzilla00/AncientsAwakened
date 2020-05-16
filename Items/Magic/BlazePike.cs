using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class BlazePike : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blaze Pike");
			Tooltip.SetDefault("Very hot to touch");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.magic = true;
			item.mana = 3;
			item.width = 56;
			item.height = 56;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileID.DD2FlameBurstTowerT1Shot;
			item.shootSpeed = 6f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "IncineriteBar", 10);
			recipe.AddIngredient(null, "BroodScale", 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}