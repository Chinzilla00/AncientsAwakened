using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomChandelier : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Chandelier");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 38;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("DoomChandelier");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ApocalyptitePlate"), 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Chain);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}