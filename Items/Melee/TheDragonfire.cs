using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TheDragonfire : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;            
            item.melee = true;            
            item.width = 52;              
            item.height = 52;             
            item.useTime = 26;          
            item.useAnimation = 26;     
            item.useStyle = 1;        
            item.knockBack = 6;      
            item.value = 20000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dragonfire");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 14);			
            recipe.AddIngredient(null, "FlamingFury", 1);
            recipe.AddIngredient(null, "IncineriteBar", 7);
            recipe.AddTile(TileID.DemonAltar);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
