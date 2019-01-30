using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class MadnessStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Staff");
        }

        public override void SetDefaults()
        {
            item.damage = 14;
            item.magic = true;
            item.mana = 6;
            item.width = 42;
            item.height = 42;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("MadnessSphere");
            item.shootSpeed = 3f;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MadnessFragment", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}