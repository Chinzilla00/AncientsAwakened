using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Body)]
	public class OceanShirt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ocean Chestplate");
			Tooltip.SetDefault(@"Increases maximum mana by 20
5% increased magic damage");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = ItemRarityID.Orange;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.statManaMax2 += 20;
            player.magicDamage += 0.05f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}