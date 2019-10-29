using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class NeithsString : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.damage = 35;
            item.noMelee = true;
            item.ranged = true;
            item.width = 42;
            item.height = 60;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = 10;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.rare = 6;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 25f;
            item.value = Item.buyPrice(0, 1, 0, 0);

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neith's String");
            Tooltip.SetDefault(@"Shoots 2 arrows at once
Can occasionally shoot ``Judgement arrow``, which lowers enemy defense
Converts wooden arrows into slower, but high-damaging mummy arrows");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(3);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<Projectiles.Anubis.MummyArrow>();
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * .7f, perturbedSpeed.Y * .7f, type, (int)(damage * 1.5f), knockBack, player.whoAmI);
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
			}
			if (Main.rand.NextBool(5))
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AnubisArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
