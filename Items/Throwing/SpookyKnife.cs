using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class SpookyKnife : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 100;
			item.thrown = true;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 14;
			item.height = 38;
			item.useTime = 10;
			item.useAnimation = 10;
			item.shoot = mod.ProjectileType("SpookyKnife");
			item.shootSpeed = 14f;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.crit = 15;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spooky Knife");
			Tooltip.SetDefault("Spreads Mourning Wood Embers on hit");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpookyWood, 10);
			recipe.SetResult(this, 99);
			recipe.AddTile(TileID.Sawmill);
			recipe.AddRecipe();
		}
	}
}
