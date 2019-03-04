using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class Dracorang : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LightDisc);
			item.melee = true;
			item.shootSpeed = 16f;
			item.stack = 1;
			item.useTime = 20;
			item.damage = 50;                            
			item.value = 20;
			item.rare = 4;
			item.knockBack = 4;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("DracorangP");
			item.width = 22;
			item.height = 32;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dracorang");
			Tooltip.SetDefault("Leaves short living flame trail");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RadiantIncinerite"), 15);
			recipe.AddIngredient(ItemID.LivingFireBlock, 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
