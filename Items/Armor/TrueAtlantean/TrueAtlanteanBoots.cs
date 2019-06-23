using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueAtlantean
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueAtlanteanBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Triton Greaves");
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
			recipe.AddIngredient(mod.ItemType("AtlanteanGreaves"));
			recipe.AddIngredient(null, "OceanCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}