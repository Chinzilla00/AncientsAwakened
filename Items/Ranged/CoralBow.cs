using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class CoralBow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Bow");
			Tooltip.SetDefault("Somehow better than a metallic bow");
        }

		public override void SetDefaults()
		{
			item.damage = 10;
			item.ranged = true;
			item.width = 28;
			item.height = 60;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.IronBow);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.LeadBow);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
