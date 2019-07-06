using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class TechneciumSword : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 51;            
			item.melee = true;            
			item.width = 60;              
			item.height = 62;             
			item.useTime = 24;          
			item.useAnimation = 24;     
			item.useStyle = 1;        
			item.knockBack = 4;      
            item.value = 108000;
            item.rare = 4;
			item.UseSound = SoundID.Item1;       
			item.autoReuse = true;   
			item.useTurn = true;               
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Greatsword");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()  //How to craft this sword
		{
			ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(null, "TechneciumBar", 15);   
			recipe.AddTile(TileID.MythrilAnvil);   
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
