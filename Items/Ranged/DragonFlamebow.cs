using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DragonFlamebow : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 14;
            item.noMelee = true;
            item.ranged = true;
            item.width = 30;
            item.height = 60;
            item.scale *= .8f;
            item.useTime = 21;
            item.useAnimation = 21;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<Projectiles.DragonArrow>();
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 25f;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Flamebow");
            Tooltip.SetDefault("Transforms arrows into Dragon Arrows");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType<Projectiles.DragonArrow>(), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 8);
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
