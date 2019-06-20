using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DepthDigger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Depth Digger");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 40;
			item.height = 40;
            item.tileBoost += 1;
            item.useTime = 13;
			item.useAnimation = 22;
			item.pick = 200;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 3600;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DeepAbyssium", 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
	}
}