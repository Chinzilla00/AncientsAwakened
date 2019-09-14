using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class CovetiteCoin : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Covetite Coin");
		}

		public override void SetDefaults()
        {
            item.width = 10;
            item.height = 14;
            item.maxStack = 999;
            item.value = 50000;
            item.rare = 8;
            item.ammo = AmmoID.Coin;
            item.notAmmo = true;
            item.damage = 60;
            item.shoot = ProjectileID.GoldCoin;
            item.shootSpeed = 3f;
            item.ranged = true;
            item.consumable = true;
        }
	}
}
