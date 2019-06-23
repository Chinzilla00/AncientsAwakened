using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueAtlantean
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueAtlanteanPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Triton Chestplate");
			Tooltip.SetDefault(@"Increases magic damage by 20%
It vibrates with the powers of Atlantis");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 5, 0, 0);
			item.rare = 7;
			item.defense = 18;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.magicDamage += 0.20f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AtlanteanPlate"));
			recipe.AddIngredient(null, "OceanCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}