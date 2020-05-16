using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class RelicBar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relic Bar");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Green;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RelicBar");
            item.value = Terraria.Item.sellPrice(0, 0, 32, 0);
        }

        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VikingRelic", 2);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
