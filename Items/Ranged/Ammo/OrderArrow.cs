using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class OrderArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Order Arrow");
			Tooltip.SetDefault("Ignores up to 30 of enemy defense");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 4f;
			item.value = 100;
			item.rare = 1;
			item.shoot = mod.ProjectileType("OrderArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 1f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 250);
			recipe.AddIngredient(mod.ItemType("OrderBar"), 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 250);
			recipe.AddRecipe();
		}
	}
}
