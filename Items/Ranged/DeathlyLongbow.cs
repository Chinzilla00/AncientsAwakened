using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DeathlyLongbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Longbow");
            Tooltip.SetDefault("Replaces Wooden Arrows with Bone Arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 42; //This is the amount of damage the item does
            item.noMelee = true; //This makes sure the bow doesn't do melee damage
            item.ranged = true; //This causes your bow to do ranged damage
            item.width = 22; //Hitbox width
            item.height = 64; //Hitbox height
            item.useTime = 23; //How long it takes to use the weapon. If this is shorter than the useAnimation it will fire twice in one click.
            item.useAnimation = 23;  //The animations time length
            item.useStyle = 5; //The style in which the item gets used. 5 for bows.
            item.shoot = 1; //Makes the bow shoot arrows
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0; //The amount of knockback the item has
            item.value = Item.sellPrice(0, 1, 8, 0);
            item.rare = 3; //The item's name color
            item.UseSound = SoundID.Item5;
            item.autoReuse = false; //if the Bow autoreuses or not
            item.shootSpeed = 8f; //The arrows speed when shot
            item.crit = 0; //Crit chance
        }
        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "BoneBow", 1);
                recipe.AddIngredient(ItemID.BeesKnees, 1);
                recipe.AddIngredient(ItemID.DemonBow, 1);
                recipe.AddIngredient(ItemID.MoltenFury, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "BoneBow", 1);
                recipe.AddIngredient(ItemID.BeesKnees, 1);
                recipe.AddIngredient(ItemID.TendonBow, 1);
                recipe.AddIngredient(ItemID.MoltenFury, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        /*public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }*/
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.BoneArrow;
            }
            return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
        }
    }
}