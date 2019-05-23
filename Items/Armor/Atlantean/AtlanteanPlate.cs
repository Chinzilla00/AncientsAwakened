using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Body)]
	public class AtlanteanPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Chestplate");
			Tooltip.SetDefault(@"Increases magic damage by 15%
It vibrates with the powers of Atlantis");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = 4;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.magicDamage += 0.15f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanShirt"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}