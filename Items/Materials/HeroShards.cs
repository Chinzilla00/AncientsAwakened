using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class HeroShards : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hero Relics");
            Tooltip.SetDefault("Shards of a shattered relic");
        }

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}
