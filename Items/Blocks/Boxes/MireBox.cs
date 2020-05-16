using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class MireBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mire Surface Music Box");
            Tooltip.SetDefault(@"Plays 'Moonlit Marsh' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("MireBox");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "Bogwood", 20);
			recipe.AddTile(TileID.Sawmill);
			recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
