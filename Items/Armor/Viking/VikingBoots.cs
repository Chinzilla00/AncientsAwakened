using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Legs)]
	public class VikingBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Greaves");
            Tooltip.SetDefault(@"Increases melee critical strike chance by 7%
Reduces movement speed by 15%");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
			item.defense = 6;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 7;
			player.moveSpeed -= 0.15f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("RelicBar", 14);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}