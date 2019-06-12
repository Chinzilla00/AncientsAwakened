using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class TechneciumDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Technecium Terraformer");
		}

		public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 7;
            item.shootSpeed = 32f;
            item.knockBack = 0f;
            item.width = 20;
            item.height = 12;
            item.damage = 20;
            item.pick = 180;
            item.UseSound = SoundID.Item23;
            item.rare = 4;
            item.value = 108000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
            item.shoot = mod.ProjectileType("TechneciumDrill");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TechneciumBar", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}