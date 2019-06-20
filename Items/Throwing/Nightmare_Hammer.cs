using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Nightmare_Hammer : BaseAAItem
	{
		public override void SetDefaults()
		{

			item.damage = 31;
			item.melee = true;
			item.width = 22;
			item.noUseGraphic = true;
			item.height = 44;
			item.useTime = 16;
			item.useAnimation = 16;
			item.shoot = mod.ProjectileType("Nightmare_Hammer_Pro");
			item.shootSpeed = 15f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 1, 26, 0);
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 3;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Hammer");
			Tooltip.SetDefault("");
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Bar", 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
