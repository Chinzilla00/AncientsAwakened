using Terraria;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class vulture_feather : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vulture Feather");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 34;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 0, 8, 0);
			item.rare = ItemRarityID.White;
		}
	}
}
