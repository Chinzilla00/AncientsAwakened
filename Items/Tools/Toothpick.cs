using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Toothpick : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 8;
            item.melee = true;
            item.width = 38;
            item.height = 38;
            item.useTime = 20;
            item.useAnimation = 20;
            item.pick = 90;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toothpick");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 12);
            recipe.AddRecipeGroup("AAMod:Gold", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
