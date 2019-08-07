using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class UraniumChainsaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Uranium Chainsaw");
		}

		public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 23;
            item.useTime = 6;
            item.shootSpeed = 46f;
            item.knockBack = 4.6f;
            item.width = 20;
            item.height = 12;
            item.damage = 50;
            item.axe = 23;
            item.UseSound = SoundID.Item23;
            item.rare = 7;
            item.value = 216000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
            item.tileBoost++;
            item.shoot = mod.ProjectileType("UraniumChainsaw");
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