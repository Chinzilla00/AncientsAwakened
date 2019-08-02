using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Blocks
{
    public class OroborosChest : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Oroboros Chest");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 5;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("OroborosChest");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {

                    line2.overrideColor = new Color(100, 0, 10);

                    line2.overrideColor = AAColor.Rarity13;
//
                }
            }
        }

        public override void AddRecipes()
		{
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DoomiteScrap", 2);
                recipe.AddIngredient(null, "OroborosWood", 12);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}