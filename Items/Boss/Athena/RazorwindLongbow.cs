using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class RazorwindLongbow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razorwind Longbow");
            Tooltip.SetDefault("Replaces wooden arrows with wind arrows with high knockback");
        }

        public override void SetDefaults()
        {
            item.damage = 50; 
            item.noMelee = true;
            item.ranged = true;
            item.width = 24;
            item.height = 62;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shootSpeed = 14f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.Athena.WindArrow>(), damage, knockBack * 3, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBow, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBow, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}