using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueRaider
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueRaiderBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asgardian Greaves");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 7;
			item.defense = 26;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RaiderLegs"));
			recipe.AddIngredient(null, "IceCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}