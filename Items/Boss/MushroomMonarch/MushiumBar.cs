using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class MushiumBar : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = 1;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MushiumBar");
            item.value = Terraria.Item.sellPrice(0, 0, 9, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushium Bar");
            Tooltip.SetDefault("Mushy");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mushium", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
