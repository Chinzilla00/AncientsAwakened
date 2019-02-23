using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class HydratoxinBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hydratoxin Bullet");
		}

		public override void SetDefaults()
		{
			item.shootSpeed = 5f;
			item.shoot = mod.ProjectileType("HydratoxinBullet");
			item.damage = 12;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
			item.knockBack = 2f;
			item.value = 15;
			item.ranged = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddIngredient(mod.ItemType("HydraToxin"), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
