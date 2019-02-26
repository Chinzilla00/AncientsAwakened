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

            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 19;
            item.useTime = 19;
            item.width = 24;
            item.height = 28;
            item.shoot = 20;
            item.UseSound = SoundID.Item12;
            item.knockBack = 0.75f;
            item.damage = 22;
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.scale = 0.8f;
            item.rare = 1;
            item.ranged = true;
            item.value = 20000;
            item.shoot = mod.ProjectileType<Projectiles.Darkray>();
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
