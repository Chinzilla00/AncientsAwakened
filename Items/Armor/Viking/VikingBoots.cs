using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Legs)]
	public class VikingBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Greaves");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
			item.defense = 6;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"RelicBar", 14);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}