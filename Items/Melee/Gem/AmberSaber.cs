using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class AmberSaber : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 24;            
            item.melee = true;            
            item.width = 44;              
            item.height = 44;               //Item Description
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 1000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amber Saber");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Amber, 5);   
            recipe.AddIngredient(ItemID.DesertFossil, 12);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
