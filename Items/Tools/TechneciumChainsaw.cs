using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TechneciumChainsaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Technecium Holosaw");
		}

		public override void SetDefaults()
		{
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 6;
            item.shootSpeed = 40f;
            item.knockBack = 4.5f;
            item.width = 20;
            item.height = 12;
            item.damage = 33;
            item.axe = 35;
            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("TechneciumChainsaw");
            item.rare = 4;
            item.value = 108000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
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