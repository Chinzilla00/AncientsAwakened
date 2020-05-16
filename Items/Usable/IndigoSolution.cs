using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class IndigoSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Indigo Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Mire");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("IndigoSolution") - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.width = 10;
			item.height = 12;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.rare = ItemRarityID.Orange;
			item.maxStack = 999;
			item.consumable = true;
		}
	}
}
