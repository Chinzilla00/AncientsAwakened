using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DoomiteGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Greatsword");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 9;
            item.useTime = 20;
            item.width = 64;
            item.height = 64;
            item.damage = 45;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.rare = 5;
            item.value = 46000;
            item.melee = true;
            item.shoot = mod.ProjectileType("DoomBlast");
            item.shootSpeed = 8f;
        }

        private static int shoot;

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            shoot++;
            if (shoot % 2 != 0) return false;

            shoot = 0;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "DoomiteSaber");
            recipe.AddIngredient(mod, "VoidEnergy", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}