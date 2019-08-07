using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class UraniumDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Uranium Drill");
		}

		public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 23;
            item.useTime = 6;
            item.shootSpeed = 40f;
            item.knockBack = 1f;
            item.width = 20;
            item.height = 12;
            item.damage = 35;
            item.pick = 200;
            item.UseSound = SoundID.Item23;
            item.rare = 7;
            item.value = 216000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
            item.tileBoost++;
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