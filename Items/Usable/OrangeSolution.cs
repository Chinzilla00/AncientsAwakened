using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class OrangeSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orange Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Inferno");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("OrangeSolution") - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.width = 10;
			item.height = 12;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.rare = 3;
			item.maxStack = 999;
			item.consumable = true;
		}
	}
}
