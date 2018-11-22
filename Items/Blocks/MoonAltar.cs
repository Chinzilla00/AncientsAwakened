using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class MoonAltar : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Altar");
        }

        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 10;
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.createTile = mod.TileType("MoonAltar");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EventideAbyssium", 15);
			recipe.AddTile(null, "ChaosCrucible");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}