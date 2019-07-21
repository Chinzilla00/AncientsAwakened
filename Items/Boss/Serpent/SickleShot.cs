using Terraria;
using System;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SickleShot : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sickleshot");
            Tooltip.SetDefault("Shoots 2 waves of 2 ice arrows with high velocity in short succession");
        }

        public override void SetDefaults()
        {

            item.damage = 25;
            item.noMelee = true;
            item.ranged = true;
            item.width = 40;
            item.height = 62;
            item.useTime = 25;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<Projectiles.Serpent.IceArrow>();
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 8f;

        }

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 15f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 2; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		    }
		    return false;
		}
    }
}
