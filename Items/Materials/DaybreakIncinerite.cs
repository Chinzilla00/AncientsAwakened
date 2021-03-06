using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class DaybreakIncinerite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daybreak Incinerite");
            Tooltip.SetDefault("Bright as the radiant sun");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
			item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 11;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("DaybreakIncineriteBar");
        }
        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 4);
            recipe.AddIngredient(ItemID.LunarBar, 1);
            recipe.AddIngredient(null, "RadiantIncinerite", 1);              //example of how to craft with a modded item
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
