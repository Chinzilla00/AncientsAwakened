using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class BoneBow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Bow");
            Tooltip.SetDefault("Replaces Wooden Arrows with Bone Arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 17; 
            item.noMelee = true; //This makes sure the bow doesn't do melee damage
            item.ranged = true; //This causes your bow to do ranged damage
            item.width = 20; //Hitbox width
            item.height = 64; //Hitbox height
            item.useTime = 25; //How long it takes to use the weapon. If this is shorter than the useAnimation it will fire twice in one click.
            item.useAnimation = 25;  //The animations time length
            item.useStyle = ItemUseStyleID.HoldingOut; //The style in which the item gets used. 5 for bows.
            item.shoot = ProjectileID.WoodenArrowFriendly; //Makes the bow shoot arrows
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0; //The amount of knockback the item has
            item.value = Item.sellPrice(0, 0, 8, 0);
            item.rare = ItemRarityID.Orange; //The item's name color
            item.UseSound = SoundID.Item5;
            item.autoReuse = false; //if the Bow autoreuses or not
            item.shootSpeed = 5f; //The arrows speed when shot
            item.crit = 0; //Crit chance
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
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