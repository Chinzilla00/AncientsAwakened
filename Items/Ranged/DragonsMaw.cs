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
            Tooltip.SetDefault("Transforms arrows into Flaming Arrows");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.FireArrow, damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false; //Makes sure to not fire the original projectile
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
