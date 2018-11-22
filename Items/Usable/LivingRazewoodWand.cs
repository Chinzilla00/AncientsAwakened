using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    class LivingRazewoodWand : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 26;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = false;
            item.createTile = mod.TileType("LivingRazewood"); //put your CustomBlock Tile name
            item.useAmmo = mod.ItemType("Razewood");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Razewood Wand");
            Tooltip.SetDefault("Consumes Razewood");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LivingWoodWand, 1);
            recipe.AddIngredient(null, "Razewood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
