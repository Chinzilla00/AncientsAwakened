using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{

    public class BunzookaEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RPG");
            Tooltip.SetDefault(@"`Rabbit Propelled Grenade`
Fires Rabbit Rockets
Bunnyzooka EX");
        }

        public override void SetDefaults()
        {
            item.damage = 550;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7.5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("RabbitRocketEX");
            item.useAmmo = AmmoID.Rocket;
            item.rare = 9;
            item.expert = true; item.expertOnly = true;
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
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("RabbitRocketEX"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}