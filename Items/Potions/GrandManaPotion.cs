using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class GrandManaPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grand Mana Potion");
		}
		
		public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.healMana = 400;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 50;
            item.consumable = true;
            item.width = 14;
            item.height = 24;
            item.value = 50000;
            item.rare = ItemRarityID.Purple;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SuperManaPotion);
            recipe.AddRecipeGroup("AAMod:AncientMaterials");
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}