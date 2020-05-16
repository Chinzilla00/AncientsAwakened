using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Stone
{
    [AutoloadEquip(EquipType.Body)]
	public class StoneSoldierPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Soldier Breastplate");
			Tooltip.SetDefault(@"Increases mining speed by 15%");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.defense = 16;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.pickSpeed -= 0.15f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}