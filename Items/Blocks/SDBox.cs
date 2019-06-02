using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class SDBox : ModItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sleeping Dragon Music Box");

            Tooltip.SetDefault(@"Plays 'Sleeping Dragon' by OmegaFerretMusic");
        }

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SDBox");
            item.width = 72;
			item.height = 36;
			item.rare = 4;
			item.value = 10000;
			item.accessory = true;
            item.rare = 11;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PagodaBox");
            recipe.AddIngredient(null, "LakeBox");
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
