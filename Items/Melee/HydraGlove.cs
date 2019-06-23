using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class HydraGlove : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydra Glove");
		}

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 7;
            item.useTime = 7;
            item.width = 28;
            item.height = 24;
            item.damage = 19;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.scale = 1.35f;
            item.melee = true;
            item.rare = 3;
            item.value = 50000;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "AbyssiumBar", 10);
            recipe.AddIngredient(mod, "HydraClaw", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
