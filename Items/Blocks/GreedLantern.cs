using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class GreedLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Lantern");
		}


		public override void SetDefaults()
		{
            item.width = 64;
			item.height = 34;
            item.value = 150;
            item.maxStack = 99;
            item.useStyle = 1;
			item.useTime = 10;
            item.useAnimation = 15;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
			item.createTile = mod.TileType("GreedLantern");
		}
	}
}