using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class PagodaBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pagoda of the Rising Sun Music Box");
            Tooltip.SetDefault("Plays 'Scorched Tower' by Rockwizard");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }
        public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("PagodaBox");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
