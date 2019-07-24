using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DragonsMaw : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 40;
            item.noMelee = true;
            item.ranged = true;
            item.width = 42;
            item.height = 60;

            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.rare = 5;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 25f;
            item.value = Item.sellPrice(0, 1, 0, 0);

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragons Maw");
            Tooltip.SetDefault("Transforms arrows into Dragon Arrows");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float rotation = MathHelper.ToRadians(5);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < 2; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("DragonLaser"), damage, knockBack, player.whoAmI);
			}
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonArrow"), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonSpirit", 25);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
