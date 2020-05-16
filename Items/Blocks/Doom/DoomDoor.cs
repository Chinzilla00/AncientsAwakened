using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomDoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Door");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("DoomDoorClosed");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ApocalyptitePlate"), 6);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}