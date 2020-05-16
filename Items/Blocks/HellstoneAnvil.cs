using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class HellstoneAnvil : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellstone Anvil");
            Tooltip.SetDefault("Is this thing supposed to be on fire?");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Orange;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("HellstoneAnvil");
        }

        public override void AddRecipes()
        { 
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.IronAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.LeadAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
