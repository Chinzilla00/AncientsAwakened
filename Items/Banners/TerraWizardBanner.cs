
namespace AAMod.Items.Banners
{
	public class TerraWizardBanner : BaseAAItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			item.width = 10;
			item.height = 24;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = 1000;
			item.createTile = mod.TileType("Banners");
			item.placeStyle = 37;        //Place style means which frame(Horizontally, starting from 0) of the tile should be placed
		}
	}
}
