using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Toad
{
    public class Todegun : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Frog Lob");
        }

		public override void SetDefaults()
		{
			item.damage = 29;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.reuseDelay = 10;
            item.shootSpeed = 14f;
            item.knockBack = 3f;
            item.width = 16;
            item.height = 16;
            item.damage = 25;
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
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            Vector2 value12 = Vector2.Normalize(new Vector2(num81, num82)) * 40f * item.scale;
            int num74 = item.shoot;
            if (Collision.CanHit(vector2, 0, 0, vector2 + value12, 0, 0))
            {
                vector2 += value12;
            }
            Vector2 vector16 = new Vector2(num81, num82);
            vector16 *= 0.8f;
            Vector2 value13 = vector16.SafeNormalize(-Vector2.UnitY);
            float num202 = 0.0174532924f * (float)(-(float)player.direction);
            for (int num203 = 0; num203 <= 2; num203++)
            {
                Projectile.NewProjectile(vector2, (vector16 + value13 * (float)num203 * 1f).RotatedBy((double)((float)num203 * num202), default(Vector2)), num74,damage, knockBack, Main.myPlayer, 0f, 0f);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
