using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class Transmuter : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Material Transmuter");
            Tooltip.SetDefault(@"Allows for Transmutation of materials into their counterparts");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Green;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.expert = true; item.expertOnly = true;
            item.createTile = mod.TileType("Transmuter");
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 10);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddRecipeGroup("AAMod:Altar");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}