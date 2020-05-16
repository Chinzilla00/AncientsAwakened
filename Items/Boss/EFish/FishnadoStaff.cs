using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class FishnadoStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishnado Staff");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.TempestStaff);
			item.damage = 150;
			item.rare = ItemRarityID.Purple;
			item.shoot = mod.ProjectileType("Fishnado");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TempestStaff);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}