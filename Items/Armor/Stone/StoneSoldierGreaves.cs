using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Stone
{
    [AutoloadEquip(EquipType.Legs)]
	public class StoneSoldierGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Soldier Greaves");
			Tooltip.SetDefault(@"Increases mining speed by 15%");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 8;
			item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.pickSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningPants);
            recipe.AddIngredient(null, "StoneShell", 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}