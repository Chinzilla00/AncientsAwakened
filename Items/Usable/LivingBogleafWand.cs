using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    class LivingBogleafWand : ModItem
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
            item.createTile = mod.TileType("LivingBogleaves"); //put your CustomBlock Tile name
            item.useAmmo = mod.ItemType("Bogwood");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Bogleaf Wand");
            Tooltip.SetDefault("Consumes Bogwood");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LivingMahoganyLeafWand, 1);
            recipe.AddIngredient(null, "Bogwood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}


