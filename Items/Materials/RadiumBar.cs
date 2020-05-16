using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class RadiumBar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Bar");
            Tooltip.SetDefault("It's sparkly");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RadiumBarTile");
            item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
        }
        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumOre", 5);              //example of how to craft with a modded item
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
