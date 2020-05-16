using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah
{
    public class RajahPelt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rabbit Pelt");
            Tooltip.SetDefault("Surpisingly durable for a pelt of fur");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
		}
	}
}
