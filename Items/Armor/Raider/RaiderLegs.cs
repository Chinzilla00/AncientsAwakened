using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Legs)]
	public class RaiderLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raider Greaves");
            Tooltip.SetDefault(@"Increases melee critical strike chance by 10%
Reduces movement speed by 10%");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 4;
			item.defense = 8;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 10;
			player.moveSpeed -= 0.1f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VikingBoots"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}