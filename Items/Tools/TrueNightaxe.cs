using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TrueNightaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 8;
            item.useAnimation = 17;
            item.pick = 205;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Nightaxe");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Nightaxe");
            recipe.AddIngredient(mod, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
