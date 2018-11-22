using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class CosmicCarbine : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Carbine");
			Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 54;
			item.height = 24;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 22f;
			item.useAmmo = mod.ItemType("Energy_Cell");
			item.crit = 5;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
		
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ModProjectile.Energy_Cell_Pro) // or ProjectileID.WoodenArrowFriendly
			{
				type = ModProjectile.CosmicLaser; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/
	}
}
