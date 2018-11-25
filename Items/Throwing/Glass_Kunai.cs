using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Glass_Kunai : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 4;
			item.thrown = true;
			item.width = 14;
			item.height = 34;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 12;
			item.useAnimation = 12;
			item.shoot = mod.ProjectileType("Glass_Kunai_Pro");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = Item.sellPrice(0, 0, 0, 0);
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass Kunai");
			Tooltip.SetDefault("Lots of piercing.");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass);
			recipe.SetResult(this, 75);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
		}
	}
}
