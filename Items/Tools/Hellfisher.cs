using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Hellfisher : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hellfisher");
		}

		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.HotlineFishingHook);
            item.shoot = mod.ProjectileType<Hellfisher_Bob>();
		}

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "IncineriteBar", 12);
                recipe.AddTile(null, "HellstoneAnvil");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
	}
}