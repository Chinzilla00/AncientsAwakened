using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class DeepAbyssium : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = 2;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("DeepAbyssium");
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deep Abyssium Bar");
            Tooltip.SetDefault("It's a wonder you can even see it, it's so dark");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 2);
            recipe.AddIngredient(null, "AbyssiumBar", 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
