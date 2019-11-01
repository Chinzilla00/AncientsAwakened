using Terraria;

namespace AAMod.Items.Materials
{
    public class VikingRelic : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Relic");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 34;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 0, 8, 0);
			item.rare = 1;
		}
	}
}
