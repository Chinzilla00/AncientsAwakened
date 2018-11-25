using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    public class Shadow_Orb : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 6, 0, 0);
			item.rare = 8;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//here
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Orb");
			Tooltip.SetDefault("");
		}
	}
}
