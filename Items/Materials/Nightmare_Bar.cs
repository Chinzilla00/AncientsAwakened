using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public abstract class Nightmare_Bar : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Bar");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 0, 21, 0);
			item.rare = 4;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("NightmareBar");
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Ore", 3);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
