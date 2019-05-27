using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class HydratoxinDart : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hydratoxin Dart");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("HydratoxinDart");
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.ammo = AmmoID.Dart;
			item.damage = 7;
			item.knockBack = 3f;
			item.shootSpeed = 4f;
			item.ranged = true;
			item.rare = 4;
			item.consumable = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("HydraToxin"), 1);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
