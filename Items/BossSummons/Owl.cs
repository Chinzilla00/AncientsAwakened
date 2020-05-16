using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class Owl : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Owl Statue");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons Athena
Can only be used in the Acropolis at the Owl Altar
'It stares into your soul.'");
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
            recipe.AddIngredient(null, "SeraphFeather", 15);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}