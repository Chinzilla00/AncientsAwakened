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
            item.damage = 60;
            item.noMelee = true;
            item.ranged = true;
            item.width = 42;
            item.height = 60;

            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.shoot = 10;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.rare = 11;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 25f;
            item.value = Item.buyPrice(0, 1, 0, 0);

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neith's String");
            Tooltip.SetDefault("Shoots 2 arrows at once\nCan occasionally shoot ``Judgement arrow``, which lowers enemy defense");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(3);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			if (Main.rand.NextBool(5))
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AnubisArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
