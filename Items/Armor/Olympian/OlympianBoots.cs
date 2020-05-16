using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Olympian
{
    [AutoloadEquip(EquipType.Legs)]
	public class OlympianBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Olympian Greaves");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.LightPurple;
			item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GladiatorLeggings);
            recipe.AddIngredient(null, "GoddessFeather", 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}