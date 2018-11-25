using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Plantera
{
    [AutoloadEquip(EquipType.Legs)]
	public class BeastLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beast Greaves");
			Tooltip.SetDefault("5% increased thrown critical strike chance"
				+ "\n+8% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 22;
            item.value = Item.buyPrice(0, 1, 50, 0);
            item.rare = 7;
			item.defense = 22; //64   16, 26, 22
        }

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 5;
			player.thrownVelocity += 0.8f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PlanteraPetal", 14); //24        10, 8, 6
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);//30      14, 10, 6
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}