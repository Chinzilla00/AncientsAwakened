using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Legs)]
	public class RadiumCuisses : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Cuisses");
			Tooltip.SetDefault(@"30% increased movement speed
Shines with the light of a starry night sky");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.3f;
            player.AddBuff(BuffID.Shine, 2);
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 27);
            recipe.AddIngredient(null, "Stardust", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}