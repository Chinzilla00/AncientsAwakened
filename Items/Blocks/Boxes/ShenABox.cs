using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class ShenABox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shen Doragon Awakened Music Box");
            
            Tooltip.SetDefault(@"Plays 'Blaze of Glory' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("ShenABox");
            item.width = 72;
			item.height = 36;
			item.rare = ItemRarityID.LightRed;
			item.value = 10000;
			item.accessory = true;
        }

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "ShenBox");
                recipe.AddIngredient(null, "ChaosSoul");
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
