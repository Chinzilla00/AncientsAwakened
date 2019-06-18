using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Banners
{
	public class FatPixieBanner : ModItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			item.width = 36;
			item.height = 36;
			item.maxStack = 9999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.createTile = mod.TileType("FatPixieBanner");
			item.placeStyle = 0;
		}
	}
}