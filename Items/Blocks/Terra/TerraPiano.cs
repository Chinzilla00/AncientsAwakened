using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Terra
{
    public class TerraPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Piano");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("KeepPiano");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TerraShard"), 15);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddIngredient(ItemID.Bone, 4);
            //recipe.AddIngredient(ItemID.Torch, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}