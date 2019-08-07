using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class YtriumDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Yttrium Drill");
		}

		public override void SetDefaults()
		{
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 13;
            item.shootSpeed = 32f;
            item.knockBack = 0f;
            item.width = 20;
            item.height = 12;
            item.damage = 10;
            item.pick = 100;
            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("YtriumDrill");
            item.rare = 2;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}