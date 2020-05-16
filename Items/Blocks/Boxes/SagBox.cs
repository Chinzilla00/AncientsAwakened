using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
	public class SagBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius Music Box");

            Tooltip.SetDefault(@"Plays 'Event Horizon' by SpectralAves");
        }

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SagBox");
            item.width = 72;
			item.height = 36;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
