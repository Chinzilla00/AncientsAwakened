using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class MizuArashi : ModItem
    {

        public override void SetDefaults()
        {

            item.damage = 105;
            item.noMelee = true;
            item.ranged = true;
            item.width = 52;
            item.height = 20;
            item.useTime = 4;
            item.reuseDelay = 15;
            item.useStyle = 5;
            item.shoot = 10;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 12f;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mizu Arashi");
            Tooltip.SetDefault(@"Has a 1/15 chance to shoot a Mizu spirit
Spirits deal 2x damage, pierce up to 10 enemies and go through tiles
77% not to consume arrows");
        }
		
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .77f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (Main.rand.NextBool(15))
			{
                Projectile.NewProjectile(position.X, position.Y, speedX * 1.5f, speedY * 1.5f, mod.ProjectileType("Mizu"), damage * 2, knockBack, player.whoAmI);
            }
			else
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
