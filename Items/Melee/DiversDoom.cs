using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class DiversDoom : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 147;            
            item.melee = true;            
            item.width = 42;              
            item.height = 50;             
            item.useTime = 23;          
            item.useAnimation = 23;     
            item.useStyle = 1;        
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item1;                  
            item.autoReuse = true;   
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Diver's Doom");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DeepAbyssium", 15);   
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
