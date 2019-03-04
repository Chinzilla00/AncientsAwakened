using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Fang_Knife : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 26;
			item.ranged = true;
			item.width = 26;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.height = 42;
			item.useTime = 12;
			item.useAnimation = 12;
			item.shoot = mod.ProjectileType("Fang_Knife_Pro");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 0, 6);
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 3;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fang Knife");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpiderFang);
			recipe.SetResult(this, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
