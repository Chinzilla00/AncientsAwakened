using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Volley : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.magic = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 4;
            item.mana = 10;
            item.UseSound = SoundID.Item21;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Volley");
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volley");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            Vector2 value12 = Vector2.Normalize(new Vector2(num81, num82)) * 40f * item.scale;
            if (Collision.CanHit(vector2, 0, 0, vector2 + value12, 0, 0))
            {
                vector2 += value12;
            }
            Vector2 vector16 = new Vector2(num81, num82);
            vector16 *= 0.8f;
            Vector2 value13 = vector16.SafeNormalize(-Vector2.UnitY);
            float num202 = 0.0174532924f * (float)(-(float)player.direction);
            for (int num203 = 0; num203 <= 1; num203++)
            {
                Projectile.NewProjectile(vector2, (vector16 + value13 * (float)num203 * 1f).RotatedBy((double)((float)num203 * num202), default(Vector2)), type, damage, knockBack, item.owner, 0f, 0f);
            }
            return false;
        }
	}
}
