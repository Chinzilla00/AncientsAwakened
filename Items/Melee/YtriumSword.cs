using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class YtriumSword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;            
            item.melee = true;            
            item.width = 50;              
            item.height = 58;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 4;      
            item.value = 20;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);

        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Yttrium Blade");
          Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "YtriumBar", 15);   
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
