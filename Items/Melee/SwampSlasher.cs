using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class SwampSlasher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SwampSlasher");
		}
		public override void SetDefaults()
		{
			item.damage = 41;
			item.melee = true;
			item.width = 35;
			item.height = 45;
			item.useTime = 40;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{//exile's katana at hellstone anvil + 10 hydra claws + 5 hydra hide
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ExilesKatana", 1);
            recipe.AddIngredient(null, "HydraClaw", 10);
            recipe.AddIngredient(null, "HydraHide", 5);
            recipe.AddTile(null, "HellstoneAnvil");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
