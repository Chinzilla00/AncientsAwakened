using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class YtriumBoomerang : ModItem
	{
		public override void SetDefaults()
		{

            item.damage = 30;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 12;
			item.useAnimation = 12;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.rare = 6;
			item.shootSpeed = 9f;
			item.shoot = mod.ProjectileType ("YtriumBoomerangP");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);

        }

        public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Yttrium Boomerang");
      Tooltip.SetDefault("");
    }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(null, "YtriumBar", 12);
				recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
		}
    }
}
