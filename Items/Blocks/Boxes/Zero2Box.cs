using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;

namespace AAMod.Items.Blocks.Boxes
{
    public class Zero2Box : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Awakened Music Box");
            Tooltip.SetDefault("Plays 'Doomsday Arrives' by Saucecoie");
        }
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("Zero2Box");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            { 
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "ZeroBox");
                recipe.AddIngredient(null, "BrokenCode");
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
