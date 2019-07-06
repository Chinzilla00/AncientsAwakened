using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class NightKeeper : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 44;            
            item.melee = true;            
            item.width = 54;              
            item.height = 54;             
            item.useTime = 22;          
            item.useAnimation = 22;     
            item.useStyle = 1;        
            item.knockBack = 4;      
            item.value = 20000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true; 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Night Keeper");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.LightsBane, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 7);			
            recipe.AddIngredient(null, "ExilesKatana", 1);
            recipe.AddIngredient(null, "AbyssiumBar", 7);
            recipe.AddTile(TileID.DemonAltar);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
