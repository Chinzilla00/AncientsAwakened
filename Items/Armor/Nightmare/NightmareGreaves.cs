using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Nightmare
{
    [AutoloadEquip(EquipType.Legs)]
	public class NightmareGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Greaves");
			Tooltip.SetDefault("5% increased trown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 1, 26, 0);
			item.rare = 5;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownVelocity += 0.05f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Bar",6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}