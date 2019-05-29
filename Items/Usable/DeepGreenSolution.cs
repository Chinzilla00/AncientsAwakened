using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class DeepGreenSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deep Green Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nConverts the jungle into forest");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("ForestSolution") - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.width = 10;
			item.height = 12;
			item.value = Item.buyPrice(0, 0, 25, 0);
			item.rare = 3;
			item.maxStack = 999;
			item.consumable = true;
		}
	}
}
