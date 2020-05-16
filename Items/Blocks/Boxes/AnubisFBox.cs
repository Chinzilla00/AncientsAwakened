using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class AnubisFBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forsaken Anubis Music Box");
            Tooltip.SetDefault(@"Plays 'Purgatorium' by Universe");
        }

        public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("AnubisFBox");
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Pink;
			item.value = 10000;
			item.accessory = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "SoulFragment", 3);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
