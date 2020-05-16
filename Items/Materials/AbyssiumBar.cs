using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class AbyssiumBar : BaseAAItem
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
            item.createTile = mod.TileType("AbyssiumBar");
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssium Bar");
            Tooltip.SetDefault("Solid Darkness");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Abyssium", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
