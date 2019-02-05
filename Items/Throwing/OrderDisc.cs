using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class OrderDisc : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LightDisc);
			item.melee = false;
			item.thrown = true;
			item.shootSpeed = 16f;
			item.stack = 1;
			item.useTime = 12;
			item.damage = 75;                            
			item.value = 20;
			item.rare = 5;
			item.knockBack = 4;
			item.useStyle = 1;
			item.useAnimation = 12;
			item.shoot = mod.ProjectileType("OrderDiscP");
			item.width = 46;
			item.height = 46;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Order Disc");
			Tooltip.SetDefault("Ignores enemy defense");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OrderBar"), 15);
			recipe.AddIngredient(ItemID.Ectoplasm, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
