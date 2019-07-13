using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Items.Boss.Rajah.Supreme
{

    public class RabbitsWrath : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabbit's Wrath");
            Tooltip.SetDefault("Drops razor sharp carrots on your foes");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.magic = true;
            item.mana = 5;
            item.width = 32;
            item.height = 32;
            item.useTime = 5;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = .5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.autoReuse = true;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("CarrotEX");
            item.rare = 9;
            AARarity = 14;
        }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, -2);
		}
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector12 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float num75 = item.shootSpeed;
            for (int num120 = 0; num120 < 3; num120++)
            {
                Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                vector2.Y -= 100 * num120;
                Vector2 vector13 = vector12 - vector2;
                if (vector13.Y < 0f)
                {
                    vector13.Y *= -1f;
                }
                if (vector13.Y < 20f)
                {
                    vector13.Y = 20f;
                }
                vector13.Normalize();
                vector13 *= num75;
                float num82 = vector13.X;
                float num83 = vector13.Y;
                float speedX5 = num82;
                float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                int p = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("CarrotEX"), damage * 3 / 2, knockBack, Main.myPlayer);
                Main.projectile[p].melee = false;
                Main.projectile[p].magic = true;
                Main.projectile[p].extraUpdates = 1;
                Main.projectile[p].usesLocalNPCImmunity = true;
                Main.projectile[p].localNPCHitCooldown = 10;
            }
            return false;
        }
    }
}