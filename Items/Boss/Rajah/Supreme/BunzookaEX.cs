using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah
{

    public class Bunzooka : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunzooka");
            Tooltip.SetDefault("Fires Rabbit Rockets");
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7.5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("RabbitRocket3");
            item.useAmmo = AmmoID.Rocket;
            item.rare = 8;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -6);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("RabbitRocket3"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}