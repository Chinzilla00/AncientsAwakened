using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class EventideBullet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eventide Bullet");
		}

		public override void SetDefaults()
		{
			item.shootSpeed = 5f;
			item.shoot = ModContent.ProjectileType<Projectiles.Ammo.EventideBullet>();
			item.damage = 25;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
			item.knockBack = 2f;
			item.value = 15;
            item.rare = ItemRarityID.LightRed;
            item.ranged = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ExplodingBullet, 500);
			recipe.AddIngredient(mod.ItemType("EventideAbyssium"), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 500);
			recipe.AddRecipe();
		}
	}
}
