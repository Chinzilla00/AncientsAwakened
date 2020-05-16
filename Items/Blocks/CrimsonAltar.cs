using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class CrimsonAltar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Altar");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("EvilAltar");
            item.placeStyle = 1;
            item.width = 28;
            item.height = 24;
            item.rare = ItemRarityID.Orange;
            item.value = 1000;
            item.accessory = false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

