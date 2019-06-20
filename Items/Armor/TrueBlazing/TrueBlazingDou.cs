using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueBlazingDou : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Perfect Blazing Dao");
			Tooltip.SetDefault(@"5% increased damage resistance");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 60000;
			item.rare = 4;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance += 0.05f;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BlazingDou"));
            recipe.AddIngredient(mod.ItemType("InfernoCrystal"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}