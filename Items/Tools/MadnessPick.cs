using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class MadnessPick : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 20;
            item.useTime = 16;
            item.autoReuse = true;
            item.width = 24;
            item.height = 28;
            item.damage = 7;
            item.pick = 55;
            item.UseSound = SoundID.Item1;
            item.knockBack = 2.5f;
            item.value = 10000;
            item.melee = true;
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
