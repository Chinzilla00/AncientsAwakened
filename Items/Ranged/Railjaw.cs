using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Railjaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Railjaw");
        }

		public override void SetDefaults()
		{
			item.damage = 17;
			item.ranged = true;
			item.width = 42;
			item.height = 20;
			item.useTime = 19;
			item.useAnimation = 19;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 1;
			item.value = 1200;
			item.rare = 1;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 5f;
			item.useAmmo = AmmoID.Bullet;
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
