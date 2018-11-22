using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class Gold_Kunai : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 13;
			item.thrown = true;
			item.width = 14;
			item.height = 34;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 12;
			item.useAnimation = 12;
			item.shoot = mod.ProjectileType("Gold_Kunai_Pro");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 0, 16);
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Kunai");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar);
			recipe.SetResult(this, 75);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
