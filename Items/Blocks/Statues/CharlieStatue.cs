using Terraria.ModLoader; using Terraria.ID;

namespace AAMod.Items.Blocks.Statues
{
	public class CharlieStatue : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guitar Statue");
        }

        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 50000;
			item.rare = ItemRarityID.Blue;
			item.createTile = mod.TileType("DevStatue");
			item.placeStyle = 13;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}