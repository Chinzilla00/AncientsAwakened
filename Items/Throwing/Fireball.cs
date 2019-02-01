using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Fireball : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 20;
			item.thrown = true;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("FireballP");
			item.shootSpeed = 12f;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.crit = 10;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
			Tooltip.SetDefault("Even better than Mario's Fire Flower!");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar);
			recipe.SetResult(this, 99);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		}
	}
}
