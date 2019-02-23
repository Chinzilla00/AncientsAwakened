using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TechneciumHamaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.useTurn = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 35;
            item.useTime = 6;
            item.knockBack = 7f;
            item.width = 20;
            item.height = 12;
            item.damage = 43;
            item.axe = 100;
            item.hammer = 85;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.value = 108000;
            item.melee = true;
            item.scale = 1.1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Landscaper");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "TechneciumBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
