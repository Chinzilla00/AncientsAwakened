using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class MadnessPick : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 5;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 11;
            item.useAnimation = 20;
            item.pick = 40;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Pickaxe");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MadnessFragment", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
