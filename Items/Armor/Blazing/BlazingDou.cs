using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Body)]
	public class BlazingDou : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Blazing Dao");
			Tooltip.SetDefault(@"2% increased damage resistance
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
            item.value = 90000;
            item.rare = 4;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance += 0.02f;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "KindledDou", 1);
            recipe.AddIngredient(null, "DoomiteUGreaves", 1);
            recipe.AddIngredient(ItemID.FossilShirt, 1);
            recipe.AddIngredient(null, "OceanBoots", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}