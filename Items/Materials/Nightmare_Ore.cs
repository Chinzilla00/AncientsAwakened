using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Nightmare_Ore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Ore");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 7, 0);
			item.rare = 4;
		}
	}
}
