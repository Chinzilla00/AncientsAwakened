using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class YtriumChainsaw : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Yttrium Chainsaw");
		}

		public override void SetDefaults()
		{
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 8;
            item.shootSpeed = 40f;
            item.knockBack = 2.75f;
            item.width = 20;
            item.height = 12;
            item.damage = 23;
            item.axe = 14;
            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("YtriumChainsaw");
            item.rare = 4;
            item.value = 54000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
		}

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "YtriumBar", 10);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}