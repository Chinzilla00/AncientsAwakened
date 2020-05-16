using Terraria;
using Terraria.ID;

namespace AAMod.Items.Ranged.Ammo
{
    public class M79Round : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.ranged = true;
			item.damage = 25;
			item.width = 8;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 20, 0);
			item.rare = ItemRarityID.Orange;
			item.consumable = true;
			item.shoot = mod.ProjectileType("M79P");
			item.ammo = item.type;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("M79 Round");
		}
    }
}
