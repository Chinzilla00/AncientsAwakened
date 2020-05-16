using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class SerpentBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent Music Box");
            Tooltip.SetDefault(@"Plays 'Burrowing Down' by Charlie Debnam");
        }
        

        public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SerpentBox");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Orange;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "SnowMana", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
