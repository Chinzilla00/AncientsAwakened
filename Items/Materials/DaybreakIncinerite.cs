using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class DaybreakIncinerite : BaseAAItem
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
            item.rare = ItemRarityID.Purple;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("DaybreakIncineriteBar");
            item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
        }
        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncineriteOre", 5);
            recipe.AddIngredient(null, "RadiantIncinerite", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
