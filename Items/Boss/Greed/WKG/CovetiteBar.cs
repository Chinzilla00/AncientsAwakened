using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class CovetiteBar : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.value = 16000;
            item.rare = ItemRarityID.Green;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("CovetiteBar");
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Covetite Bar");
            Tooltip.SetDefault("It's somehow shiny but not at the same time. How did greed fall for this?");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CovetiteOre", 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
