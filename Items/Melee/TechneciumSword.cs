using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class TechneciumSword : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 51;            //Sword damage
			item.melee = true;            //if it's melee
			item.width = 60;              //Sword width
			item.height = 62;             //Sword height
			item.useTime = 24;          //how fast 
			item.useAnimation = 24;     
			item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
			item.knockBack = 4;      //Sword knockback
			item.value = 100000;        
			item.rare = 4;
			item.UseSound = SoundID.Item1;       //1 is the sound of the sword
			item.autoReuse = true;   //if it's capable of autoswing.
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
			recipe.AddIngredient(null, "TechneciumBar", 15);   //you need 1 DirtBlock
			recipe.AddTile(TileID.MythrilAnvil);   //at work bench
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
