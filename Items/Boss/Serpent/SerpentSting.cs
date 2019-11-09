using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SerpentSting : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Serpent's Sting");
			Tooltip.SetDefault("Turns bullets into snow shots");
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.ranged = true;
			item.width = 52;
			item.height = 24;
			item.useAnimation = 40;
			item.useTime = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item40;
			item.autoReuse = false;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;
            item.crit = 3;
		}


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Serpent.Sting>(), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 4);
		}
	}
}
