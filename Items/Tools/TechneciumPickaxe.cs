using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
	public class TechneciumPickaxe : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 7;
			item.useAnimation = 20;
			item.pick = 180;
			item.useStyle = 1;
			item.knockBack = 5;
            item.value = 108000;
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Pickaxe");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()  
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TechneciumBar", 15);
			recipe.AddTile(TileID.MythrilAnvil);   
			recipe.SetResult(this);  
			recipe.AddRecipe();
		}
    }
}
