using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class MadnessBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Bow");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 26;
            item.useTime = 26;
            item.width = 12;
            item.height = 28;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 11;
            item.shootSpeed = 5f;
            item.knockBack = 1f;
            item.rare = 2;
            item.noMelee = true;
            item.value = 3000;
            item.ranged = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MadnessShard", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}