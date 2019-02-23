using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class HydraFang : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 18;
			item.thrown = true;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 28;
			item.height = 34;
			item.useTime = 15;
			item.useAnimation = 15;
			item.shoot = mod.ProjectileType("HydraFangP");
			item.shootSpeed = 16f;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 3;
			item.UseSound = SoundID.Item19;
			item.autoReuse = true;
			item.crit = 10;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydra Fang");
			Tooltip.SetDefault("Pierces up to 3 enemies");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AbyssiumBar"));
			recipe.SetResult(this, 99);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
