using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public abstract class FishronScale : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishron Scale");
            Tooltip.SetDefault("Smells like dead fish and sweet victory");
        }

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = 8;
		}
	}
}
