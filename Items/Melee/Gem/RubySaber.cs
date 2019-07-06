using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class RubySaber : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 26;            
            item.melee = true;            
            item.width = 44;              
            item.height = 44;               //Item Description
            item.useTime = 17;          
            item.useAnimation = 17;     
            item.useStyle = 1;        
            item.knockBack = 3;      
            item.value = 1000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ruby Saber");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Ruby, 5);   
            recipe.AddRecipeGroup("AAMod:Gold", 12);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
