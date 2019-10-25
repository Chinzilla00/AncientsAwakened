using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class MadnessHammer : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 30;
            item.useTime = 24;
            item.autoReuse = true;
            item.damage = 7;
            item.hammer = 50;
            item.UseSound = SoundID.Item1;
            item.knockBack = 3f;
            item.value = 10000;
            item.melee = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Hammer");
        }

        public override void AddRecipes()  //How to craft item item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MadnessFragment", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
