using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged
{
    public class DoomGun : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 22;
            item.useTime = 22;
            item.width = 24;
            item.height = 28;
            item.UseSound = SoundID.Item12;
            item.knockBack = 0.75f;
            item.damage = 20;
            item.shootSpeed = 25f;
            item.noMelee = true;
            item.scale = 0.8f;
            item.rare = 1;
            item.ranged = true;
            item.value = 2000;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Darkray>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Pistol");
        }

		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
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
