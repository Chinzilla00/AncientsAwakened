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
            Tooltip.SetDefault(@"Increases magic critical strike chance by 15%");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 7;
			item.defense = 16;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 15;
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