using Terraria;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class M79Parts : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("M79 Parts");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Orange;
		}
	}
}
