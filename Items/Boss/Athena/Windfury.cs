using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class Windfury : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windfury");
            Tooltip.SetDefault("Replaces wooden arrows with gale arrows with high knockback and infinite piercing");
        }

        public override void SetDefaults()
        {
            item.damage = 140; 
            item.noMelee = true;
            item.ranged = true;
            item.width = 26;
            item.height = 50;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedX, ModContent.ProjectileType<Projectiles.Athena.GaleArrow>(), damage, knockBack * 5, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }
    }
}