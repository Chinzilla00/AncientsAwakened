using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TheCorruptor : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 34;            
            item.melee = true;            
            item.width = 72;              
            item.height = 72;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 4;      
            item.value = 10000;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true; 
			item.shoot =  306;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Corruptor");
            Tooltip.SetDefault("Shoots corrupt eaters that break after hitting an enemy");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.RottenChunk, 8);			
            recipe.AddTile(TileID.DemonAltar);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
