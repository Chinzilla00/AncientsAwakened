using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Melee
{
    public class SwimmingHydra : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swimming Hydra");
		}

		public override void SetDefaults()
		{
			item.damage = 42;
			item.melee = true;
			item.width = 36;
			item.height = 40;
			item.useTime = 27;
			item.useAnimation = 27;  
			item.useStyle = 1;
            item.knockBack = 2;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
            item.shootSpeed = 10;
            item.shoot = mod.ProjectileType<Projectiles.HydraSlash>();
		}

        int shoot = 0;

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            shoot++;
            if (shoot % 3 != 0) return false;

            shoot = 0;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), 180);
		}
	}
}
