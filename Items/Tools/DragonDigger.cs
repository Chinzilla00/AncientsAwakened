using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DragonDigger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dragon Digger");
            Tooltip.SetDefault("Dragons Don't really dig, but this'll do");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 22;
			item.useAnimation = 22;
			item.pick = 75;
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
                recipe.AddIngredient(null, "IncineriteBar", 12);
                recipe.AddIngredient(null, "BroodScale", 6);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
	}
}