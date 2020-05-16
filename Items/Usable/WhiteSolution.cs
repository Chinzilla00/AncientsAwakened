using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class WhiteSolution : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the snow biome");
		}

		public override void SetDefaults()
		{
			item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.SnowSolution>() - ProjectileID.PureSpray;
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
