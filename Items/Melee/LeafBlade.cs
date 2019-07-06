using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class LeafBlade : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 22;            
            item.melee = true;            
            item.width = 42;              
            item.height = 60;               //Item Description
            item.useTime = 24;          
            item.useAnimation = 24;     
            item.useStyle = 1;        
            item.knockBack = 3;      
            item.value = 1000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = false;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Leaf Blade");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(null, "Everleaf", 6);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
