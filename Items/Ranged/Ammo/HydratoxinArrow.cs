using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class HydratoxinArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hydratoxin Arrow");
			Tooltip.SetDefault("Inflicts Hydratoxin debuff on hit");
		}

		public override void SetDefaults()
		{
			item.damage = 15;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 4f;
			item.value = 100;
			item.rare = ItemRarityID.LightRed;
			item.shoot = mod.ProjectileType("HydratoxinArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 1f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 150);
			recipe.AddIngredient(mod.ItemType("HydraToxin"), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 150);
			recipe.AddRecipe();
		}
	}
}
