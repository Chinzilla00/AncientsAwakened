using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Zero
{

    public class GenocideCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genocide Cannon");
            Tooltip.SetDefault("Fires highly explosive Rockets");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 10f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("GRocket");
            item.useAmmo = AmmoID.Rocket;
            item.rare = 9;
            AARarity = 13;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("GRocket"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}