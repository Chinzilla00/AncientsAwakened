using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SerpentSting : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Serpent's Sting");
			Tooltip.SetDefault("Turns bullets into snow shots");
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.ranged = true;
			item.width = 52;
			item.height = 24;
			item.useAnimation = 18;
			item.useTime = 18;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item40;
			item.autoReuse = true;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;
            item.crit = 3;
		}


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.Serpent.Sting>(), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 4);
		}
	}
}
