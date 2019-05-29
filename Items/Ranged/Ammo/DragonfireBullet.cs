using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class DragonfireBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dragonfire Bullet");
		}

		public override void SetDefaults()
		{
			item.shootSpeed = 5f;
			item.shoot = mod.ProjectileType("DragonfireBullet");
			item.damage = 13;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
			item.knockBack = 2f;
			item.value = 15;
            item.rare = 4;
            item.ranged = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddIngredient(mod.ItemType("DragonFire"), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
