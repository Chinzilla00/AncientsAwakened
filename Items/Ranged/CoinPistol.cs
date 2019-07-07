using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class CoinPistol : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coin Pistol");
			Tooltip.SetDefault("Coins do half of their normal damage");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 20;
			item.useTime = 20;
			item.width = 50;
			item.height = 18;
			item.shoot = 158;
			item.useAmmo = AmmoID.Coin;
			item.UseSound = SoundID.Item11;
			item.damage = 0;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.value = 20000;
			item.rare = 3;
			item.knockBack = 2f;
			item.ranged = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			damage /= 2;
			return true;
		}
	}
}
