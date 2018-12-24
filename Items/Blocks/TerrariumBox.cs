using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
	public class TerrariumBox : ModItem
	{
            
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terrarium Music Box");
		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("TerrariumBox");
			item.width = 24;
			item.height = 24;
			item.rare = 4;
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
            recipe.AddIngredient(null, "GripsBox", 1);
            recipe.AddIngredient(null, "BroodBox", 1);
            recipe.AddIngredient(null, "HydraBox", 1);
            recipe.AddIngredient(null, "Boss6Box", 1);
            recipe.AddIngredient(null, "Equibox", 1);
            recipe.AddIngredient(null, "AkumaBox", 1);
            recipe.AddIngredient(null, "YamataBox", 1);
            recipe.AddIngredient(null, "ZeroBox", 1);
            recipe.AddIngredient(null, "AkumaABox", 1);
            recipe.AddIngredient(null, "YamataABox", 1);
            recipe.AddIngredient(null, "ZeroABox", 1);
            recipe.AddIngredient(null, "ShenBox", 1);
            recipe.AddIngredient(null, "ShenABox", 1);
            recipe.AddIngredient(null, "IZBox", 1);
            recipe.AddIngredient(null, "RoHBox", 1);
            recipe.AddIngredient(null, "SABox", 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
