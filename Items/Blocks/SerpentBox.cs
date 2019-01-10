using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class SerpentBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent Music Box");
		}
        

        public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SerpentBox");
			item.width = 24;
			item.height = 24;
			item.rare = 3;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "SnowMana", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
