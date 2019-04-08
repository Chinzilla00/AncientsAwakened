using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueBlazingSuneate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Blazing Suneate");
            Tooltip.SetDefault(@"Increases movement speed by 25%
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 50000;
			item.rare = 7;
			item.defense = 18;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.25f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("BlazingSuneate"));
			recipe.AddIngredient(null, "InfernoCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}