using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Olympian
{
    [AutoloadEquip(EquipType.Body)]
	public class OlympianPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Olympian Breastplate");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 5, 0, 0);
			item.rare = ItemRarityID.LightPurple;
			item.defense = 8;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GladiatorBreastplate);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}