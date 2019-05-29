using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class WhiteSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the snow biome");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType<Projectiles.SnowSolution>() - ProjectileID.PureSpray;
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
