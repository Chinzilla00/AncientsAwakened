using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DoomGun : ModItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 44;
            item.noMelee = true;
            item.useAmmo = AmmoID.Bullet;
            item.ranged = true;
            item.width = 46;
            item.height = 22;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<Projectiles.Darkray>();
            item.knockBack = 0;
            item.value = 10;
            item.rare = 2;
            item.UseSound = SoundID.Item12;
            item.autoReuse = false;
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Pistol");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}
