using Terraria;
using System;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class RoyalScepterEX : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.mana = 5;
            item.damage = 260;
            item.useStyle = 5;
            item.shootSpeed = 9f;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Rajah.Supreme.CarrotEX>();
            item.width = 58;
            item.height = 57;
            item.UseSound = SoundID.Item39;
            item.useAnimation = 30;
            item.useTime = 10;
            item.autoReuse = true;
            item.rare = 8;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.magic = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Staff");
            Tooltip.SetDefault(@"Shoots a spread of five golden carrots
Royal Scepter EX");
            Item.staff[item.type] = true;
        }

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 45f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 10f;
		    double offsetAngle;
		    for (int i = 0; i < 5; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	int proj = Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
                Main.projectile[proj].ranged = false;
                Main.projectile[proj].magic = true;
            }
		    return false;
		}
    }
}
