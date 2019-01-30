using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class UraniumAxe : ModItem
    {
        public override void SetDefaults()
        {
            item.useTurn = true;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 39;
            item.useTime = 7;
            item.knockBack = 7f;
            item.width = 36;
            item.height = 38;
            item.damage = 39;
            item.axe = 100;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.value = 81000;
            item.melee = true;
            item.scale = 1.1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Logger");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "UraniumBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
