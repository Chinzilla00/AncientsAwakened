using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SerpentSting : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Serpent's sting");
			Tooltip.SetDefault("Turns bullets into snow shots");
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.ranged = true;
			item.width = 52;
			item.height = 24;
			item.useAnimation = 3;
			item.useTime = 18;
			item.useStyle = 18;
			item.noMelee = true;
			item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
			item.UseSound = SoundID.Item40;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType<Projectiles.Serpent.Sting>();
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			
			item.crit = 3;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 4);
		}
	}
}
