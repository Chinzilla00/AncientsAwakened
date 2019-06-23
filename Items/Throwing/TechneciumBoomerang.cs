using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class TechneciumBoomerang : BaseAAItem
	{
		public override void SetDefaults()
		{

			item.damage = 52;            
			item.melee = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 15;
			item.useAnimation = 15;
			item.noUseGraphic = true;
			item.useStyle = 1;
			item.knockBack = 0;
            item.value = 108000;
            item.rare = 6;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType ("TechneciumBoomerangP");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Boomerang");
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
			recipe.AddIngredient(null, "TechneciumBar", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
