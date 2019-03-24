using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class RadiumArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Radium Arrow");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 4f;
			item.value = 30;
			item.rare = 11;
			item.shoot = mod.ProjectileType("DragonArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 6f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "RadiumBar", 3);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 400);
			recipe.AddRecipe();
		}
	}
}
