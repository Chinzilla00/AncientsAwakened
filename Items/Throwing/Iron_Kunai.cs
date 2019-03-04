using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Iron_Kunai : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 9;
			item.ranged = true;
			item.width = 14;
			item.height = 34;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 14;
			item.useAnimation = 14;
			item.shoot = mod.ProjectileType("Iron_Kunai_Pro");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 0, 4);
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Kunai");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar);
			recipe.SetResult(this, 75);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
