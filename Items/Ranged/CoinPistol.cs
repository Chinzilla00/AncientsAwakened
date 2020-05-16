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
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.autoReuse = true;
			item.useAnimation = 20;
			item.useTime = 20;
			item.width = 50;
			item.height = 18;
			item.shoot = ProjectileID.CopperCoin;
			item.useAmmo = AmmoID.Coin;
			item.UseSound = SoundID.Item11;
			item.damage = 0;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.value = 20000;
			item.rare = ItemRarityID.Orange;
			item.knockBack = 2f;
			item.ranged = true;
		}
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -1);
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			damage /= 2;
			return true;
		}
	}
}
