using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Xiphos : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 29;            
            item.melee = true;            
            item.width = 32;              
            item.height = 32;             
            item.useTime = 23;          
            item.useAnimation = 23;     
            item.useStyle = 1;        
            item.knockBack = 4;
            item.value = Terraria.Item.sellPrice(0, 0, 20, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;                  
            item.autoReuse = true;   
            item.useTurn = true;               
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Xiphos");
          Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "StarcloudBar", 15);   
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
