using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class Soul_Forge : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Forge");
			Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 34;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 10000;
			item.createTile = mod.TileType("Soul_Forge_Placed");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("AnyIchor", 15);
			recipe.AddRecipeGroup("AnyHardmodeForge");
			recipe.AddIngredient(null, "Nightmare_Bar", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}