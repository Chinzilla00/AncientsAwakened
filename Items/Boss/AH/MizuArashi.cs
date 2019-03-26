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
            item.useTime = 5;
            item.useAnimation = 5;
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
            Tooltip.SetDefault(@"Occasionally shoots Mizu spirit
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
				float numberProjectiles = 3;
				float rotation = MathHelper.ToRadians(5);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X*1.5f, perturbedSpeed.Y*1.5f, mod.ProjectileType("Mizu"), damage*2, knockBack, player.whoAmI);
				}
			}
			else
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
