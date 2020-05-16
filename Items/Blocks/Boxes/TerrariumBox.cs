using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class TerrariumBox : BaseAAItem
	{
            
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terrarium Music Box");
            Tooltip.SetDefault("Plays 'Heart of the World' by Quicksilvur feat Charlie Debnam");

        }

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("TerrariumBox");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
            
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBoxTitle);
            recipe.AddIngredient(null, "MonarchBox", 1);
            recipe.AddIngredient(null, "InfernoBox", 1);
            recipe.AddIngredient(null, "MireUBox", 1);
            recipe.AddIngredient(null, "InfernoBox", 1);
            recipe.AddIngredient(null, "MireUBox", 1);
            recipe.AddIngredient(null, "VoidBox", 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
