using Terraria.ModLoader;

using Terraria.ID;

namespace AAMod.Items.Usable
{
    class LivingRazeleafWand : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 26;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = false;
            item.createTile = mod.TileType("LivingRazeleaves"); //put your CustomBlock Tile name
            item.useAmmo = mod.ItemType("Razewood");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Razeleaf Wand");
            Tooltip.SetDefault("Consumes Razewood");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeafWand, 1);
            recipe.AddIngredient(null, "Razewood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
