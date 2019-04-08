using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Head)]
	public class OceanHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ocean Helmet");
            Tooltip.SetDefault(@"Allows to breath underwater");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 3;
            item.defense = 3;
        }
		
		public override void UpdateEquip(Player player)
		{
            player.gills = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}