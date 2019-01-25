using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Blastshot : ModItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 70;
            item.noMelee = true;

            item.ranged = true;
            item.width = 62;
            item.height = 24;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<Projectiles.DragonfireProj>();
            item.knockBack = 0;
            item.value = 10;
            item.rare = 5;
            item.UseSound = SoundID.Item34;
            item.autoReuse = false;
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blastshot");
            Tooltip.SetDefault("Doesn't use ammo");
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


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Dragonfire", 5);
            recipe.AddIngredient(null, "IncineriteBare", 10);
            recipe.AddIngredient(null, "SoulOfSmite", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
