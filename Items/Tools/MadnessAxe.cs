using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class MadnessAxe : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 20;
            item.useTime = 20;
            item.autoReuse = true;
            item.width = 24;
            item.height = 28;
            item.damage = 7;
            item.axe = 10;
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
            DisplayName.SetDefault("Madness Axe");
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
