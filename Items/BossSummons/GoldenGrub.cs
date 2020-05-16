using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class GoldenGrub : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Grub");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons Greed
Can only be used in Greed's Hoard at the Altar of Desire
'It's really shiny.'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.rare = ItemRarityID.LightPurple;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CovetiteCrystal", 15);
            recipe.AddIngredient(ItemID.Topaz, 2);
            recipe.AddIngredient(ItemID.MechanicalWorm, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}