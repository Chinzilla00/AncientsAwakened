using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class TheSquirter : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 84;
            item.noMelee = true;

            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType ("Squirt");
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item34;
            item.autoReuse = false;
            item.shootSpeed = 14f;

        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, -2);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Squirter");
            Tooltip.SetDefault("Doesnt use ammo");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SlimeGun, 1);
            recipe.AddIngredient(ItemID.Gel, 200);
            recipe.AddIngredient(null, "DeepAbyssium", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
