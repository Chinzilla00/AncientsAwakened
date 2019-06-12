using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class UraniumDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Uranium Drill");
		}

		public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 10;
            item.shootSpeed = 32f;
            item.knockBack = 0f;
            item.width = 20;
            item.height = 12;
            item.damage = 15;
            item.pick = 150;
            item.UseSound = SoundID.Item23;
            item.rare = 4;
            item.value = 81000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
            item.shoot = mod.ProjectileType("UraniumDrill");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "UraniumBar", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}