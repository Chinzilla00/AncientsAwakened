using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class VoidUnit : BaseAAItem
	{

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Recreation Unit");
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
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.createTile = mod.TileType("VoidUnit");
            item.rare = 10;
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 15);
			recipe.AddTile(null, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}