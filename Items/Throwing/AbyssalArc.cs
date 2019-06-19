using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class AbyssalArc : ModItem
	{
		public override void SetDefaults()
		{

            item.damage = 60;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 16;
			item.useAnimation = 16;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
			item.shootSpeed = 15f;
			item.shoot = mod.ProjectileType ("AntimonBoomerangP");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Arc");
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
				recipe.AddIngredient(null, "DeepAbyssium", 12);
				recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
		}
    }
}
