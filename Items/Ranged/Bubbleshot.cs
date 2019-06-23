using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Bubbleshot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bubbleshot");
        }

		public override void SetDefaults()
		{
			item.damage = 29;
			item.ranged = true;
			item.width = 42;
			item.height = 20;
			item.useTime = 5;
			item.useAnimation = 13;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = 50000;
			item.rare = 5;
			item.UseSound = SoundID.Item85;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Bubble");
			item.shootSpeed = 4f;
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

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
