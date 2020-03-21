using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Blocks
{
    public class AkumaABox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma Awakened Music Box");
            Tooltip.SetDefault(@"Plays 'Dawn of the Dragon' by Universe");
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
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("AkumaABox");
			item.width = 24;
			item.height = 24;
			item.rare = 4;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AkumaBox");
            recipe.AddIngredient(null, "TaiyangBaolei");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
