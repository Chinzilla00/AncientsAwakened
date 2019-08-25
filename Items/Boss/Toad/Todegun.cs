using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Toad
{
    public class Todegun : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Frog Lob");
        }

		public override void SetDefaults()
		{
			item.damage = 59;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.reuseDelay = 10;
            item.shootSpeed = 8f;
            item.knockBack = 3f;
            item.width = 16;
            item.height = 16;
            item.damage = 15;
            item.UseSound = SoundID.DD2_BetsysWrathShot;
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 70, 0);
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ToadShot");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
