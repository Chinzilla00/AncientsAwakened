using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class GreedChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Greed Chest");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 1000;
			item.createTile = mod.TileType("GreedChest");
		}
    }
}