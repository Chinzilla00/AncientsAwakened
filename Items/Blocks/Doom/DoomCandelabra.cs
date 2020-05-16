using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomCandelabra : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Candelabra");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("DoomCandelabra");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ApocalyptitePlate"), 5);
            recipe.AddIngredient(ItemID.Torch, 3);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}