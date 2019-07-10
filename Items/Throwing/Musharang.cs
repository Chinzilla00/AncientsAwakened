using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Musharang : BaseAAItem
	{
		public override void SetDefaults()
		{

            item.damage = 16;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 16;
			item.useAnimation = 16;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 1;
			item.shootSpeed = 6f;
			item.shoot = mod.ProjectileType ("Musharang");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Musharang");
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
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
