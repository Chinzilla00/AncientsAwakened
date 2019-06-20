using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class UraniumBar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Bar");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.value = 22000;
            item.rare = 3;
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("UraniumBar");
        }

        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Uranium", 4);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
