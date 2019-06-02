using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class HK_MP5 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("HK MP5");
			Tooltip.SetDefault("Turns bullets into explosive bullets!");
		}

		public override void SetDefaults()
		{
			item.damage = 9;
			item.ranged = true;
			item.width = 52;
			item.height = 24;
			item.useAnimation = 8;
			item.useTime = 8;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item40;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            type = ProjectileID.ExplosiveBullet;
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(04));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 2);
		}
	}
}
