using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class TechneciumBar : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 70, 0);
            item.width = 32;
            item.height = 32;
			item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("TechneciumBar");
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Technecium Bar");
          Tooltip.SetDefault("");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TechneciumOre", 6);              //example of how to craft with a modded item
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
