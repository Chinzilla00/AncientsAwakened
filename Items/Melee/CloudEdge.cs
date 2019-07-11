using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class CloudEdge : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 20;            
            item.melee = true;            
            item.width = 32;              
            item.height = 32;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 1;      
            item.value = 5000;        
            item.rare = 2;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;
            item.shoot = mod.ProjectileType("CloudEdgeP");
            item.shootSpeed = 12f;                                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cloud Edge");
      Tooltip.SetDefault("Shoots cloud projectiles");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.FallenStar, 5);   
			recipe.AddIngredient(ItemID.Cloud, 200);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
