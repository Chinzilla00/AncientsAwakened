using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class FancyTruffle : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fancy Truffle");
			Tooltip.SetDefault("Attracts a royal creature which flourishes in water & combat");
        }    
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ScalyTruffle);
			item.width = 32;
			item.height = 30;
			item.value = 500000;
			item.rare = ItemRarityID.Purple;
			item.mountType = mod.MountType("PrinceFishron");
		}



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
